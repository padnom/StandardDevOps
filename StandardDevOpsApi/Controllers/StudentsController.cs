using System;
using System.Threading.Tasks;

using StandardDevOpsApi.Models.LibraryAccounts;
using StandardDevOpsApi.Models.Students;
using StandardDevOpsApi.Services.Foundations.StudentEvents;
using StandardDevOpsApi.Services.Foundations.Students;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace StandardDevOpsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : RESTFulController
    {
        private readonly IStudentService studentService;
        private readonly IStudentEventService studentEventService;

        public StudentsController(IStudentService studentService,IStudentEventService studentEventService)
        {
            this.studentService = studentService;
            this.studentEventService = studentEventService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Student>> PostStudentAsync(Student student)
        {
            Guid studentId = Guid.NewGuid();
            student = new Student
            {
                Id = studentId,
                Name = "name"
            };
            await studentEventService.PublishStudentToQueueAsync(student);

            return Created(student);
        }
    }
}
