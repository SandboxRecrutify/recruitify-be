using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Host.Configuration;
using Recrutify.Services.DTOs;
using Recrutify.Services.Helpers.Abstract;
using Recrutify.Services.Services;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Tests.Service
{
    public class ProjectServiceTest
    {
        private ProjectService _projectService;
        private Mock<IProjectRepository> _projectRepositoryMock;
        private Mock<IUserService> _userServiceMock;
        private IMapper _mapper;
        private Mock<IPrimarySkillService> _primarySkillMock;
        private Mock<IStaffHelper> _staffHelperMock;

        [SetUp]
        public void Setup()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _userServiceMock = new Mock<IUserService>();
            _mapper = MapperConfig.GetConfiguration()
                .CreateMapper();
            _primarySkillMock = new Mock<IPrimarySkillService>();
            _staffHelperMock = new Mock<IStaffHelper>();

            _projectService = new ProjectService(
               _projectRepositoryMock.Object,
               _mapper,
               _userServiceMock.Object,
               _primarySkillMock.Object,
               _staffHelperMock.Object);
        }

        [Test]
        public async Task GetAsync_Success()
        {
            var project = new Project()
            {
                Name = "New",
            };
            var id = Guid.NewGuid();
            _projectRepositoryMock.Setup(x => x.GetAsync(id))
                .ReturnsAsync(project);
            var dto = new ProjectDTO()
            {
                Name = "New",
            };

            var result = await _projectService.GetAsync(id);

            Assert.AreEqual(dto.Name, result.Name);
        }
    }
}
