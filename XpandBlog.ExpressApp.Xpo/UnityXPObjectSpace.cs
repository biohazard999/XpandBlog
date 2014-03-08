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
    public class UnityXPObjectSpace : XPObjectSpace, IUnityContainerProvider
    {
        public UnityXPObjectSpace(ITypesInfo typesInfo, XpoTypeInfoSource xpoTypeInfoSource, CreateUnitOfWorkHandler createUnitOfWorkDelegate) : base(typesInfo, xpoTypeInfoSource, createUnitOfWorkDelegate)
        {
        }

        public IUnityContainer UnityContainer
        {
            get { return Session.GetUnityContainer(); }
        }

        protected override UnitOfWork RecreateUnitOfWork()
        {
            var uow = base.RecreateUnitOfWork();

            var unityContainer = uow.GetUnityContainer();

            if (unityContainer != null)
            {
                unityContainer.RegisterInstance<IObjectSpace>(this, new HierarchicalLifetimeManager());
                unityContainer.RegisterInstance<XPObjectSpace>(this, new HierarchicalLifetimeManager());
                unityContainer.RegisterInstance(this, new HierarchicalLifetimeManager());
            }

            return uow;
        }

        public override IObjectSpace CreateNestedObjectSpace()
        {
            return new NestedUnityXPObjectSpace(this);
        }
    }
}