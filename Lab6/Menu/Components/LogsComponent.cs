using Lab6.Models;
using Lab6.Repositories.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace Lab6.Menu.Components
{
    public class LogsComponent
    {
        private readonly LogsService _logsService;
        private readonly UserService _userService;
        private readonly WorkoutService _workoutService;
        private readonly IMongoClient _client;

        public LogsComponent(LogsService logsService, UserService userService, WorkoutService workoutService, IMongoClient client)
        {
            _logsService = logsService;
            _userService = userService;
            _workoutService = workoutService;
            _client = client;
        }
        public void Layout()
        {
            while (true)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("1. <Add  Log>");
                Console.WriteLine("2. <Update Logs>");
                Console.WriteLine("3. <Delete  Logs>");
                Console.WriteLine("4. <Get Logs>");
                Console.WriteLine("5. <Get Log By Id>");
                Console.WriteLine("6. <Create Index for Logs>");
                Console.WriteLine("7. <Get Indexes for Logs>");
                Console.WriteLine("8. <Exit>");
                Console.WriteLine("-----------------");

                int.TryParse(Console.ReadLine(), out int result);

                switch (result)
                {
                    case 1:
                        {
                            Console.WriteLine("Input your ID:");

                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId UserId)))
                            {
                                Console.WriteLine("Input valid ID");
                                break;
                            }

                            var user = _userService.GetUserById(UserId);
                            if(user == null)
                            {
                                Console.WriteLine("Input exist UserId");
                                break;
                            }

                            Console.WriteLine("Input your workoutID:");
                            
                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId WorkoutId)))
                            {
                                Console.WriteLine("Input valid workoutID");
                                break;
                            }

                            var workout = _workoutService.GetWorkoutsById(WorkoutId);
                            if (workout == null)
                            {
                                Console.WriteLine("Input exist UserId");
                                break;
                            }

                            Console.WriteLine("Specify the number of workouts per month:");
                            var countFlag = int.TryParse(Console.ReadLine(),out int count);
                            Console.WriteLine("Specify the number of workouts per month:");
                            var dateFlag = DateOnly.TryParse(Console.ReadLine(), out DateOnly date);

                            if(countFlag && dateFlag)
                            {
                                //using var session = _client.StartSession();

                                //session.StartTransaction();
                                //try
                                //{
                                    var log = new Logs()
                                    {
                                        Id = ObjectId.GenerateNewId(),
                                        UserId = UserId,
                                        WorkoutId = WorkoutId,
                                        Date = date,
                                        CountOfMonth = count
                                    };
                                    _logsService.CreateOne(log);

                                    if (count > 8)
                                    {
                                        float weight = user.Weight - 0.7f;
                                        _userService.Update(
                                    Builders<Users>.Filter.Eq(nameof(user.Id), UserId),
                                    Builders<Users>.Update.Set(nameof(user.Weight), weight)
                                    );
                                    }
                                    //session.CommitTransaction();
                                //}
                                //catch (Exception ex)
                                //{
                                //    Console.WriteLine("Error transaction: " + ex.Message);
                                //    session.AbortTransactionAsync();
                                //}
                            }

                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine("Input your ID:");

                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId UserId)))
                            {
                                Console.WriteLine("Input valid ID");
                                break;
                            }

                            var userLog = _logsService.GetLogByUserId(UserId);
                            if (userLog == null)
                            {
                                Console.WriteLine("Input exist UserId");
                                break;
                            }

                            Console.WriteLine("Input your workoutID:");

                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId WorkoutId)))
                            {
                                Console.WriteLine("Input valid workoutID");
                                break;
                            }

                            var workoutLog = _logsService.GetLogByWorkoutId(WorkoutId);
                            if (workoutLog == null)
                            {
                                Console.WriteLine("Input exist UserId");
                                break;
                            }

                            Console.WriteLine("Specify the parameter to be updated: ");
                            var parameterUpdate = Console.ReadLine();
                            Console.WriteLine("Specify the value of the parameter to be updated");
                            var valueUpdate = Console.ReadLine();

                            var filter = Builders<Logs>.Filter.And(
                                Builders<Logs>.Filter.Eq(nameof(UserId), UserId),
                                Builders<Logs>.Filter.Eq(nameof(WorkoutId), WorkoutId));

                            _logsService.Update(
                                filter,
                                Builders<Logs>.Update.Set(parameterUpdate, valueUpdate));


                            var user = _userService.GetUserById(UserId);
                            if(int.Parse(valueUpdate) > 8)
                            {
                                float weight = user.Weight - 0.7f;
                                _userService.Update(
                                Builders<Users>.Filter.Eq(nameof(user.Id), UserId),
                                Builders<Users>.Update.Set(nameof(user.Weight), weight)
                                  );
                            }

                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("Input your ID:");

                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId UserId)))
                            {
                                Console.WriteLine("Input valid ID");
                                break;
                            }

                            var userLog = _logsService.GetLogByUserId(UserId);
                            if (userLog == null)
                            {
                                Console.WriteLine("Input exist UserId");
                                break;
                            }

                            Console.WriteLine("Input your workoutID:");

                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId WorkoutId)))
                            {
                                Console.WriteLine("Input valid workoutID");
                                break;
                            }

                            var workoutLog = _logsService.GetLogByWorkoutId(WorkoutId);
                            if (workoutLog == null)
                            {
                                Console.WriteLine("Input exist UserId");
                                break;
                            }


                            var filter = Builders<Logs>.Filter.And(
                                Builders<Logs>.Filter.Eq(nameof(UserId), UserId),
                                Builders<Logs>.Filter.Eq(nameof(WorkoutId), WorkoutId));

                            _logsService.DeleteOne(filter);
                               

                        }
                        break;
                    case 4:
                        {
                            var users = _userService.GetUsers();
                            var workout = _workoutService.GetWorkouts();
                            var logs = _logsService.GetLogs();

                            var lookup = (from l in logs.AsQueryable()
                                         join w in workout.AsQueryable() on l.WorkoutId equals w.Id
                                         join u in users.AsQueryable() on l.UserId equals u.Id
                                         select new
                                         {
                                             Name = u.Name,
                                             Email = u.Email,
                                             Weight = u.Weight,
                                             Dificulty = w.Dificulty,
                                             Duration = w.Duration,
                                             Count = l.CountOfMonth
                                         }).ToList();

                            foreach (var item in lookup)
                            {
                                Console.WriteLine(
                                    item.Name + '\n' +
                                    item.Email + '\n' +
                                    item.Weight + '\n' +
                                    item.Dificulty + '\n' +
                                    item.Duration + '\n' +
                                    item.Count + '\n');
                            }
                        }
                        break;
                    case 5:
                        {

                            Console.WriteLine("Input your ID:");

                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId UserId)))
                            {
                                Console.WriteLine("Input valid ID");
                                break;
                            }
                            Console.WriteLine("Input your workoutID:");

                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId WorkoutId)))
                            {
                                Console.WriteLine("Input valid workoutID");
                                break;
                            }

                            var log = _logsService.GetLogByUserIdAndWorkoutId(UserId, WorkoutId);
                            

                            if(log != null) 
                            { 
                                var user = _userService.GetUserById(log.UserId);
                                var workout = _workoutService.GetWorkoutsById(log.WorkoutId);

                                
                                 Console.WriteLine(user.Name + " " + user.Email + " " + user.Weight + " " + workout.Dificulty + " " + workout.Duration + " " + log.CountOfMonth );
                                
                            }
                        }
                        break;
                    case 6:
                        {
                            Console.WriteLine("Input a parameter for index: ");
                            var parameter = Console.ReadLine();

                            var indexKeysDefinition = Builders<Logs>.IndexKeys.Ascending(parameter);

                            _logsService.CreateIndex(new CreateIndexModel<Logs>(indexKeysDefinition));

                        }
                        break;
                    case 7:
                        {
                            var indexes = _logsService.GetIndexes();
                            foreach (var index in indexes)
                            {
                                Console.WriteLine(index);
                            }
                        }
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("Incorect inputed value,try again...");
                        break;

                }
            }
        }
    }
}
