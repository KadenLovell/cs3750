using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Persistence;

namespace Server.Services {
    public class LoginService {
        private readonly DataContext _context;
        public LoginService(DataContext context) {
            _context = context;
        }
        public async Task<dynamic> LoginAsync(dynamic model) {
            var email = (string)model.email;
            var password = (string)model.password;

            var user = await _context.User.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == password).SingleOrDefaultAsync();

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