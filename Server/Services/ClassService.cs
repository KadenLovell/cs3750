using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class ClassService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Class> _repository;
        public ClassService(IHttpContextAccessor httpContextAccessor, IRepository<Class> repository) {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
    
        public async Task<dynamic> GetClassAsync(long id) {
            var classes = await _repository.GetClassById(id);

            var result = new {
                id = classes.Id,
                code = classes.Code,
                name = classes.Name,
                description = classes.Description,
                department = classes.Department,
                creditHours = classes.CreditHours,
                location = classes.Location,
                startTime = classes.StartTime?.ToString("d"),
                endTime = classes.EndTime?.ToString("d"),
                maxCapacity = classes.MaxCapacity
            };

            return result;
        }

        public async Task<dynamic> AddClassAsync(dynamic model) {
            var exists = await _repository.ClassExistsByNameOrCode((string)model.name, (string)model.code);

            if (exists) {
                var error = new {
                    errors = new {
                        classExists = true
                    }
                };

                return error;
            }

            var classes = new Class {
                Code = model.code,
                Name = model.name,
                Description = model.description,
                Department = model.department,
                CreditHours = model.creditHours,
                Location = model.location,
                StartTime = DateTime.Parse((string)model.startTime),
                EndTime = DateTime.Parse((string)model.endTime),
                MaxCapacity = model.maxCapacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };

            await _repository.AddAsync(classes);

            var result = new {
                success = true,
                classes = new {
                    id = classes.Id,
                    code = classes.Code,
                    name = classes.Name,
                    description = classes.Description,
                    department = classes.Department,
                    creditHours = classes.CreditHours,
                    location = classes.Location,
                    startTime = classes.StartTime,
                    endTime = classes.EndTime,
                    maxCapacity = classes.MaxCapacity
                }
            };

            return result;
        }

        public async Task<dynamic> UpdateClassAsync(dynamic model) {
            var classes = await _repository.GetClassById((long)model.id);

            if (classes == null) {
                return null;
            }

            classes.Code = model.code;
            classes.Name = model.Name;
            classes.Description = model.description;
            classes.Department = model.department;
            classes.CreditHours = model.creditHours;
            classes.Location = model.location;
            classes.StartTime = DateTime.Parse((string)model.startTime);
            classes.EndTime = DateTime.Parse((string)model.endTime);
            classes.MaxCapacity = model.maxCapacity;

            await _repository.UpdateAsync(classes);

            var result = new {
                success = true,
                id = classes.Id,
                code = classes.Code,
                name = classes.Name,
                description = classes.Description,
                department = classes.Department,
                creditHours = classes.CreditHours,
                location = classes.Location,
                startTime = classes.StartTime?.ToString("d"),
                endTime = classes.EndTime?.ToString("d"),
                maxCapacity = classes.MaxCapacity,
            };

            return result;
        }
    }
}