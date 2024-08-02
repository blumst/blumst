using AutoMapper;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Services
{
    public class ContentService
    {
        private readonly IRepository<Content> _contentRepository;
        private readonly IMapper _mapper;

        public ContentService(IRepository<Content> contentRepository, IMapper mapper)
        {
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContentDto>> GetAllContentAsync(CancellationToken cancellationToken)
        {
            var content = await _contentRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ContentDto>>(content);   
        }

        public async Task<ContentDto> GetContentByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var content = await _contentRepository.GetByIdAsync(id, cancellationToken);
            return content == null ? throw new KeyNotFoundException("Content not found") : _mapper.Map<ContentDto>(content);
        }

        public async Task CreateContentAsync(ContentDto contentDto, CancellationToken cancellationToken)
        {
            var content = _mapper.Map<Content>(contentDto);
            await _contentRepository.AddAsync(content, cancellationToken);
            await _contentRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateContentAsync(Guid id, ContentDto contentDto, CancellationToken cancellationToken)
        {
            if (id != contentDto.Id)
                throw new ArgumentException("Id not found.");

            var currentContent = await _contentRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("Content not found.");

            _mapper.Map(contentDto, currentContent);

            _contentRepository.Update(currentContent);
            await _contentRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteContentAsync(Guid id, CancellationToken cancellationToken)
        {
            _ = await _contentRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Content not found");

            await _contentRepository.DeleteAsync(id, cancellationToken);
            await _contentRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
