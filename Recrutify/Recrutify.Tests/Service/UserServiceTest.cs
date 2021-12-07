using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Host.Configuration;
using Recrutify.Services.Helpers.Abstract;
using Recrutify.Services.Services;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Tests.Service
{
    public class UserServiceTest
    {
        private IUserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;
        private IMapper _mapper;
        private Mock<IStaffHelper> _staffHelperMock;

        [SetUp]
        public void Setup()
        {
            _staffHelperMock = new Mock<IStaffHelper>();
            _mapper = MapperConfig.GetConfiguration()
                .CreateMapper();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(
                _userRepositoryMock.Object,
                _mapper,
                _staffHelperMock.Object);
        }

        [Test]
        public async Task GetNamesByIdsAsync_Success()
        {
            var ids = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            };
            var names = new Dictionary<Guid, string>()
            {
                { ids[0], "Name1" },
                { ids[1], "Name2" },
                { ids[2], "Name3" },
            };
            _userRepositoryMock
                .Setup(x => x.GetNamesByIdsAsync(ids))
                .ReturnsAsync(names);

            var result = await _userService.GetNamesByIdsAsync(ids);

            Assert.AreEqual(names.GetValueOrDefault(ids[0]), result.GetValueOrDefault(ids[0]));
            Assert.AreEqual(names.GetValueOrDefault(ids[1]), result.GetValueOrDefault(ids[1]));
            Assert.AreEqual(names.GetValueOrDefault(ids[2]), result.GetValueOrDefault(ids[2]));
        }
    }
}
