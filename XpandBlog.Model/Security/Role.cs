using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;

namespace XpandBlog.Model.Security
{
    public class Role : BlogBaseObjectOid, ISecurityRole
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
    }
}