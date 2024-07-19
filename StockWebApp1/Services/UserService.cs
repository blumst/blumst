using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // ...
    }
}
