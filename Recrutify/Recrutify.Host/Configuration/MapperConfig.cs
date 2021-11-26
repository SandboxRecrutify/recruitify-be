using AutoMapper;
using AutoMapper.Configuration;
using Recrutify.Host.Configuration.Profiles;

namespace Recrutify.Host.Configuration
{
    public static class MapperConfig
    {
        public static MapperConfiguration GetConfiguration()
        {
            var configExpression = new MapperConfigurationExpression();

            configExpression.AddProfile<ProjectProfile>();
            configExpression.AddProfile<CandidateProfile>();
            configExpression.AddProfile<ScheduleProfile>();

            var config = new MapperConfiguration(configExpression);
            config.AssertConfigurationIsValid();

            return config;
        }
    }
}
