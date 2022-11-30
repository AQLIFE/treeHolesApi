using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace treeHolesApi.Model
{
    [Table("TreeHolesInfo")]
    public class TreeInfo
    {
        [Key]
        public int InfoId { get; set; }

        [Column]
        public string? InfoContext { get; set; }

        [Column("InfoCreated")]
        public DateTime? CreateDate { get; set; }

        [Column("InfoState")]
        public int State { get; set; }

        public TreeInfo() { }

        public TreeInfo(string info) {
            this.InfoId = 0;
            this.InfoContext = info;
            this.CreateDate = DateTime.Now;
            this.State = 5;
        }
    }
}
