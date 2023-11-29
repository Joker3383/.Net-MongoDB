using Lab6.Menu.Components;
using Lab6.Models;
using Lab6.Repositories.Services;
using MongoDB.Driver;

namespace Lab6.Menu
{
    public class App
    {
        private string connectionString { get; set; }
        public string databaseName { get; set; }
        public App(string connection, string databaseName)
        {
            connectionString = connection;
            this.databaseName = databaseName;
        }
        public void Run()
        {


            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);


            while (true)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("1. ||Users");
                Console.WriteLine("2. ||Workouts");
                Console.WriteLine("3. ||Logs");
                Console.WriteLine("4. ||Nutritions");
                Console.WriteLine("5. ||Exit");
                Console.WriteLine("-----------------");

                int.TryParse(Console.ReadLine(), out int result);

                switch (result)
                {
                    case 1:
                        UserComponent user = new UserComponent(new UserService(database, nameof(Users)));
                        user.Layout();
                        break;
                    case 2:
                        WorkoutsComponent workouts = new WorkoutsComponent(new WorkoutService(database, nameof(Workouts)));
                        workouts.Layout();
                        break;
                    case 3:
                        LogsComponent logs = new LogsComponent(
                            new LogsService(database, nameof(Logs)),
                            new UserService(database, nameof(Users)),
                            new WorkoutService(database, nameof(Workouts)),
                            client);
                        logs.Layout();
                        break;
                    case 4:
                        NutritionComponent nutrition = new NutritionComponent(
                            new NutritionService(database, nameof(Nutrition)),
                            new UserService(database, nameof(Users)),
                            client
                            );
                        nutrition.Layout();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Incorect inputed value,try again...");
                        break;
                }
            }
        }
    }
}
