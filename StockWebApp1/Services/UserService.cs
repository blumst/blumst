using AutoMapper;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? throw new KeyNotFoundException("User not found") 
                : _mapper.Map<UserDto>(user);
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.DateCreated = DateTime.UtcNow;

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Guid id, UserDto userDto)
        {
            if (id != userDto.Id)
                throw new ArgumentException("Id not found.");

            var currentUser = await _userRepository.GetByIdAsync(id) 
                ?? throw new Exception("User not found.");

            _mapper.Map(userDto, currentUser);

            _userRepository.Update(currentUser);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            _ = await _userRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("User not found");

            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();
        }
    }
}
