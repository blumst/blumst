namespace StockWebApp1.Interfaces
{
    public interface IBaseService<TEntity, TDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<TDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task CreateAsync(TDto dto, CancellationToken cancellationToken);
        Task UpdateAsync(Guid id, TDto dto, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
