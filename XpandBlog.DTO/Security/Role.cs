using XpandBlog.Contracts.Security;

namespace XpandBlog.DTO.Security
{
    public class Role : IRole, Microsoft.AspNet.Identity.IRole<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}