using System.Configuration;
using DevExpress.ExpressApp.Model;
using XpandBlog.Contracts.Security;
using XpandBlog.DTO.Security;


namespace XpandBlog.Domain.Security
{
    public class UserMapper : IUserMapper<Model.Security.User, User>
    {
        static UserMapper()
        {
            AutoMapper.Mapper.CreateMap<User, Model.Security.User>()
                .ForMember(m => m.Session, c => c.Ignore())
                .ForMember(m => m.Oid, c => c.MapFrom(u => u.Id));

            AutoMapper.Mapper.CreateMap<Model.Security.User, User>()
                .ForMember(m => m.Id, c => c.MapFrom(u => u.Oid));
        }

        public Model.Security.User MapUser(User @from, Model.Security.User to)
        {
            return AutoMapper.Mapper.Map(@from, to);
        }

        public User MapUser(Model.Security.User @from, User to)
        {
            return AutoMapper.Mapper.Map(@from, to);
        }
    }
}