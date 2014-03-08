using XpandBlog.Contracts;

namespace XpandBlog.Domain
{
    public abstract class Mapper<TInterface, TObject, TPoco> : IMapper<TInterface, TObject, TPoco> where TInterface : class
        where TObject : TInterface
        where TPoco : TInterface
    {

        public abstract TObject MapUser(TPoco from, TObject to);

        public abstract TPoco MapUser(TObject from, TPoco to);
    }
}