using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using Microsoft.Practices.Unity;
using XpandBlog.Xpo;

namespace XpandBlog.ExpressApp.Xpo
{
    public class NestedUnityXPObjectSpace : XPNestedObjectSpace
    {
        public NestedUnityXPObjectSpace(IObjectSpace parentObjectSpace) : base(parentObjectSpace)
        {
        }

        protected override UnitOfWork RecreateUnitOfWork()
        {
            var uow = base.RecreateUnitOfWork();

            var unitContainer = uow.GetUnityContainer();

            if (unitContainer != null)
            {
                unitContainer.RegisterInstance<IObjectSpace>(this, new HierarchicalLifetimeManager());
                unitContainer.RegisterInstance<XPObjectSpace>(this, new HierarchicalLifetimeManager());
                unitContainer.RegisterInstance<XPNestedObjectSpace>(this, new HierarchicalLifetimeManager());
                unitContainer.RegisterInstance(this, new HierarchicalLifetimeManager());
            }

            return uow;
        }

        public override IObjectSpace CreateNestedObjectSpace()
        {
            return new NestedUnityXPObjectSpace(this);
        }
    }
}