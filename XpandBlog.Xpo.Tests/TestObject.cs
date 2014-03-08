using System.ComponentModel;
using System.Runtime.CompilerServices;
using DevExpress.Xpo;
using XpandBlog.Xpo.Tests.Annotations;
using XpandBlog.Xpo;

namespace XpandBlog.Xpo.Tests
{
    public class TestObject : XPBaseObject
    {
        private int _Oid;
        private string _Name;

        public TestObject(Session session) : base(session)
        {
        }

        [Key(AutoGenerate = true)]
        public int Oid
        {
            get { return _Oid; }
            set { SetPropertyValue(ref _Oid, value); }
        }

        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue(ref _Name, value); }
        }

        [NotifyPropertyChangedInvocator]
        protected bool SetPropertyValue<T>(ref T valueHolder, T value, [CallerMemberName] string propertyName = null)
        {
            return SetPropertyValue(propertyName, ref valueHolder, value);
        }
    }
}
