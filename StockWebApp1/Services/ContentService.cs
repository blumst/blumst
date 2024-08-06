using AutoMapper;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Services
{
    public class ContentService : BaseService<Content, ContentDto>
    {
        public ContentService(IRepository<Content> repository, IMapper mapper)
            : base(repository, mapper) { }       
    }
}
