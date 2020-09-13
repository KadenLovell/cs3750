using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class UserService {
        private readonly DataContext _context;
        public UserService(DataContext context) {
            _context = context;
        }
        public async Task<dynamic> AddUserAsync(dynamic model) {
            var username = (string)model.username;
            var email = (string)model.email;

            var usernameExists = await _context.User.AnyAsync(x => x.Username == username);
            var emailExists = await _context.User.AnyAsync(x => x.Email == email);

            if (usernameExists) {
                var error = new {
                    errors = new {
                        usernameExists = true
                    }
                };
                return error;
            }

            if (emailExists) {
                var error = new {
                    errors = new {
                        emailExists = true
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

            _context.Entry(user).State = EntityState.Added;
            await _context.SaveChangesAsync();

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