using System;
using System.Threading.Tasks;
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

        public StudentEventOrchestrationService(
            IStudentEventService studentEventService,
            IStudentService studentService,
            ILocalStudentEventService localStudentEventService)
        {
            this.studentEventService = studentEventService;
            this.studentService = studentService;
            this.localStudentEventService = localStudentEventService;
        }

        public void ListenToStudentEvents()
        {
            this.studentEventService.ListenToStudentEvent(async (student) =>
            {
                await this.studentService.AddStudentAsync(student);
                await this.localStudentEventService.PublishStudentAsync(student);
            });
        }
    }
}
