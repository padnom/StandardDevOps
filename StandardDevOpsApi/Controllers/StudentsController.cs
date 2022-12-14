using System;
using System.Threading.Tasks;

using StandardDevOpsApi.Models.LibraryAccounts;
using StandardDevOpsApi.Models.Students;
using StandardDevOpsApi.Services.Foundations.StudentEvents;
using StandardDevOpsApi.Services.Foundations.Students;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using StandardDevOpsApi.Brokers.Apis.ElasticApis;

namespace StandardDevOpsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : RESTFulController
    {
        private readonly IStudentIndexationService studentService;
        private readonly IStudentEventService studentEventService;
        private readonly IElasticApiBroker elasticApiBroker;

        public StudentsController(IStudentIndexationService studentService,IStudentEventService studentEventService, IElasticApiBroker elasticApiBroker)
        {
            this.studentService = studentService;
            this.studentEventService = studentEventService;
            this.elasticApiBroker = elasticApiBroker;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Student>> PostStudentAsync(Student student)
        {
            student.Id = Guid.NewGuid();
            await this.studentEventService.PublishStudentToQueueAsync(student);
            return Created(student);
        }
    }
}
