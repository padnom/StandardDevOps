﻿using System;
using System.Threading.Tasks;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Brokers.Events
{
    public partial class EventBroker
    {
        private static Func<Student, ValueTask<Student>> StudentEventHandler;

        public void ListenToStudentEvent(Func<Student, ValueTask<Student>> studentEventHandler) =>
            StudentEventHandler = studentEventHandler;

        public async ValueTask PublishStudentEventAsync(Student student) =>
            await StudentEventHandler(student);
    }
}
