using System.Threading.Tasks;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class LoginService {
        private readonly IRepository<User> _repository;
        public LoginService(IRepository<User> repository) {
            _repository = repository;
        }
        public async Task<dynamic> LoginAsync(dynamic model) {
            var user = await _repository.GetUserByEmailAsync((string)model.email);

            if (user == null) {
                var error = new {
                    errors = new {
                        userNotFound = true
                    }
                };

                return error;
            }

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