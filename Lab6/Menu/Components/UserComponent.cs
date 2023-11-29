using Lab6.Models;
using Lab6.Repositories.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lab6.Menu.Components
{
    public class UserComponent
    {
        private readonly UserService _userService;

        public UserComponent(UserService userService)
        {
            _userService = userService;
        }
        public void Layout()
        {
            while (true)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("1. <Add One User>");
                Console.WriteLine("2. <Add Users>");
                Console.WriteLine("3. <Update User>");
                Console.WriteLine("4. <Delete One User>");
                Console.WriteLine("5. <Delete  Users>");
                Console.WriteLine("6. <Get Users>");
                Console.WriteLine("7. <Get By Id>");
                Console.WriteLine("8. <Create Index for User>");
                Console.WriteLine("9. <Get Indexes for User>");
                Console.WriteLine("10. <Exit>");
                Console.WriteLine("-----------------");

                int.TryParse(Console.ReadLine(), out int result);

                switch (result)
                {
                    case 1:
                        {
                            Console.WriteLine("Input a Name:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Input a Email:");
                            string email = Console.ReadLine();
                            Console.WriteLine("Input your current weight:");
                            float.TryParse(Console.ReadLine(), out float weight);

                            _userService.CreateOne(new Users()
                            {
                                Id = ObjectId.GenerateNewId(),
                                Name = name,
                                Email = email,
                                Weight = weight
                            });

                        }
                        break;
                    case 2:
                        {
                            var users = new List<Users>();
                            Console.WriteLine("Input count of users that you want to Add");
                            int.TryParse(Console.ReadLine(), out int count);

                            for (; count > 0; count--)
                            {
                                Console.WriteLine("Input a Name:");
                                string name = Console.ReadLine();
                                Console.WriteLine("Input a Email:");
                                string email = Console.ReadLine();
                                Console.WriteLine("Input your current weight:");
                                float.TryParse(Console.ReadLine(), out float weight);

                                var user = new Users()
                                {
                                    Id = ObjectId.GenerateNewId(),
                                    Name = name,
                                    Email = email,
                                    Weight = weight
                                };

                                users.Add(user);
                            }
                            var quantity = _userService.CreateMany(users);
                            Console.WriteLine($"You added {quantity}'(s) users");
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

                            _userService.Update(
                                Builders<Users>.Filter.Eq(parameterSearch, valueSearch),
                                Builders<Users>.Update.Set(parameterUpdate, valueUpdate)
                                );
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("Specify the parameter by which to delete: ");
                            var parameterDelete = Console.ReadLine();
                            Console.WriteLine("Specify the value of the parameter by which the deletion will be performed:");
                            var valueDelete = Console.ReadLine();

                            _userService.DeleteOne(Builders<Users>.Filter.Eq(parameterDelete, valueDelete));
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("Specify the parameter by which to delete: ");
                            var parameterDelete = Console.ReadLine();
                            Console.WriteLine("Specify the value of the parameter by which the deletion will be performed:");
                            var valueDelete = Console.ReadLine();

                            _userService.DeleteMany(Builders<Users>.Filter.Eq(parameterDelete, valueDelete));
                        }
                        break;
                    case 6:
                        {
                            var users = _userService.GetUsers();
                            foreach (var user in users)
                            {
                                Console.WriteLine("-----------------");
                                Console.WriteLine(user.Name + '\n' + user.Email + '\n' + user.Weight);
                            }
                        }
                        break;
                    case 7:
                        {
                            var id = ObjectId.Parse(Console.ReadLine());
                            var user = _userService.GetUserById(id);
                            Console.WriteLine
                                ($"|| User:" + $"{user.Name} \t" + $"{user.Email} \t" + $"{user.Weight} \t" + $"");
                        }
                        break;
                    case 8:
                        {
                            var indexes = _userService.GetIndexes();
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

                            var indexKeysDefinition = Builders<Users>.IndexKeys.Ascending(parameter);

                            _userService.CreateIndex(new CreateIndexModel<Users>(indexKeysDefinition));

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
