namespace XpandBlog.Contracts
{
    public interface IMapper<TInterface, TObject, TPoco> 
        where TInterface : class 
        where TObject : TInterface 
        where TPoco : TInterface
    {
        TObject MapUser(TPoco from, TObject to);
        TPoco MapUser(TObject from, TPoco to);
    }
}