using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class UserService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _repository;
        public UserService(IHttpContextAccessor httpContextAccessor, IRepository<User> repository) {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<dynamic> GetActiveUserAsync() {
            var id = _httpContextAccessor.HttpContext.User.Identity.Id();
            var user = await _repository.GetUserById(id);

            if (user == null) {
                return null;
            }

            var result = new {
                id = _httpContextAccessor.HttpContext.User.Identity.Id(),
                username = user.Username,
                firstname = user.FirstName,
                lastname = user.LastName,
                email = user.Email,
                role = user.Role
            };

            return result;
        }

        public async Task<dynamic> GetUserAsync(long id) {
            var user = await _repository.GetUserById(id);

            var result = new {
                id = user.Id,
                username = user.Username,
                email = user.Email,
                role = user.Role,
                firstName = user.FirstName,
                lastName = user.LastName,
                dateOfBirth = user.DateOfBirth?.ToString("d"),
                streetAddress = user.StreetAddress,
                city = user.City,
                state = user.State,
                zip = user.Zip,
                phone = user.Phone,
                bio = user.Bio,
                linkedInUrl = user.LinkedInUrl,
                githubUrl = user.LinkedInUrl,
                facebookUrl = user.FacebookUrl
            };

            return result;
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
                Role = model.role,
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
                    role = user.Role,
                    firstname = user.FirstName,
                    lastname = user.LastName,
                    dateOfBirth = user.DateOfBirth
                }
            };

            return result;
        }

        public async Task<dynamic> UpdateUserAsync(dynamic model) {
            var user = await _repository.GetUserById((long)model.id);

            if (user == null) {
                return null;
            }

            user.Username = model.username;
            user.Email = model.email;
            user.Role = model.role;
            user.FirstName = model.firstName;
            user.LastName = model.lastName;
            user.DateOfBirth = DateTime.Parse((string)model.dateOfBirth);
            user.StreetAddress = model.streetAddress;
            user.City = model.city;
            user.State = model.state;
            user.Zip = model.zip;
            user.Phone = model.phone;
            user.Bio = model.bio;
            user.LinkedInUrl = model.linkedInUrl;
            user.GithubUrl = model.githubUrl;
            user.FacebookUrl = model.facebookUrl;

            await _repository.UpdateAsync(user);

            var result = new {
                success = true,
                id = user.Id,
                username = user.Username,
                email = user.Email,
                role = user.Role,
                firstName = user.FirstName,
                lastName = user.LastName,
                dateOfBirth = user.DateOfBirth?.ToString("d"),
                streetAddress = user.StreetAddress,
                city = user.City,
                state = user.State,
                zip = user.Zip,
                phone = user.Phone,
                bio = user.Bio,
                linkedInUrl = user.LinkedInUrl,
                githubUrl = user.LinkedInUrl,
                facebookUrl = user.FacebookUrl
            };

            return result;
        }
    }
}