using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class UserAssignmentService {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserCourses> _repository;
        public UserAssignmentService(IHttpContextAccessor httpContextAccessor, IRepository<UserCourses> repository) {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
