using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.Helpers;
using Microsoft.Practices.Unity;
using XpandBlog.Persistent.Base;
using XpandBlog.Xpo;

namespace XpandBlog.ExpressApp.Xpo
{
    public static class UnityObjectSpaceExtentions
    {
        public static IUnityContainer GetUnityContainer(this IObjectSpace os)
        {
            if (os is IUnityContainerProvider)
                return (os as IUnityContainerProvider).UnityContainer;

            if (os is XPObjectSpace)
                return (os as XPObjectSpace).Session.GetUnityContainer();

            return null;
        }

        public static Session GetSession(this IObjectSpace os)
        {
            if (os is XPObjectSpace)
                return (os as XPObjectSpace).Session;
            return null;
        }

        public static UnitOfWork CreateUnitOfWork(this IObjectSpaceProvider objectSpaceProvider)
        {
            if (objectSpaceProvider is UnityXPObjectSpaceProvider)
            {
                return new UnityUnitOfWork((objectSpaceProvider as UnityXPObjectSpaceProvider).DataLayer, (objectSpaceProvider as UnityXPObjectSpaceProvider).UnityContainer);
            }

            if (objectSpaceProvider is XPObjectSpaceProvider)
            {
                return new UnitOfWork((objectSpaceProvider as XPObjectSpaceProvider).DataLayer);
            }
            return null;
        }
        public static Session CreateSession(this IObjectSpaceProvider objectSpaceProvider)
        {
            if (objectSpaceProvider is XPObjectSpaceProvider)
            {
                return new Session((objectSpaceProvider as XPObjectSpaceProvider).DataLayer);
            }
            return null;
        }


    }
}