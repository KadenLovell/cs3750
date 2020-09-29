using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class CredentialsService {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IRepository<User> _repository;

        public CredentialsService(ILoggerFactory loggerFactory, IRepository<User> repository) {
            _loggerFactory = loggerFactory;
            _repository = repository;
        }

        public async Task<Credentials> GetCredentialsAsync(dynamic model) {
            var email = (string)model.email;
            var password = (string)model.password;

            var result = await GetCredentialsAsync(email, password);

            return result;
        }

        private async Task<Credentials> GetCredentialsAsync(string email, string password) {
            var user = await _repository.GetUserByEmailAsync(email);
            var result = new Credentials(user);

            return result;
        }
    }

    public class Credentials {
        public Credentials(dynamic error) {
            Error = error;
        }
        public Credentials(User user) {
            User = user;
        }
        public User User { get; }
        public dynamic Error { get; }
    }
}