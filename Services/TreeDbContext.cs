using Microsoft.EntityFrameworkCore;
using treeHolesApi.Model;

namespace treeHolesApi.Services
{
    public class TreeDbContext : DbContext
    {
        public DbSet<TreeInfo>? treeInfos { get; set; }

        public TreeDbContext(DbContextOptions<TreeDbContext> option) : base(option)        {

        }
    }
}
