using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using XpandBlog.Contracts.Security;

namespace XpandBlog.Model.Security
{
    

    public class User : BlogBaseObjectOid, ISecurityUser, ISecurityUserWithRoles, IUser
    {
        private string _username;
        private bool _isActive;
        private string _passwordHash;

        public User(Session session) : base(session)
        {
        }

        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get { return Roles.OfType<ISecurityRole>().ToList(); }
        }

        public string Username
        {
            get { return _username; }
            set { SetPropertyValue(ref _username, value); }
        }

        string ISecurityUser.UserName { get { return Username; } }

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

        int IUser<int>.Id
        {
            get { return Oid; }
            set { Oid = value; }
        }
    }
}
