using AutoMapper;
using Moq;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;
using StockWebApp1.Services;

namespace StockWebApp1.Tests
{
    public class BaseServiceTests
    {
        private readonly Mock<IRepository<Comment>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BaseService<Comment, CommentDto> _service;


        public BaseServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Comment>>();
            _mapperMock = new Mock<IMapper>();
            _service = new BaseService<Comment, CommentDto>(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_WhisEntities_ReturnsDtos()
        {
            var comments = new List<Comment> { new() { Description = "test comment" } };
            var commentDtos = new List<CommentDto> { new() { Description = "test comment" } };

            _repositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(comments);
            _mapperMock.Setup(x => x.Map<IEnumerable<CommentDto>>(comments)).Returns(commentDtos);

            var result = await _service.GetAllAsync(CancellationToken.None);

            Assert.Equal(commentDtos, result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenEntityFound_ReturnsDto()
        {
            var comment = new Comment { Description = "test comment"};
            var commentDto = new CommentDto { Description = "test comment" };

            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(comment);
            _mapperMock.Setup(x => x.Map<CommentDto>(comment)).Returns(commentDto);

            var result = await _service.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
            
            Assert.Equal(commentDto, result);
        }

        [Fact]
        public async Task CreateAsync_WhenOk_MapsAndSavesEntity()
        {
            var comment = new Comment { Description = "test comment" };
            var commentDto = new CommentDto { Description = "test comment" };

            _mapperMock.Setup(x => x.Map<Comment>(commentDto)).Returns(comment);
           
            await _service.CreateAsync(commentDto, CancellationToken.None);

            _repositoryMock.Verify(x => x.AddAsync(comment, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityFound_UpdatesEntity()
        {
            var id = Guid.NewGuid();
            var commentDto = new CommentDto { Id = id, Description = "updated comment" };
            var comment = new Comment { Id = commentDto.Id, Description = "original comment" };

            _repositoryMock.Setup(x => x.GetByIdAsync(commentDto.Id, It.IsAny<CancellationToken>())).ReturnsAsync(comment);

            await _service.UpdateAsync(commentDto.Id, commentDto, CancellationToken.None);

            _mapperMock.Verify(x => x.Map(commentDto, comment), Times.Once);
            _repositoryMock.Verify(x => x.Update(comment), Times.Once);
            _repositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityFound_DeletesEntity()
        {
            var id = Guid.NewGuid();
            var comment = new Comment { Id = id, Description = "test comment"};
            
            _repositoryMock.Setup(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(comment);

            await _service.DeleteAsync(id, CancellationToken.None);

            _repositoryMock.Verify(x => x.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
