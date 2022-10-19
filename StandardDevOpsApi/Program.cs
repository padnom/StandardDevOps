using StandardDevOpsApi.Brokers.Apis.ElasticApis;
using StandardDevOpsApi.Brokers.Events;
using StandardDevOpsApi.Brokers.Queues;
using StandardDevOpsApi.Brokers.Storages;
using StandardDevOpsApi.Services.Foundations.LibraryAccounts;
using StandardDevOpsApi.Services.Foundations.LibraryCards;
using StandardDevOpsApi.Services.Foundations.LocalStudentEvents;
using StandardDevOpsApi.Services.Foundations.StudentEvents;
using StandardDevOpsApi.Services.Foundations.Students;
using StandardDevOpsApi.Services.Orchestrations.LibraryAccounts;
using StandardDevOpsApi.Services.Orchestrations.StudentEvents;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StorageBroker>();
builder.Services.AddSingleton<IStorageBroker, StorageBroker>();
builder.Services.AddSingleton<IQueueBrokerMassTransit, QueueBrokerMassTransit>();
builder.Services.AddSingleton<IQueueBroker, QueueBroker>();
builder.Services.AddSingleton<IEventBroker, EventBroker>();
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<IStudentEventService, StudentEventServiceMassTransit>();
builder.Services.AddSingleton<ILocalStudentEventService, LocalStudentEventService>();
builder.Services.AddSingleton<ILibraryAccountService, LibraryAccountService>();
builder.Services.AddSingleton<ILibraryCardService, LibraryCardService>();
builder.Services.AddSingleton<IStudentEventOrchestrationService, StudentEventOrchestrationService>();
builder.Services.AddSingleton<ILibraryAccountOrchestrationService, LibraryAccountOrchestrationService>();
builder.Services.AddSingleton<IElasticApiBroker, ElasticApiBroker>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
((IApplicationBuilder)app).ApplicationServices.GetService<ILibraryAccountOrchestrationService>().ListenToLocalStudentEvent();
((IApplicationBuilder)app).ApplicationServices.GetService<IStudentEventOrchestrationService>().ListenToStudentEvents();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
