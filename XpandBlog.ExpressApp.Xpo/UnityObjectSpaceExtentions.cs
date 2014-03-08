using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
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
         
    }
}