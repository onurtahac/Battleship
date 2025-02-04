using AutoMapper;
using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Application.Interfaces;
using BattleshipAPI.Domain.Entites;
using BattleshipAPI.Domain.Entities;
using BattleshipAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Application.Services
{
    public class UserAppService : IUserAppService
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserAppService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<bool> AddUserAsync(UserDTO user)
        {
            try
            {
                var userEntity = _mapper.Map<User>(user);
                return await _userService.AddUserAsync(userEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                return await _userService.DeleteUserAsync(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                return false;
            }
        }

        public async Task<UserDTO?> GetByUserNameAsync(UserDTO userDto)
        {
            try
            {
                // UserService içindeki GetUserByUserNameAsync metodunu çağır
                var user = await _userService.GetUserByUserNameAsync(userDto.Name);

                // Eğer kullanıcı yoksa null döndür
                if (user == null)
                {
                    return null;
                }

                // User -> UserDTO dönüşümü yap ve döndür
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user by username: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return _mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving users: {ex.Message}");
                return Enumerable.Empty<UserDTO>();
            }
        }

        public async Task<UserDTO> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                var user = await _userService.GetUserByEmailAndPasswordAsync(email, password);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user by email and password: {ex.Message}");
                return null;
            }
        }




        public async Task<UserDTO> GetUserByEmailIdAsync(string email)
        {
            // UserService'ten User nesnesi alınıyor
            var user = await _userService.GetUserByEmailIdAsync(email);

            // Eğer kullanıcı bulunduysa, AutoMapper ile User nesnesini UserDTO'ya dönüştürüyoruz
            if (user != null)
            {
                return _mapper.Map<UserDTO>(user); // User nesnesini UserDTO'ya dönüştürme
            }

            // Eğer kullanıcı bulunamazsa null döndürüyoruz
            return null;
        }

        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user by ID: {ex.Message}");
                return null;
            }
        }

        


        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userService.GetUserByEmailAndPasswordAsync(email, password);
                if (user != null)
                {
                    return $"Welcome {user.Name}!";
                }
                return "Invalid email or password.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging in: {ex.Message}");
                return "An error occurred during login.";
            }
        }

        public async Task<bool> RegisterAsync(string name, string surname, string email, string password)
        {
            try
            {
                var user = new User
                {
                    Name = name,
                    Surname = surname,
                    Email = email,
                    PasswordHash = password
                };
                return await _userService.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering user: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(UserDTO user)
        {
            try
            {
                var userEntity = _mapper.Map<User>(user);
                return await _userService.UpdateUserAsync(userEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                return false;
            }
        }
    }
}
