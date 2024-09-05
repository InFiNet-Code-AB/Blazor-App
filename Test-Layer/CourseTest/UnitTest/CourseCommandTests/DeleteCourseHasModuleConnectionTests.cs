using Application_Layer.Commands.CourseCommands.DeleteCourseHasModuleConnection;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Course;

namespace Test_Layer.CourseTest.UnitTest.CourseCommandTests
{
    public class DeleteCourseHasModuleConnectionTests
    {
        private DeleteCourseHasModuleConnectionCommandHandler _handler;
        private ICourseRepository _courseRepository;

        [SetUp]
        public void SetUp()
        {
            _courseRepository = A.Fake<ICourseRepository>();
            _handler = new DeleteCourseHasModuleConnectionCommandHandler(_courseRepository);
        }

        [Test]
        public async Task Handle_Successful_Deletion_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResult = new DeleteCourseHasModuleConnectionResult { Success = true, Message = "Connection is successfully deleted" };

            var command = new DeleteCourseHasModuleConnectionCommand("validCourseId", "validModuleId");
            A.CallTo(() => _courseRepository.DeleteCourseHasModuleConnection(command.CourseId, command.ModuleId)).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.That(result.Message, Is.EqualTo(expectedResult.Message));
        }

        [Test]
        public async Task Handle_EmptyIds_ReturnsFailure()
        {
            // Arrange
            var command = new DeleteCourseHasModuleConnectionCommand("", "");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("CourseId or ModuleId cannot be empty!"));
        }
        [Test]
        public async Task Handle_DeletionFailure_ReturnsFailure()
        {
            // Arrange
            var command = new DeleteCourseHasModuleConnectionCommand("validCourseId", "validModuleId");
            A.CallTo(() => _courseRepository.DeleteCourseHasModuleConnection(command.CourseId, command.ModuleId)).ThrowsAsync(new Exception("Deletion failed"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("An error occurred: Deletion failed"));
        }
    }
}
