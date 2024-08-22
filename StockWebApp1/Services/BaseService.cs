using AutoMapper;
using StockWebApp1.Extensions;
using StockWebApp1.Interfaces;

namespace StockWebApp1.Services
{
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        private readonly string _entityName;

        protected BaseService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _entityName = typeof(TEntity).Name;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<TDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            entity.EnsureFound($"{_entityName} not found");
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task CreateAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(dto);

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Guid id, TDto dto, CancellationToken cancellationToken)
        {
            if (!id.Equals(dto!.GetType().GetProperty("Id")?.GetValue(dto)))
                throw new ArgumentException("Id not found");

            var currentEntity = await _repository.GetByIdAsync(id, cancellationToken);
            currentEntity.EnsureFound($"{_entityName} not found");

            _mapper.Map(dto, currentEntity);

            _repository.Update(currentEntity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            entity.EnsureFound($"{_entityName} not found");

            await _repository.DeleteAsync(id, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
