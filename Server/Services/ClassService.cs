using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
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

        public async Task<dynamic> GetClassesAsync() {
            var classes = await _repository.GetClassesAsync();
            if (classes == null || classes.Count == 0) {
                return null;
            }

            var result = new List<dynamic>();

            foreach (var obj in classes) {
                result.Add(new {
                    obj.Id,
                    obj.Code,
                    obj.Description
                });
            }

            return result;
        }

        public async Task<dynamic> GetClassAsync(long id) {
            var classes = await _repository.GetClassByIdAsync(id);

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
                capacity = classes.Capacity
            };

            return result;
        }

        public async Task<dynamic> AddClassAsync(dynamic model) {
            // var exists = await _repository.ClassExistsByNameOrCodeAsync((string)model.name, (string)model.code);

            // if (exists) {
            //     var error = new {
            //         errors = new {
            //             classExists = true
            //         }
            //     };

            //     return error;
            // }

            var obj = new Class {
                Code = model.code,
                Name = model.name,
                Description = model.description,
                Department = model.department,
                CreditHours = model.creditHours,
                Location = model.location,
                StartTime = null, // DateTime.Parse((string)model.startTime),
                EndTime = null, // DateTime.Parse((string)model.endTime),
                Capacity = model.capacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };

            await _repository.AddAsync(obj);

            var result = new {
                success = true,
                classes = new {
                    id = obj.Id,
                    code = obj.Code,
                    name = obj.Name,
                    description = obj.Description,
                    department = obj.Department,
                    creditHours = obj.CreditHours,
                    location = obj.Location,
                    capacity = obj.Capacity
                }
            };

            return result;
        }

        public async Task<dynamic> UpdateClassAsync(dynamic model) {
            var obj = await _repository.GetClassByIdAsync((long)model.id);

            if (obj == null) {
                return null;
            }

            obj.Code = model.code;
            obj.Name = model.Name;
            obj.Description = model.description;
            obj.Department = model.department;
            obj.CreditHours = model.creditHours;
            obj.Location = model.location;
            obj.StartTime = DateTime.Parse((string)model.startTime);
            obj.EndTime = DateTime.Parse((string)model.endTime);
            obj.Capacity = model.capacity;

            await _repository.UpdateAsync(obj);

            var result = new {
                success = true,
                id = obj.Id,
                code = obj.Code,
                name = obj.Name,
                description = obj.Description,
                department = obj.Department,
                creditHours = obj.CreditHours,
                location = obj.Location,
                startTime = obj.StartTime?.ToString("d"),
                endTime = obj.EndTime?.ToString("d"),
                capacity = obj.Capacity,
            };

            return result;
        }
    }
}