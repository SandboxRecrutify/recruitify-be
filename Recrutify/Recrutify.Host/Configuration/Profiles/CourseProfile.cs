using Recrutify.DataAccess;

namespace Recrutify.Host.Configuration.Profiles
{
    public class CourseProfile : AutoMapper.Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>();
        }
    }
}
