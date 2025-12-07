using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApplicationMVC.Models
{
    public class IndexModel
    {
        private IConfiguration _configuration;

        public List<Course> Courses = new List<Course>();

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Course> GetCourses()
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(AppContext.BaseDirectory)
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddEnvironmentVariables();
            //IConfiguration _configuration = builder.Build();

            var config = _configuration.GetSection("Common:Settings");


            string? connectionString = config.GetValue<string>("DBConnectionString");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string is null or empty.");
            }

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var sqlcommand = new SqlCommand(
            "SELECT CourseID,CourseName,Rating FROM Course;", sqlConnection);
            using (SqlDataReader sqlDatareader = sqlcommand.ExecuteReader())
            {
                while (sqlDatareader.Read())
                {
                    Courses.Add(new Course()
                    {
                        CourseID = Int32.Parse(sqlDatareader["CourseID"].ToString()),
                        CourseName = sqlDatareader["CourseName"].ToString(),
                        Rating = Decimal.Parse(sqlDatareader["Rating"].ToString())
                    });
                }
            }

            return Courses;
        }
    }
}
