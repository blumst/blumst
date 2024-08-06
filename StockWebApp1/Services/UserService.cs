using AutoMapper;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;
using StockWebApp1.Services;

namespace StockWebApp1
{
    public class UserService : BaseService<User, UserDto>
    {
        public UserService(IRepository<User> repository, IMapper mapper) 
            : base(repository, mapper) { }

        public override async Task CreateAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(userDto);

            user.DateCreated = DateTime.UtcNow; 

            await _repository.AddAsync(user, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
