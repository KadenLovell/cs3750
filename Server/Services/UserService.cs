using System;
using System.Threading.Tasks;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class UserService {
        private readonly IRepository<User> _repository;
        public UserService(IRepository<User> repository) {
            _repository = repository;
        }
        public async Task<dynamic> AddUserAsync(dynamic model) {
            var exists = await _repository.UserExistsByUsernameOrEmail((string)model.username, (string)model.email);

            if (exists) {
                var error = new {
                    errors = new {
                        userExists = true
                    }
                };

                return error;
            }

            var user = new User {
                Username = model.username,
                FirstName = model.firstname,
                LastName = model.lastname,
                DateOfBirth = DateTime.Parse((string)model.dateOfBirth),
                Email = model.email,
                Password = model.password,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };

            await _repository.AddAsync(user);

            var result = new {
                success = true,
                user = new {
                    id = user.Id,
                    username = user.Username,
                    email = user.Email,
                    firstname = user.FirstName,
                    lastname = user.LastName,
                    dateOfBirth = user.DateOfBirth
                }
            };

            return result;
        }
    }
}