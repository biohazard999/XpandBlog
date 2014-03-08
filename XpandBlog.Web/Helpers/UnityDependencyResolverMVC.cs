using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace XpandBlog.Web.Helpers
{
    public class UnityDependencyResolverMVC : IDependencyResolver
    {
        private const string HttpContextKey = "perRequestContainer";

        private readonly IUnityContainer _Container;

        public UnityDependencyResolverMVC(IUnityContainer container)
        {
            _Container = container;
        }

        public object GetService(Type serviceType)
        {
            if (typeof(IController).IsAssignableFrom(serviceType))
            {
                return ChildContainer.Resolve(serviceType);
            }

            return IsRegistered(serviceType) ? ChildContainer.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (IsRegistered(serviceType))
            {
                yield return ChildContainer.Resolve(serviceType);
            }

            foreach (var service in ChildContainer.ResolveAll(serviceType))
            {
                yield return service;
            }
        }

        protected IUnityContainer ChildContainer
        {
            get
            {
                var childContainer = HttpContext.Current.Items[HttpContextKey] as IUnityContainer;

                if (childContainer == null)
                {
                    HttpContext.Current.Items[HttpContextKey] = childContainer = _Container.CreateChildContainer();
                }

                return childContainer;
            }
        }

        public static void DisposeOfChildContainer()
        {
            var childContainer = HttpContext.Current.Items[HttpContextKey] as IUnityContainer;

            if (childContainer != null)
            {
                childContainer.Dispose();
            }
        }

        private bool IsRegistered(Type typeToCheck)
        {
            var isRegistered = true;

            if (typeToCheck.IsInterface || typeToCheck.IsAbstract)
            {
                isRegistered = ChildContainer.IsRegistered(typeToCheck);

                if (!isRegistered && typeToCheck.IsGenericType)
                {
                    var openGenericType = typeToCheck.GetGenericTypeDefinition();

                    isRegistered = ChildContainer.IsRegistered(openGenericType);
                }
            }

            return isRegistered;
        }
    }
}