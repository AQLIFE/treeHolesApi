using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using treeHolesApi.Model;
using treeHolesApi.Services;

namespace treeHolesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreeHolesController : Controller
    {
        private readonly TreeDbContext dbContext;

        public TreeHolesController(TreeDbContext _context)
        {
            dbContext = _context;
        }

        [HttpGet]
        public ActionResult<HttpStatusCode> Index()
        {
            return HttpStatusCode.OK;
        }

        [HttpPost]
        public ActionResult<int> Add(string treeInfo)
        {
            TreeInfo info = new TreeInfo() 
            { InfoId = 0, InfoContext = treeInfo, CreateDate = new DateTime() };

            dbContext.Entry<TreeInfo>(info).State = EntityState.Added;

            return dbContext.SaveChanges();
        }

        [HttpDelete]
        public ActionResult<int> Delete(int id)
        {
            TreeInfo ?info = dbContext.Find<TreeInfo>(id);
            if (info != null)
            {
                dbContext.Entry<TreeInfo>(info).State = EntityState.Deleted;
                return dbContext.SaveChanges();
            }
            else return 0;
        }
    }
}
