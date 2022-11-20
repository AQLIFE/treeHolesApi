using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult<List<TreeInfo>> Index()
        {
            if (dbContext.treeInfos != null)
                return dbContext.treeInfos.ToList();
            else throw new Exception("数据库记录为空,或未连接到数据库");
        }

        [HttpPost]
        public ActionResult<int> Add(string treeInfo)
        {
            TreeInfo info = new()
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
