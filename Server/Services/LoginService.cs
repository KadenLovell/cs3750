using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class LoginService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _repository;
        public LoginService(IHttpContextAccessor httpContextAccessor, IRepository<User> repository) {
            _httpContextAccessor = httpContextAccessor;
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

            var claims = new List<Claim> {
                new Claim("https://localhost:4200/claims/firstname", user.FirstName),
                new Claim("https://localhost:4200/claims/lastname", user.LastName),
                new Claim("https://localhost:4200/claims/email", user.Email),
                new Claim("https://localhost:4200/claims/username", user.Username),
                new Claim("https://localhost:4200/claims/id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                // TOOD: after student/teacher role is added, update value
                new Claim("https://localhost:4200/claims/role", "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            var authProperties = new AuthenticationProperties {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(6),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = false,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                RedirectUri = "https://localhost:4200/login"
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await _httpContextAccessor.HttpContext.SignInAsync("Cookies", claimsPrincipal);


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

        public async Task LogoutAsync() {
            await _httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}