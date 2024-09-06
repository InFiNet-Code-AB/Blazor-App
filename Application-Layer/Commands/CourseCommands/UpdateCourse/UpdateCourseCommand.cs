using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared_Layer.DTO_s.Course;

namespace Application_Layer.Commands.CourseCommands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<IActionResult>
    {
        public string CourseId { get; set; }
        public CourseUpdateDTO CourseUpdateDTO { get; set; }

        public UpdateCourseCommand(CourseUpdateDTO courseUpdateDTO, string courseId)
        {
            CourseUpdateDTO = courseUpdateDTO;
            CourseId = courseId;
        }
    }
}
