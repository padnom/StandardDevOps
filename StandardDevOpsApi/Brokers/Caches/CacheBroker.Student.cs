using System.Text.Json;

using Elastic.Clients.Elasticsearch;

using MassTransit;

using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Brokers.Caches
{
    public partial class CacheBroker
    {
        public async ValueTask<Student> DeleteStudentAsync(Student student)
        {
            await this.database.KeyDeleteAsync(student.Id.ToString());
            return student;

        }

        public async ValueTask<Student> InsertStudentAsync(Student student)
        {
            return await UpdateStudentAsync(student);
        }

        public async ValueTask<IEnumerable<Student>> SelectAllStudentsAsync()
        {

            var server = GetServer();
            var data = server.Keys();
            var studentIds = data?.Select(k => k.ToString());
            List<Student> students = new ();

            foreach (var guid in studentIds)
            {
                Student student = await SelectStudentByIdAsync(guid);
                students.Add(student);
            }
            return students;

        }

        public async ValueTask<Student> SelectStudentByIdAsync(string studentId)
        {
            var data = await this.database.StringGetAsync(studentId);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<Student>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async ValueTask<Student> UpdateStudentAsync(Student student)
        {
            await this.database.StringSetAsync(student.Id.ToString(), JsonSerializer.Serialize(student));
            return await SelectStudentByIdAsync(student.Id.ToString());
        }
    }
}