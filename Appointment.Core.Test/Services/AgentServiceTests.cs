using Appointment.Core.Dto;
using Appointment.Core.Dto.Agent;
using Appointment.Core.Services.Agent;
using Appointment.Data.Contexts;
using Appointment.Data.Models;
using Appointment.Data.Repositories.User;
using Appointment.Utils.Auth.UserInfo;
using Appointment.Utils.Constant;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Core.Test.Services;

public class AgentServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsSuccessResultWithData()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var mapperMock = new Mock<IMapper>();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestAppointmentDB")
            .Options;

        // Mock IUserInfo
        var userInfoMock = new Mock<IUserInfo>();

        using (var dbContext = new AppDbContext(options, userInfoMock.Object))
        {
            // Populate the in-memory database with test data
            dbContext.Users.AddRange(new List<Users>
            {
                new Users { Id = 1, FirstName = "Agent 1", LastName = "Bob", PhoneNumber = "089527733996", UserType = UserType.Agent },
                new Users { Id = 2, FirstName = "Agent 2", LastName = "John", PhoneNumber = "08127933015", UserType = UserType.Agent },
                // Add more test data as needed
            });
            
            dbContext.SaveChanges();

            // Simulate repository method
            userRepositoryMock.Setup(repo => repo.GetByTypeAsync(UserType.Agent))
                .ReturnsAsync(await dbContext.Users.ToListAsync());

            // Simulate mapping
            var agentData = new List<AgentResultDto>
            {
                new AgentResultDto { AgentId = 1, FullName = "Agent 1 Bob", PhoneNumber = "089527733996"},
                new AgentResultDto { AgentId = 2, FullName = "Agent 2 John", PhoneNumber = "08127933015"}
            };
            mapperMock.Setup(mapper => mapper.MapFrom<List<AgentResultDto>>(It.IsAny<List<Users>>()))
                .Returns(agentData);

            // Create an instance of AgentService using the in-memory database context
            var agentService = new AgentService(mapperMock.Object, userRepositoryMock.Object);

            // Act
            var result = await agentService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count); // Assuming we added two agents in the test data
        }
    }
}