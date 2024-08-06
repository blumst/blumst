using Microsoft.AspNetCore.Mvc;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    public class BaseController<TEntity, TService, TDto> : ControllerBase 
        where TService : BaseService<TEntity, TDto>
    {
        protected readonly TService _service;

        public BaseController(TService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TDto dto, CancellationToken cancellationToken)
        {
            await _service.CreateAsync(dto, cancellationToken);
            return Ok(dto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TDto dto, CancellationToken cancellationToken)
        {
            await _service.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
