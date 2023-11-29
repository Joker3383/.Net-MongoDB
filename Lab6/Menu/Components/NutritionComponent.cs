using Lab6.Models;
using Lab6.Repositories.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lab6.Menu.Components
{
    public class NutritionComponent
    {
        private readonly NutritionService _nutritionService;
        private readonly UserService _userService;
        private readonly IMongoClient _client;
        public NutritionComponent(NutritionService nutritionService, UserService userService,IMongoClient client)
        {
            _nutritionService = nutritionService;
            _userService = userService;
            _client = client;
        }
        public void Layout()
        {
            while (true)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("1. <Add Nutrition>");
                Console.WriteLine("2. <Update Nutrition>");
                Console.WriteLine("3. <Delete Nutrition>");
                Console.WriteLine("4. <Get Nutrition>");
                Console.WriteLine("5. <Get Nutrition By UserId>");
                Console.WriteLine("6. <Create Index for Nutrition>");
                Console.WriteLine("7. <Get Indexes for Nutrition>");
                Console.WriteLine("8. <Exit>");
                Console.WriteLine("-----------------");

                int.TryParse(Console.ReadLine(), out int result);

                switch (result)
                {
                    case 1:
                        {
                            Console.WriteLine("Input your ID:");

                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId Id)))
                            {
                                Console.WriteLine("Input valid ID");
                                break;
                            }

                            var existNutrition = _nutritionService.GetNutritionById(Id);
                            if(existNutrition != null) { Console.WriteLine("For this user nitrition is already exist."); break; }


                            Console.WriteLine("Input your count of intake:");
                            int.TryParse(Console.ReadLine(), out int intake);
                            Console.WriteLine("Input your current weight:");
                            int.TryParse(Console.ReadLine(), out int calories);

                            var nutrition = new Nutrition()
                            {
                                Id = ObjectId.GenerateNewId(),
                                UserId = Id,
                                Intake = intake,
                                CalorieCountAtDay = calories
                            };
                            _nutritionService.CreateOne(nutrition);
                        }
                        break;
                    case 2:
                        {
                            using var session = _client.StartSession();

                            session.StartTransaction();
                            try
                            {
                                Console.WriteLine("Input your ID:");

                                if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId UserId)))
                                {
                                    Console.WriteLine("Input valid ID");
                                    break;
                                }

                                Console.WriteLine("Specify the parameter to be updated: ");
                                var parameterUpdate = Console.ReadLine();
                                Console.WriteLine("Specify the value of the parameter to be updated");
                                var valueUpdate = Console.ReadLine();

                                _nutritionService.Update(
                                    Builders<Nutrition>.Filter.Eq(nameof(UserId), UserId),
                                    Builders<Nutrition>.Update.Set(parameterUpdate, valueUpdate));

                                var user = _userService.GetUserById(UserId);
                                if (user != null)
                                {

                                    user.Weight += 0.5f;
                                    _userService.Update(Builders<Users>.Filter.Eq(nameof(user.Id), UserId), Builders<Users>.Update.Set(u => u.Weight, user.Weight));
                                }
                                else
                                {
                                    Console.WriteLine("User haven`t nutrition.");
                                }
                                session.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error transaction: " + ex.Message);
                                session.AbortTransaction();
                            }
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("Input your ID:");

                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId Id)))
                            {
                                Console.WriteLine("Input valid ID");
                                break;
                            }

                            var existNutrition = _nutritionService.GetNutritionById(Id);
                            _nutritionService.DeleteOne(existNutrition);
                        }
                        break;
                    case 4:
                        {
                            var nutritions = _nutritionService.GetNutritions();
                            var users = _userService.GetUsers();

                            var lookup = (from n in nutritions.AsQueryable()
                                          join u in users.AsQueryable() on n.UserId equals u.Id
                                          select new
                                          {
                                              Name = u.Name,
                                              Email = u.Email,
                                              Intake = n.Intake,
                                              Calories = n.CalorieCountAtDay,
                                              Weight = u.Weight

                                          }).ToList();
                            foreach( var item in lookup )
                            {
                                Console.WriteLine(
                                    item.Name + '\n' +
                                    item.Email + '\n' +
                                    item.Weight + '\n' +
                                    item.Calories + '\n'+
                                    item.Intake + '\n');
                            }
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("Input your ID:");

                            if (!(ObjectId.TryParse(Console.ReadLine(), out ObjectId Id)))
                            {
                                Console.WriteLine("Input valid ID");
                                break;
                            }

                            var nutrition = _nutritionService.GetNutritionById(Id);
                            var user = _userService.GetUserById(Id);

                            if(nutrition != null && user !=null ) 
                            {
                                Console.WriteLine(user.Name + " " + user.Email + " " + user.Weight + " " + nutrition.Intake + " " + nutrition.CalorieCountAtDay);
                            }
                            else { Console.WriteLine("For this user nutrition isn`t exist"); }
                        }
                        break;
                    case 6:
                        {
                            Console.WriteLine("Input a parameter for index: ");
                            var parameter = Console.ReadLine();

                            var indexKeysDefinition = Builders<Nutrition>.IndexKeys.Ascending(parameter);

                            _nutritionService.CreateIndex(new CreateIndexModel<Nutrition>(indexKeysDefinition));

                        }
                        break;
                    case 7:
                        {
                            var indexes = _nutritionService.GetIndexes();
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
