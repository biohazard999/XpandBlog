using System.Runtime.InteropServices.ComTypes;

namespace XpandBlog.Contracts.Security
{
    public interface IRole : IRole<int>
    {
        
    }
    public interface IRole<TKey>
    {
        TKey Id { get; set; }
        string Name { get; set; }
    }
}