using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;

namespace XpandBlog.Model.Security
{
    public class User : BlogBaseObjectOid, ISecurityUser, ISecurityUserWithRoles
    {
        private string _userName;
        private bool _isActive;
        private string _passwordHash;

        public User(Session session) : base(session)
        {
        }

        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get { return Roles.OfType<ISecurityRole>().ToList(); }
        }

        public string UserName
        {
            get { return _userName; }
            set { SetPropertyValue(ref _userName, value); }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { SetPropertyValue(ref _isActive, value); }
        }

        public string PasswordHash
        {
            get { return _passwordHash; }
            set { SetPropertyValue(ref _passwordHash, value); }
        }

        [Association("User-Role")]
        public XPCollection<Role> Roles
        {
            get { return GetCollection<Role>("Roles"); }
        }
    }
}
