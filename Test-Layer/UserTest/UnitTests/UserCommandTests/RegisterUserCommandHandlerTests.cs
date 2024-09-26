﻿using Application_Layer.Commands.RegisterNewUser;
using AutoFixture;
using AutoFixture.DataAnnotations;
using AutoMapper;
using Domain_Layer.Models.User;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;
using Shared_Layer.DTO_s.User;

namespace Test_Layer.UserTest.UnitTests.UserCommandTests
{
    [TestFixture]
    public class RegisterUserCommandHandlerTests
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private RegisterUserCommandHandler _handler;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
            _handler = new RegisterUserCommandHandler(_userRepository, _mapper);
            _fixture = new Fixture();
            _fixture.Customize(new NoDataAnnotationsCustomization());

        }

        [Test]
        public async Task Handle_ValidNewUser_ReturnsRegisteredUser1()
        {
            // Arrange
            var userDto = _fixture.Build<RegisterUserDTO>()
                                  .With(u => u.Email, "test@example.com")
                                  .With(u => u.Password, "Password123!")
                                  .With(u => u.ConfirmPassword, "Password123!")
                                  .With(u => u.Role, "Teacher")
                                  .Create();

            var command = new RegisterUserCommand(userDto);
            var newUser = _mapper.Map<UserModel>(userDto);
            var registeredUser = _fixture.Build<UserModel>()
                                         .With(u => u.Email, newUser.Email)
                                         .Create();

            A.CallTo(() => _mapper.Map<UserModel>(A<RegisterUserDTO>.That.IsEqualTo(userDto)))
                .Returns(newUser);
            A.CallTo(() => _userRepository.RegisterUserAsync(newUser, userDto.Password, userDto.Role))
                .Returns(registeredUser);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.EqualTo(registeredUser));
            Assert.That(result.Id, Is.EqualTo(registeredUser.Id));
            Assert.That(result.FirstName, Is.EqualTo(registeredUser.FirstName));
            Assert.That(result.Email, Is.EqualTo(registeredUser.Email));
            Assert.That(result.Role, Is.EqualTo(registeredUser.Role));
            A.CallTo(() => _userRepository.RegisterUserAsync(A<UserModel>.That.Matches(u => u.Email == newUser.Email && u.FirstName == newUser.FirstName && u.LastName == newUser.LastName), userDto.Password, userDto.Role))
                .MustHaveHappenedOnceExactly();
        }


        [Test]
        public async Task Handle_ValidNewUser_ReturnsRegisteredUser()
        {
            // Arrange
            var command = _fixture.Create<RegisterUserCommand>();
            var newUser = _fixture.Create<UserModel>();
            var registeredUser = _fixture.Create<UserModel>();

            A.CallTo(() => _mapper.Map<UserModel>(command.NewUser))
                .Returns(newUser);
            A.CallTo(() => _userRepository.RegisterUserAsync(newUser, command.NewUser.Password, command.NewUser.Role))
                .Returns(registeredUser);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.EqualTo(registeredUser));
            A.CallTo(() => _userRepository.RegisterUserAsync(newUser, command.NewUser.Password, command.NewUser.Role))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Handle_InvalidNewUser_ThrowsArgumentNullException()
        {
            // Arrange
            var command = new RegisterUserCommand(null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, default));
        }
        [Test]
        public void Handle_FailedRegistration_ThrowsException()
        {
            // Arrange
            var userDto = _fixture.Create<RegisterUserDTO>();
            var command = new RegisterUserCommand(userDto);
            var newUser = _mapper.Map<UserModel>(userDto);

            A.CallTo(() => _mapper.Map<UserModel>(A<RegisterUserDTO>.That.IsEqualTo(userDto)))
                .Returns(newUser);
            A.CallTo(() => _userRepository.RegisterUserAsync(newUser, userDto.Password, userDto.Role))
                .Throws<Exception>();

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, default));
        }
    }
}
