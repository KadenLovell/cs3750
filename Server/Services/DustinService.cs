using Server.Persistence;

namespace Server.Services {
    public class DustinService {
        private readonly DataContext _context;
        public DustinService(DataContext context) {
            _context = context;
        }
    }
}