// using System.Collections.Generic;
// using System.Security.Claims;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class LoginService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _repository;
        private readonly CredentialsService _credentialsService;
        public LoginService(IHttpContextAccessor httpContextAccessor, IRepository<User> repository, CredentialsService credentialsService) {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
            _credentialsService = credentialsService;
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

            // var credentials = (Credentials)await _credentialsService.GetCredentialsAsync(model);
            // ClaimsIdentity identity;
            // identity = CreateIdentity(credentials);
            // await _httpContextAccessor.HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            // var test = _httpContextAccessor.HttpContext.User.Identity;

            var result = new {
                success = true,
                user = new {
                    id = user.Id,
                    username = user.Username,
                    email = user.Email,
                    firstname = user.FirstName,
                    lastname = user.LastName,
                    dateOfBirth = user.DateOfBirth,
                    // test
                }
            };

            return result;
        }

        // public async Task SignOutAsync() {
        //     var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
        //     await _httpContextAccessor.HttpContext.SignOutAsync();
        // }

        // private static ClaimsIdentity CreateIdentity(Credentials credentials) {
        //     // adds common claims
        //     var claims = new List<Claim> {
        //         new Claim("https://localhost:4200/claims/id", credentials.User.Id.ToString()),
        //         new Claim("https://localhost:4200/claims/firstname", credentials.User.FirstName),
        //         new Claim("https://localhost:4200/claims/lastname", credentials.User.LastName),
        //         new Claim("https://localhost:4200/claims/username", credentials.User.Username),
        //         new Claim("https://localhost:4200/claims/email", credentials.User.Email)
        //     };

        //     var identity = new ClaimsIdentity(claims, "Cookies");

        //     return identity;
        // }
    }
}