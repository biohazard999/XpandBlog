using System;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using XpandBlog.Contracts.Security;
using XpandBlog.DTO.Security;
using XpandBlog.ExpressApp.Xpo;
using XpandBlog.Web.Helpers.Xaf;
using XpandBlog.Xpo;

namespace XpandBlog.Web.Models.Identity
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context, IUnityContainer unityContainer)
        {
            var manager = new ApplicationUserManager(unityContainer.Resolve<XafUserStore>());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false,
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            //manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<User, int>
            //{
            //    MessageFormat = "Ihr Sicherheitscode ist: {0}"
            //});
            //manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<User, int>
            //{
            //    Subject = "Fairwork Sicherheitscode",
            //    BodyFormat = "Ihr Sicherheitscode ist: {0}"
            //});
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();

            //var dataProtectionProvider = options.DataProtectionProvider;
            //if (dataProtectionProvider != null)
            //{
            //    manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            //}

            return manager;
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {

            return Task.FromResult(false);
        }
    }

    public class XafUserStore : IUserStore<User, int>,
                                IUserPasswordStore<User,int>
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

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(false);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }
    }
}