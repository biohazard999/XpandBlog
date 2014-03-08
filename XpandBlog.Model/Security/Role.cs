using System.Security.Cryptography;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using XpandBlog.Contracts.Security;

namespace XpandBlog.Model.Security
{
    public class Role : BlogBaseObjectOid, ISecurityRole, IRole
    {
        private string _name;

        public Role(Session session) : base(session)
        {
        }

        public string Name
        {
            get { return _name; }
            set { SetPropertyValue(ref _name, value); }
        }

        [Association("User-Role")]
        public XPCollection<User> Users
        {
            get { return GetCollection<User>("Users"); }
        }

        int IRole<int>.Id
        {
            get { return this.Oid; }
            set { Oid = value; }
        }
    }
}