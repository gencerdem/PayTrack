using PayTrack.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrack.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetByIdAsync(string id);
        Task<UserDTO> CreateAsync(CreateUserDTO user);
        Task<UserDTO> UpdateAsync(UserDTO user);
        Task DeleteAsync(string id);
    }
}
