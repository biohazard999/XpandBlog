using XpandBlog.Contracts.Security;

namespace XpandBlog.DTO.Security
{
    public class User : IUser, Microsoft.AspNet.Identity.IUser<int>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public string PasswordHash { get; set; }
        
        string Microsoft.AspNet.Identity.IUser<int>.UserName
        {
            get { return Username; }
            set { Username = value; }
        }
    }
}