using System;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;

using StandardDevOpsApi.Models.Students;
using StandardDevOpsApi.Services.Foundations.LocalStudentEvents;
using StandardDevOpsApi.Services.Foundations.StudentEvents;
using StandardDevOpsApi.Services.Foundations.Students;

namespace StandardDevOpsApi.Services.Orchestrations.StudentEvents
{
    public class StudentEventOrchestrationService : IStudentEventOrchestrationService
    {
        private readonly IStudentEventService studentEventService;
        private readonly IStudentService studentService;
        private readonly ILocalStudentEventService localStudentEventService;
        private readonly IStudentIndexationService studentIndexationService;

        public StudentEventOrchestrationService(
            IStudentEventService studentEventService,
            IStudentService studentService,
            ILocalStudentEventService localStudentEventService,
            IStudentIndexationService studentIndexationService)
        {
            this.studentEventService = studentEventService;
            this.studentService = studentService;
            this.localStudentEventService = localStudentEventService;
            this.studentIndexationService = studentIndexationService;
        }

        public void ListenToStudentEvents()
        {
            this.studentEventService.ListenToStudentEvent(async (student) =>
            {
                await this.studentService.AddStudentAsync(student);
                await this.localStudentEventService.PublishStudentAsync(student);
                await this.studentIndexationService.AddStudentAsync(student);
            });
        }
    }
}
