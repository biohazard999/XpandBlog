using DevExpress.Xpo;

namespace XpandBlog.Model
{
    [NonPersistent]
    public abstract class BlogBaseObjectOid : BlogBaseObject
    {
        private int _oid;

        protected BlogBaseObjectOid(Session session) : base(session)
        {
        }


        [Key(AutoGenerate = true)]
        public int Oid
        {
            get { return _oid; }
            set { SetPropertyValue(ref _oid, value); }
        }
    }
}