using AutoMapper;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Services
{
    public class CommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(IRepository<Comment> commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentAsync(CancellationToken cancellationToken)
        {
            var comments =  await _commentRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public async Task<CommentDto> GetCommentByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetByIdAsync(id, cancellationToken);
            return comment == null ? throw new KeyNotFoundException("Comment not found") : _mapper.Map<CommentDto>(comment);
        }

        public async Task CreateCommentAsync(CommentDto commentDto, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            comment.DateCreated = DateTime.UtcNow;
            await _commentRepository.AddAsync(comment, cancellationToken);
            await _commentRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateCommentAsync(Guid id, CommentDto commentDto, CancellationToken cancellationToken)
        {
            if (id != commentDto.Id)
                throw new ArgumentException("Id not found.");

            var currentComment = await _commentRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("Comment not found.");

            _mapper.Map(commentDto, currentComment);

            _commentRepository.Update(currentComment);
            await _commentRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCommentAsync(Guid id, CancellationToken cancellationToken)
        {
            _ = await _commentRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Comment not found");

            await _commentRepository.DeleteAsync(id, cancellationToken);
            await _commentRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
