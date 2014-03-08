using DevExpress.Xpo;
using Microsoft.Practices.Unity;
using XpandBlog.Persistent.Base;

namespace XpandBlog.Xpo
{
    public class NestedUnityUnitOfWork : NestedUnitOfWork, IUnityContainerProvider
    {
        private IUnityContainer _UnityContainer;

        protected internal NestedUnityUnitOfWork(Session parent, IUnityContainer unityContainer) : base(parent)
        {
            _UnityContainer = unityContainer;
            _UnityContainer.RegisterInstance<Session>(this);
            _UnityContainer.RegisterInstance<UnitOfWork>(this);
            _UnityContainer.RegisterInstance<NestedUnitOfWork>(this);
            _UnityContainer.RegisterInstance(this);
        }

        public IUnityContainer UnityContainer
        {
            get { return _UnityContainer; }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (_UnityContainer != null)
                {
                    var container = _UnityContainer;

                    _UnityContainer = null;

                    container.Dispose();
                }
            }
        }
    }
}