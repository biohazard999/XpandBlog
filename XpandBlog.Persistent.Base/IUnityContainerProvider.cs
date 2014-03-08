using Microsoft.Practices.Unity;

namespace XpandBlog.Persistent.Base
{
    public interface IUnityContainerProvider
    {
        IUnityContainer UnityContainer { get; }
    }
}
