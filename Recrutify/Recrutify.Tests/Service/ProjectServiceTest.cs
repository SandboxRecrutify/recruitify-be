using AutoMapper;
using Moq;
using NUnit.Framework;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Helpers.Abstract;
using Recrutify.Services.Services;
using Recrutify.Services.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Tests.Service
{
    public class ProjectServiceTest
    {
        private ProjectService _projectService;
        private Mock<IProjectRepository> _projectRepositoryMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IPrimarySkillService> _primarySkillMock;
        private Mock<IStaffHelper> _staffHelperMock;

        [SetUp]
        public void Setup()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();
            _primarySkillMock = new Mock<IPrimarySkillService>();
            _staffHelperMock = new Mock<IStaffHelper>();

            _projectService = new ProjectService(
               _projectRepositoryMock.Object,
               _mapperMock.Object,
               _userServiceMock.Object,
               _primarySkillMock.Object,
               _staffHelperMock.Object);
        }

        [Test]
        [TestCase(8, "New")]
        public void GetTestAsync(Guid id, ProjectDTO expectProject)
        {
            var project = new Project()
            {
                Name = "New",
            };
            _projectRepositoryMock.Setup(x => x.GetAsync(id).Result)
                .Returns(project);
            var dto = new ProjectDTO();
            var actualResult = _mapperMock.Setup(x => x.Map(dto, project));
            Assert.AreEqual(expectProject, actualResult);
        }

    }
}
