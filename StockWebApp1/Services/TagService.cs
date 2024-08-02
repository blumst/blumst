using AutoMapper;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Services
{
    public class TagService
    {
        private readonly IRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public TagService(IRepository<Tag> tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagDto>> GetAllTagAsync(CancellationToken cancellationToken)
        {
            var tags = await _tagRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }

        public async Task<TagDto> GetTagByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetByIdAsync(id, cancellationToken);
            return tag == null ? throw new KeyNotFoundException("Tag not found") 
                : _mapper.Map<TagDto>(tag);
        }

        public async Task CreateTagAsync(TagDto tagDto, CancellationToken cancellationToken)
        {
            var tag = _mapper.Map<Tag>(tagDto);
            await _tagRepository.AddAsync(tag, cancellationToken);
            await _tagRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateTagAsync(Guid id, TagDto tagDto, CancellationToken cancellationToken)
        {
            if (id != tagDto.Id)
                throw new ArgumentException("Id not found.");

            var currentTag = await _tagRepository.GetByIdAsync(id, cancellationToken) 
                ?? throw new Exception("Tag not found.");

            _mapper.Map(tagDto, currentTag);

            _tagRepository.Update(currentTag);
            await _tagRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteTagAsync(Guid id, CancellationToken cancellationToken)
        {
            _ = await _tagRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Tag not found");

            await _tagRepository.DeleteAsync(id, cancellationToken);
            await _tagRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
