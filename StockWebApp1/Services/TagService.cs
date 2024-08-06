using AutoMapper;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Services
{
    public class TagService : BaseService<Tag, TagDto>
    {
        public TagService(IRepository<Tag> repository, IMapper mapper) 
            : base(repository, mapper) { }

    }
}
