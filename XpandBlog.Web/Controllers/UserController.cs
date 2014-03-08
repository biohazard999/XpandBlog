using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using XpandBlog.ExpressApp.Xpo;
using XpandBlog.Model.Security;
using XpandBlog.Xpo;
using Controller = System.Web.Mvc.Controller;

namespace XpandBlog.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IObjectSpaceProvider _OsProvider;

        public UserController(IObjectSpaceProvider osProvider)
        {
            _OsProvider = osProvider;
        }

        public async Task<ActionResult> Index()
        {
            using (var os = _OsProvider.CreateObjectSpace())
            {
                var users = await os.GetSession().GetObjectsAsync<User>(CriteriaOperator.Parse("1=1"));
                
                return View(users.Select(m => new DTO.Security.User {
                    Username = m.Username,
                    IsActive = m.IsActive,
                    PasswordHash = m.PasswordHash
                }));
            }
        }
    }
}