using Microsoft.AspNetCore.Mvc;
using Moq;
using StockWebApp1.Controllers;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;
using StockWebApp1.Services;


namespace StockWebApp1.Tests
{
    public class BaseControllerTests
    {
        private readonly Mock<IBaseService<Comment, CommentDto>> _serviceMock;
        private readonly BaseController<Comment, IBaseService<Comment, CommentDto>, CommentDto> _controller;

        public BaseControllerTests()
        {
            _serviceMock = new Mock<IBaseService<Comment, CommentDto>>();
            _controller = new BaseController<Comment, IBaseService<Comment, CommentDto>, CommentDto>(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_WithListOfEntities_ReturnsOk()
        {
            var comments = new List<CommentDto> { new CommentDto { Description = "test comment" } };
            _serviceMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(comments);

            var result = await _controller.GetAll(CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(comments, okResult.Value);
        }

        [Fact]
        public async Task GetById_WithEntity_ReturnsOk()
        {
            var comment = new CommentDto { Description = "test comment" };
            _serviceMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(comment);

            var result = await _controller.GetById(Guid.NewGuid(), CancellationToken.None );

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(comment, okResult.Value);
        }

        [Fact]
        public async Task Create_WithCreatedEntity_ReaturnOk()
        {
            var comment = new CommentDto { Description = "test comment" };

            var result = await _controller.Create(comment, CancellationToken.None);
            _serviceMock.Verify(x => x.CreateAsync(comment, It.IsAny<CancellationToken>()), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(comment, okResult.Value);
        }

        [Fact]
        public async Task Update_WhenOk_ReturnsNoContent()
        {
            var comment = new CommentDto { Id = Guid.NewGuid(), Description = "updated comment" };
            
            var result = await _controller.Update(comment.Id, comment, CancellationToken.None);
            _serviceMock.Verify(x => x.UpdateAsync(comment.Id, comment, It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WhenOk_ReturnsNoContent()
        {
            var id = Guid.NewGuid();

            var result = await _controller.Delete(id, CancellationToken.None);
            _serviceMock.Verify(x => x.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
