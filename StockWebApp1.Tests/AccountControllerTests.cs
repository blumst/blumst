using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StockWebApp1.Controllers;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;
using System.Security.Claims;

namespace StockWebApp1.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _controller = new AccountController(_accountServiceMock.Object);
        }

        [Fact]
        public async Task Register_WhenRegistrationIsSuccessful_ReturnsOk()
        {
            // Arrange
            var registerDto = new RegisterDto { UserName = "testUser", Password = "testPassword123" };
            _accountServiceMock.Setup(x => x.RegisterAsync(registerDto)).ReturnsAsync(IdentityResult.Success);
            
            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User Register Successfully", okResult.Value);
        }

        [Fact]
        public async Task Register_WhenRegistrationFails_ResturnsBadRequest()
        {
            var registerDto = new RegisterDto { UserName = "testUser", Password = "testPassword123" };
            _accountServiceMock.Setup(x => x.RegisterAsync(registerDto))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed" }));

            var result = await _controller.Register(registerDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("There was an error processing your request, please try again", badRequestResult.Value );
        }

        [Fact]
        public async Task Login_WhenValidToken_ReturnsOk()
        {
            var loginDto = new LoginDto { UserName = "testUser", Password = "testPassword123" };
            _accountServiceMock.Setup(x => x.LoginAsync(loginDto)).ReturnsAsync("validToken");

            var result = await _controller.Login(loginDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<LoginResponse>(okResult.Value);
            Assert.Equal("validToken", value.Token);
        }

        [Fact]
        public async Task Login_WhenLoginFails_ReturnsBadRequest()
        {
            var loginDto = new LoginDto { UserName = "testUser", Password = "testPassword123" };
            _accountServiceMock.Setup(x => x.LoginAsync(loginDto)).ReturnsAsync(string.Empty);

            var result = await _controller.Login(loginDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid login attempt", badRequestResult.Value);
        }

        [Fact]
        public async Task Logout_WhenSuccess_ReturnsOk()
        {
            var result = await _controller.Logout();

            _accountServiceMock.Verify(x => x.LogoutAsync(), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Profile_WhenUserProfile_ReturnsOk()
        {
            var user = new User { UserName = "testUser", Email = "test@gmail.com" };
            _accountServiceMock.Setup(x => x.GetProfileAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var result = await _controller.Profile();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okResult.Value);
        }
    }
}