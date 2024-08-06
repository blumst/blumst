using AutoMapper;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Services
{
    public class CommentService : BaseService<Comment, CommentDto>
    {

        public CommentService(IRepository<Comment> repository, IMapper mapper)
            : base(repository, mapper) { }
        
        public override async Task CreateAsync(CommentDto commentDto, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(commentDto);

            comment.DateCreated = DateTime.UtcNow; 

            await _repository.AddAsync(comment, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
