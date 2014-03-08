using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Microsoft.Practices.Unity;
using XpandBlog.Persistent.Base;

namespace XpandBlog.Xpo
{
    public class UnityUnitOfWork : UnitOfWork, IUnityContainerProvider
    {
        private IUnityContainer _UnityContainer;

        public UnityUnitOfWork()
        {
        }

        public UnityUnitOfWork(IUnityContainer unityContainer)
        {
            SetUnityContainer(unityContainer);
        }

        public UnityUnitOfWork(XPDictionary dictionary, IUnityContainer unityContainer)
            : base(dictionary)
        {
            SetUnityContainer(unityContainer);
        }

        public UnityUnitOfWork(IDataLayer layer, IUnityContainer unityContainer, params IDisposable[] disposeOnDisconnect)
            : base(layer, disposeOnDisconnect)
        {
            SetUnityContainer(unityContainer);
        }

        public UnityUnitOfWork(IObjectLayer layer, IUnityContainer unityContainer, params IDisposable[] disposeOnDisconnect)
            : base(layer, disposeOnDisconnect)
        {
            SetUnityContainer(unityContainer);
        }

        public IUnityContainer UnityContainer
        {
            get { return _UnityContainer; }
        }

        private void SetUnityContainer(IUnityContainer unityContainer)
        {
            if (unityContainer != null)
            {
                unityContainer.RegisterInstance<Session>(this);
                unityContainer.RegisterInstance<UnitOfWork>(this);
                unityContainer.RegisterInstance(this);
            }
            _UnityContainer = unityContainer;
        }

        protected override NestedUnitOfWork CreateNestedUnitOfWork()
        {
            return _UnityContainer != null ? new NestedUnityUnitOfWork(this, _UnityContainer.CreateChildContainer()) : base.CreateNestedUnitOfWork();
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