using Moq;

using StandardDevOpsApi.Models.Students;
using StandardDevOpsApi.Services.Foundations.LocalStudentEvents;
using StandardDevOpsApi.Services.Foundations.StudentEvents;
using StandardDevOpsApi.Services.Foundations.Students;

using Tynamix.ObjectFiller;

namespace StandardDevOpsApi.Services.Orchestrations.StudentEvents
{
    public partial class StudentEventOrchestrationServiceTests
    {
        private readonly Mock<ILocalStudentEventService> localStudentEventServiceMock;
        private readonly IStudentEventOrchestrationService studentEventOrchestrationService;
        private readonly Mock<IStudentEventService> studentEventServiceMock;
        private readonly Mock<IStudentIndexationService> studentIndexationServiceMock;
        private readonly Mock<IStudentService> studentServiceMock;

        public StudentEventOrchestrationServiceTests()
        {
            this.studentEventServiceMock = new Mock<IStudentEventService>();
            this.studentServiceMock = new Mock<IStudentService>(MockBehavior.Strict);
            this.studentIndexationServiceMock = new Mock<IStudentIndexationService>(MockBehavior.Strict);
            this.localStudentEventServiceMock = new Mock<ILocalStudentEventService>(MockBehavior.Strict);

            this.studentEventOrchestrationService = new StudentEventOrchestrationService(
                studentEventService: this.studentEventServiceMock.Object,
                studentService: this.studentServiceMock.Object,
                localStudentEventService: this.localStudentEventServiceMock.Object,
                studentIndexationService: this.studentIndexationServiceMock.Object
                );
        }

        private static Student CreateRandomStudent() =>
          CreateStudentFiller().Create();

        private static Filler<Student> CreateStudentFiller() =>
            new Filler<Student>();
    }
}