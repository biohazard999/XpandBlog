using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Microsoft.Practices.Unity;
using XpandBlog.ExpressApp.Xpo;
using XpandBlog.Model;

namespace XpandBlog.Web.Helpers.Xaf
{
    public interface IXafHelper
    {
        IObjectSpace CreateObjectSpace();
    }

    public class XafHelper : IXafHelper
    {
        private readonly IObjectSpaceProvider _ObjectSpaceProvider;

        static XafHelper()
        {
            XpoDefault.GuidGenerationMode = GuidGenerationMode.UuidCreateSequential;
            Session.GlobalSuppressExceptionOnReferredObjectAbsentInDataStore = false;
            XpoDefault.IdentityMapBehavior = IdentityMapBehavior.Strong;
            XpoDefault.Session = null;
        }

        private static IEnumerable<Type> Types
        {
            get { return ModelTypeList.ExportedTypes; }
        }

        public XafHelper(IUnityContainer unityContainer, string connectionString = null)
        {
            if (connectionString == null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }

            foreach (var type in Types)
            {
                XafTypesInfo.Instance.RegisterEntity(type);
            }

            var typesInfoSource = new XpoTypeInfoSource(XafTypesInfo.Instance as TypesInfo);

            foreach (var type in Types)
            {
                typesInfoSource.RegisterEntity(type);
            }

            _ObjectSpaceProvider = new UnityXPObjectSpaceProvider(new ConnectionStringDataStoreProvider(connectionString), XafTypesInfo.Instance, typesInfoSource, unityContainer, true);

            using (var objectSpace = _ObjectSpaceProvider.CreateUpdatingObjectSpace(false))
            {
                var xpObjectSpace = objectSpace as XPObjectSpace;
                if (xpObjectSpace != null)
                    xpObjectSpace.Session.CreateObjectTypeRecords();
            }

        }

        public IObjectSpace CreateObjectSpace()
        {
            return _ObjectSpaceProvider.CreateObjectSpace();
        }
    }
}