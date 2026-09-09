using OrexApp.Features.MantainUser.DTOs.UpdateUserRequest;
using OrexApp.Features.MantainUser.DTOs.CreateUserRequest;
using OrexApp.Features.MantainUser.DTOs.UserResponse;

namespace OrexApp.Features.MantainUser.IUserService
{
    public interface IUsersService
    {
        Task<List<UsersResponse>> GetAll();
        Task<UsersResponse?> GetById(string id);
        Task<UsersResponse> CreateAsync(CreateUsersRequest request);
        Task<UsersResponse?> UpdateAsync(string id, UpdateUsersRequest request);
        Task<bool> DeactivatedAsync(string id);
    }
}