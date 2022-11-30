using Microsoft.EntityFrameworkCore;
using treeHolesApi.Model;
using treeHolesApi.Models;

namespace treeHolesApi.Services
{
    public class TreeDbContext : DbContext
    {
        public DbSet<TreeInfo>? treeInfos { get; set; }

        public virtual DbSet<State>? States { get; set; }

        public TreeDbContext(DbContextOptions<TreeDbContext> option) : base(option)
        {

        }

    }
}
