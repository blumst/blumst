using Microsoft.AspNetCore.Identity;
using Moq;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Tests
{
    public class BaseServiceTests
    {
        private readonly Mock<IBaseService<Comment, CommentDto>> _serviceMock;

        public BaseServiceTests()
        {
            _serviceMock = new Mock<IBaseService<Comment, CommentDto>>();
        }

        [Fact]
        public async Task GetAllAsync_WhisEntities_ReturnsDtos()
        {
            var comment = new List<CommentDto> { new() { Description = "test comment" } };
            _serviceMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(comment);

            var result = await _serviceMock.Object.GetAllAsync(CancellationToken.None);

            Assert.Equal(comment, result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenEntityFound_ReturnsDto()
        {
            var comment = new CommentDto { Description = "test comment" };
            _serviceMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(comment);

            var result = await _serviceMock.Object.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
            
            Assert.Equal(comment, result);
        }

        [Fact]
        public async Task CreateAsync_WhenOk_MapsAndSavesEntity()
        {
            var comment = new CommentDto { Description = "test comment" };
            _serviceMock.Setup(x => x.CreateAsync(comment, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            
            await _serviceMock.Object.CreateAsync(comment, CancellationToken.None);

            _serviceMock.Verify(x => x.CreateAsync(comment, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityFound_UpdatesEntity()
        {
            var comment = new CommentDto { Description = "test comment" };
            _serviceMock.Setup(x => x.UpdateAsync(comment.Id, comment, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            await _serviceMock.Object.UpdateAsync(comment.Id, comment, CancellationToken.None);

            _serviceMock.Verify(x => x.UpdateAsync(comment.Id, comment, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityFound_DeletesEntity()
        {
            var id = Guid.NewGuid();
            _serviceMock.Setup(x => x.DeleteAsync(id, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            await _serviceMock.Object.DeleteAsync(id, CancellationToken.None);

            _serviceMock.Verify(x => x.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
