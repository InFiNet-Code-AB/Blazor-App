using Application_Layer.Commands.CourseCommands.CreateCourse;
using MediatR;
using Shared_Layer.DTO_s.Course;

namespace Application_Layer.Commands.CourseCommands
{
    public class CreateCourseCommand : IRequest<CreateCourseResult>
    {
        public CreateCourseDTO CreateCourseDTO { get; set; }

        public CreateCourseCommand(CreateCourseDTO createCourseDTO)
        {
            CreateCourseDTO = createCourseDTO;
        }
    }
}

