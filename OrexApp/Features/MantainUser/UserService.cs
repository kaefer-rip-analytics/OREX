using OrexApp.Features.MantainUser.DTOs.UpdateUserRequest;
using OrexApp.Features.MantainUser.DTOs.CreateUserRequest;
using OrexApp.Features.MantainUser.DTOs.UserResponse;
using OrexApp.Features.MantainUser.User;
using OrexApp.Features.MantainUser.IUserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OrexApp.Features.MantainUser.UserService
{
    public class UserService : IUsersService
    {
        private readonly UserManager<Users> _userManager;

        public UserService(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UsersResponse>> GetAll()
        {
            var users = await _userManager.Users
                .Select(user => new UsersResponse(
                    user.Id,
                    user.Nome,
                    user.Email,
                    user.Roles,
                    user.Ativo,
                    user.DtCadastro,
                    user.DtAtualizacao)).ToListAsync();

            return users;
        }

        public async Task<UsersResponse?> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return UsersResponse.From(user);
        }

        public async Task<UsersResponse> CreateAsync(CreateUsersRequest request)
        {
            var user = new Users
            {
                UserName = request.Nome,
                Nome = request.Nome,
                Email = request.Email,
                Roles = request.Roles,
                Ativo = request.Ativo,
                DtCadastro = DateTime.UtcNow
            };
            
            await _userManager.CreateAsync(user, request.Password);

            return UsersResponse.From(user);
        }

        public async Task<UsersResponse?> UpdateAsync(string id, UpdateUsersRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
            }

            user.Nome = request.Nome;
            user.Ativo = request.Ativo;
            user.DtAtualizacao = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return UsersResponse.From(user);
        }

        public async Task<bool> DeactivatedAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                return false;
            }

            user.Ativo = false;
            user.DtAtualizacao = DateTime.UtcNow;
            
            await _userManager.UpdateAsync(user);

            return true;
        }
    }
}