using Server.Persistence;

namespace Server.Services {
    public class RyanService {
        private readonly DataContext _context;
        public RyanService(DataContext context) {
            _context = context;
        }
    }
}