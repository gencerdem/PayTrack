using AutoMapper;
using PayTrack.Application;
using PayTrack.Application.DTOs;
using PayTrack.Application.Interfaces;
using PayTrack.Domain.Entities;
using PayTrack.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrack.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync(x=>x.IsActive);
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(user is not null && user.IsActive ? user : null);
        }

        public async Task<UserDTO> CreateAsync(CreateUserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateAsync(UserDTO userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(userDto.Id);
            if (existingUser is null || existingUser.IsActive == false) 
                throw new CustomException("User not found."); 
            

            _mapper.Map(userDto, existingUser); 

            _userRepository.Update(existingUser);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDTO>(existingUser);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null || user.IsActive == false)
                throw new CustomException("User not found.");

            if (user != null)
            {
                user.IsActive = false;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
            }
        }
    }
}
