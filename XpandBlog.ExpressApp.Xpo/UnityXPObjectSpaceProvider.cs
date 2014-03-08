using System;
using System.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using Microsoft.Practices.Unity;
using XpandBlog.Persistent.Base;
using XpandBlog.Xpo;

namespace XpandBlog.ExpressApp.Xpo
{
    public class UnityXPObjectSpaceProvider : XPObjectSpaceProvider, IUnityContainerProvider
    {
        private IUnityContainer _UnityContainer;

        public IUnityContainer UnityContainer
        {
            get { return _UnityContainer; }
        }

        public UnityXPObjectSpaceProvider(IXpoDataStoreProvider dataStoreProvider, IUnityContainer unityContainer, bool threadSafe)
            : base(dataStoreProvider, threadSafe)
        {
            SetUnityContainer(unityContainer);
        }

        public UnityXPObjectSpaceProvider(string connectionString, IDbConnection connection, IUnityContainer unityContainer, bool threadSafe)
            : base(connectionString, connection, threadSafe)
        {
            SetUnityContainer(unityContainer);
        }

        public UnityXPObjectSpaceProvider(IXpoDataStoreProvider dataStoreProvider, ITypesInfo typesInfo, XpoTypeInfoSource xpoTypeInfoSource, IUnityContainer unityContainer, bool threadSafe)
            : base(dataStoreProvider, typesInfo, xpoTypeInfoSource, threadSafe)
        {
            SetUnityContainer(unityContainer);
        }

        public UnityXPObjectSpaceProvider(IXpoDataStoreProvider dataStoreProvider, IUnityContainer unityContainer)
            : base(dataStoreProvider)
        {

            SetUnityContainer(unityContainer);
        }

        public UnityXPObjectSpaceProvider(string connectionString, IDbConnection connection, IUnityContainer unityContainer)
            : base(connectionString, connection)
        {
            SetUnityContainer(unityContainer);
        }

        public UnityXPObjectSpaceProvider(IXpoDataStoreProvider dataStoreProvider, ITypesInfo typesInfo, XpoTypeInfoSource xpoTypeInfoSource, IUnityContainer unityContainer)
            : base(dataStoreProvider, typesInfo, xpoTypeInfoSource)
        {
            SetUnityContainer(unityContainer);
        }

        protected override UnitOfWork CreateUnitOfWork(IDataLayer dataLayer)
        {
            return new UnityUnitOfWork(dataLayer, UnityContainer.CreateChildContainer(), new IDisposable[0]);
        }

        protected override IObjectSpace CreateObjectSpaceCore()
        {
            return new UnityXPObjectSpace(TypesInfo, XpoTypeInfoSource, () => CreateUnitOfWork(DataLayer));
        }
        
        private void SetUnityContainer(IUnityContainer unityContainer)
        {
            _UnityContainer = unityContainer;
            _UnityContainer.RegisterInstance(this);
            _UnityContainer.RegisterInstance<XPObjectSpaceProvider>(this);
            _UnityContainer.RegisterInstance<IObjectSpaceProvider>(this);
        }
    }
}
