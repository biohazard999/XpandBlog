using System;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using Microsoft.AspNet.Identity;
using XpandBlog.Contracts.Security;
using XpandBlog.DTO.Security;

namespace XpandBlog.Web.Models.Identity
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store) : base(store)
        {
        }
    }

    public class XafUserStore : IUserStore<User, int>
    {
        private readonly IObjectSpaceProvider _ObjectSpaceProvider;
        private readonly IUserMapper<Model.Security.User, User> _UserMapper;

        public XafUserStore(IObjectSpaceProvider objectSpaceProvider, IUserMapper<Model.Security.User, User> userMapper)
        {
            _ObjectSpaceProvider = objectSpaceProvider;
            _UserMapper = userMapper;
        }

        public Task CreateAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> FindByNameAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            
        }
    }
}