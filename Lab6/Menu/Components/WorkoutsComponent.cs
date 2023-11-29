using Lab6.Models;
using Lab6.Repositories.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lab6.Menu.Components
{
    public class WorkoutsComponent
    {
        private readonly WorkoutService _workoutsService;

        public WorkoutsComponent(WorkoutService workoutsService)
        {
            _workoutsService = workoutsService;
        }
        public void Layout()
        {
            while (true)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("1. <Add One Workouts>");
                Console.WriteLine("2. <Add Workouts>");
                Console.WriteLine("3. <Update Workouts>");
                Console.WriteLine("4. <Delete One Workouts>");
                Console.WriteLine("5. <Delete  Workouts>");
                Console.WriteLine("6. <Get Workouts>");
                Console.WriteLine("7. <Get Workouts By Id>");
                Console.WriteLine("8. <Create Index for Workouts>");
                Console.WriteLine("9. <Get Indexes for Workouts>");
                Console.WriteLine("10. <Exit>");
                Console.WriteLine("-----------------");

                int.TryParse(Console.ReadLine(), out int result);

                switch (result)
                {
                    case 1:
                        {
                            Console.WriteLine("Input a Name:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Input a description:");
                            string description = Console.ReadLine();
                            Console.WriteLine("Input a duration:");
                            string duration = Console.ReadLine();
                            Console.WriteLine("Input a dificulty:");
                            string dificulty = Console.ReadLine();

                            _workoutsService.CreateOne(new Workouts()
                            {
                                Id = ObjectId.GenerateNewId(),
                                Name = name,
                                Description = description,
                                Duration = duration,
                                Dificulty = dificulty
                            });
                        }
                        break;
                    case 2:
                        {
                            var workouts = new List<Workouts>();
                            Console.WriteLine("Input count of users that you want to Add");
                            int.TryParse(Console.ReadLine(), out int count);

                            for (; count > 0; count--)
                            {
                                Console.WriteLine("Input a Name:");
                                string name = Console.ReadLine();
                                Console.WriteLine("Input a description:");
                                string description = Console.ReadLine();
                                Console.WriteLine("Input a duration:");
                                string duration = Console.ReadLine();
                                Console.WriteLine("Input a dificulty:");
                                string dificulty = Console.ReadLine();

                                var workout = new Workouts()
                                {
                                    Id = ObjectId.GenerateNewId(),
                                    Name = name,
                                    Description = description,
                                    Duration = duration,
                                    Dificulty = dificulty
                                };

                                workouts.Add(workout);
                            }
                            var quantity = _workoutsService.CreateMany(workouts);
                            Console.WriteLine($"You added {quantity}'(s) workouts");
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("Specify the parameter for which the update will be performed: ");
                            var parameterSearch = Console.ReadLine();
                            Console.WriteLine("Specify the value of the parameter by which the update will be performed");
                            var valueSearch = Console.ReadLine();

                            Console.WriteLine("Specify the parameter to be updated: ");
                            var parameterUpdate = Console.ReadLine();
                            Console.WriteLine("Specify the value of the parameter to be updated");
                            var valueUpdate = Console.ReadLine();

                            _workoutsService.Update(
                                Builders<Workouts>.Filter.Eq(parameterSearch, valueSearch),
                                Builders<Workouts>.Update.Set(parameterUpdate, valueUpdate)
                                );
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("Specify the parameter by which to delete: ");
                            var parameterDelete = Console.ReadLine();
                            Console.WriteLine("Specify the value of the parameter by which the deletion will be performed:");
                            var valueDelete = Console.ReadLine();

                            _workoutsService.DeleteOne(Builders<Workouts>.Filter.Eq(parameterDelete, valueDelete));
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("Specify the parameter by which to delete: ");
                            var parameterDelete = Console.ReadLine();
                            Console.WriteLine("Specify the value of the parameter by which the deletion will be performed:");
                            var valueDelete = Console.ReadLine();

                            _workoutsService.DeleteMany(Builders<Workouts>.Filter.Eq(parameterDelete, valueDelete));
                        }
                        break;
                    case 6:
                        {
                            var workouts = _workoutsService.GetWorkouts();
                            foreach (var workout in workouts)
                            {
                                Console.WriteLine("-----------------");
                                Console.WriteLine(workout.Name + '\n' + workout.Description + '\n' + workout.Duration + workout.Dificulty + '\n');
                            }
                        }
                        break;
                    case 7:
                        {
                            var id = ObjectId.Parse(Console.ReadLine());
                            var workout = _workoutsService.GetWorkoutsById(id);
                            Console.WriteLine
                                ($"|| Workout:" + $"{workout.Name} \t" + $"{workout.Description} \t" + $"{workout.Duration} \t" + $"{workout.Dificulty} \t");

                        }
                        break;
                    case 8:
                        {
                            var indexes = _workoutsService.GetIndexes();
                            foreach (var index in indexes)
                            {
                                Console.WriteLine(index);
                            }
                        }
                        break;
                    case 9:
                        {
                            Console.WriteLine("Input a parameter for index: ");
                            var parameter = Console.ReadLine();

                            var indexKeysDefinition = Builders<Workouts>.IndexKeys.Ascending(parameter);

                            _workoutsService.CreateIndex(new CreateIndexModel<Workouts>(indexKeysDefinition));
                        }
                        break;
                    case 10:
                        return;
                    default:
                        Console.WriteLine("Incorect inputed value,try again...");
                        break;

                }
            }
        }
    }
}
