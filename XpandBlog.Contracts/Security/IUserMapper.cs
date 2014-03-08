using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpandBlog.Contracts.Security
{
    public interface IUserMapper<TUser, TUserPoco> : IMapper<IUser, TUser, TUserPoco>
        where TUserPoco : IUser
        where TUser : IUser
    {
    }
}
