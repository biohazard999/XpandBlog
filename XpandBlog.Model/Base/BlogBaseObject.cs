using System.Runtime.CompilerServices;
using DevExpress.Xpo;
using XpandBlog.Model.Annotations;

namespace XpandBlog.Model
{
    [NonPersistent]
    public abstract class BlogBaseObject : XPBaseObject
    {
        protected BlogBaseObject(Session session) : base(session)
        {
        }

        [NotifyPropertyChangedInvocator]
        public bool SetPropertyValue<T>(ref T valueHolder, T value, [CallerMemberName] string propertyName = null)
        {
            return SetPropertyValue(propertyName, ref valueHolder, value);
        }
    }
}
