using System.Reflection;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using XpandBlog.Contracts.Security;
using XpandBlog.Domain.Security;
using XpandBlog.DTO.Security;
using XpandBlog.Web.Helpers;
using XpandBlog.Web.Helpers.Xaf;

namespace XpandBlog.Web
{
    public static class BootstrapperMVC
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolverMVC(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterInstance<IXafHelper>(new XafHelper(unityContainer),new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IUserMapper<Model.Security.User, User>, UserMapper>();

            return unityContainer;
        }
    }
}