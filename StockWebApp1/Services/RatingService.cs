using AutoMapper;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Services
{
    public class RatingService : BaseService<Rating, RatingDto>
    {
        public RatingService(IRepository<Rating> repository, IMapper mapper)
            : base(repository, mapper) { }
    }
}
