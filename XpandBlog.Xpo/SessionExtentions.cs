using DevExpress.Xpo;
using Microsoft.Practices.Unity;
using XpandBlog.Persistent.Base;

namespace XpandBlog.Xpo
{
    public static class SessionUnityExtentions
    {
        public static IUnityContainer GetUnityContainer(this Session session)
        {
            if (session is IUnityContainerProvider)
                return (session as IUnityContainerProvider).UnityContainer;

            return null;
        }
    }
}