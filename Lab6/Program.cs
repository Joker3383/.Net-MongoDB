

using Lab6.Menu;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("D:/dev/Lab6/Lab6/appsettings.json")
            .Build();

string connectionString = configuration["MongoDB:ConnectionString"];
string databaseName = configuration["MongoDB:DatabaseName"];

var app = new App( connectionString, databaseName);
app.Run();