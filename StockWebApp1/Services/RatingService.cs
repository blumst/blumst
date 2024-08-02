using AutoMapper;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;
using System.Data;

namespace StockWebApp1.Services
{
    public class RatingService
    {
        private readonly IRepository<Rating> _ratingRepository;
        private readonly IMapper _mapper;

        public RatingService(IRepository<Rating> ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RatingDto>> GetAllRatingAsync(CancellationToken cancellationToken)
        {
            var ratings = await _ratingRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<RatingDto>>(ratings);
        }

        public async Task<RatingDto> GetRatingByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var rating = await _ratingRepository.GetByIdAsync(id, cancellationToken);
            return rating == null ? throw new KeyNotFoundException("Rating not found") 
                : _mapper.Map<RatingDto>(rating);
        }

        public async Task CreateRatingAsync(RatingDto ratingDto, CancellationToken cancellationToken)
        {
            var rating = _mapper.Map<Rating>(ratingDto);
            await _ratingRepository.AddAsync(rating, cancellationToken);
            await _ratingRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRatingAsync(Guid id, RatingDto ratingDto, CancellationToken cancellationToken)
        {
            if (id != ratingDto.Id)
                throw new ArgumentException("Id not found.");

            var currentRating = await _ratingRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("Rating not found.");

            _mapper.Map(ratingDto, currentRating);

            _ratingRepository.Update(currentRating);
            await _ratingRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRatingAsync(Guid id, CancellationToken cancellationToken)
        {
            _ = await _ratingRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Rating not found");

            await _ratingRepository.DeleteAsync(id, cancellationToken);
            await _ratingRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
