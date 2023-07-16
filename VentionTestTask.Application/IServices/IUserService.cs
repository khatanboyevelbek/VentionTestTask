using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VentionTestTask.Domain.DTOs;
using VentionTestTask.Domain.Entities;

namespace VentionTestTask.Application.IServices
{
    public interface IUserService
    {
        public Task<User> AddUserAsync(CreateUserDto createUserDto);
        public Task<User> UpdateUserAsync(UpdateUserDto updateUserDto);
        public Task DeleteUserAsync(Guid userId);
        public Task<IQueryable<User>> RetrieveAllUsersAsync(Expression<Func<User, bool>> expression = null);
        public Task<User> RetrieveUserByIdAsync(Guid userId);

    }
}
