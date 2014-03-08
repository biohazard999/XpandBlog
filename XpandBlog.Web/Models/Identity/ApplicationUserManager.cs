using System;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using Microsoft.AspNet.Identity;
using XpandBlog.Contracts.Security;
using XpandBlog.DTO.Security;
using XpandBlog.ExpressApp.Xpo;
using XpandBlog.Xpo;

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

        private UnitOfWork CreateUnitOfWork()
        {
            return _ObjectSpaceProvider.CreateUnitOfWork();
        }

        private IObjectSpace CreateObjectSpace()
        {
            return _ObjectSpaceProvider.CreateObjectSpace();
        }

        public Task CreateAsync(User user)
        {
            using (var uow = CreateUnitOfWork())
            {
                var dbUser = new Model.Security.User(uow);

                _UserMapper.MapUser(user, dbUser);

                uow.CommitChanges();

                user.Id = dbUser.Oid;

                return Task.FromResult<object>(null);
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (var uow = CreateUnitOfWork())
            {
                var dbUser = await uow.FindObjectByKeyAsync<Model.Security.User>(user.Id);

                _UserMapper.MapUser(dbUser, user);

                uow.CommitChanges();
            }
        }

        public async Task DeleteAsync(User user)
        {
            using (var uow = CreateUnitOfWork())
            {
                var dbUser = await uow.FindObjectByKeyAsync<Model.Security.User>(user.Id);

                uow.Delete(dbUser);
                
                uow.CommitChanges();
            }
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            using (var uow = CreateUnitOfWork())
            {
                var dbUser = await uow.FindObjectByKeyAsync<Model.Security.User>(userId);

                return _UserMapper.MapUser(dbUser, new User());
            }
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            using (var uow = CreateUnitOfWork())
            {
                var dbUser = await uow.FindObjectAsync<Model.Security.User>(new BinaryOperator("Username", userName));

                return _UserMapper.MapUser(dbUser, new User());
            }
        }

        void IDisposable.Dispose()
        {
            
        }
    }
}