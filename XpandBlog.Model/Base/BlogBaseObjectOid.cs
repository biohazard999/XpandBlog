using DevExpress.Xpo;

namespace XpandBlog.Model
{
    [NonPersistent]
    public abstract class BlogBaseObjectOid : BlogBaseObject
    {
        protected BlogBaseObjectOid(Session session) : base(session)
        {
        }

    }
}