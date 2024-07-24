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

        public async Task<IEnumerable<TagDto>> GetAllTagAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }

        public async Task<TagDto> GetTagByIdAsync(Guid id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            return tag == null ? throw new KeyNotFoundException("Tag not found") 
                : _mapper.Map<TagDto>(tag);
        }

        public async Task CreateTagAsync(TagDto tagDto)
        {
            var tag = _mapper.Map<Tag>(tagDto);
            await _tagRepository.AddAsync(tag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task UpdateTagAsync(Guid id, TagDto tagDto)
        {
            if (id != tagDto.Id)
                throw new ArgumentException("Id not found.");

            var currentTag = await _tagRepository.GetByIdAsync(id) 
                ?? throw new Exception("Tag not found.");

            _mapper.Map(tagDto, currentTag);

            _tagRepository.Update(currentTag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(Guid id)
        {
            _ = await _tagRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Tag not found");

            await _tagRepository.DeleteAsync(id);
            await _tagRepository.SaveChangesAsync();
        }
    }
}
