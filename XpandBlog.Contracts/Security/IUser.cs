namespace XpandBlog.Contracts.Security
{
    public interface IUser : IUser<int>
    {
        
    }

    public interface IUser<TKey>
    {
        TKey Id { get; set; }
        string Username { get; set; }
        bool IsActive { get; set; }
        string PasswordHash { get; set; }
    }
}
