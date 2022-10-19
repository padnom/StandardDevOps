using System;
using System.Threading.Tasks;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Services.Orchestrations.StudentEvents
{
    public interface IStudentEventOrchestrationService
    {
        void ListenToStudentEvents();
    }
}
