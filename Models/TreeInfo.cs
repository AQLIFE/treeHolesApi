using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace treeHolesApi.Model
{
    [Table("TreeHolesInfo")]
    public class TreeInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int InfoId { get; set; }

        [Column]
        public string? InfoContext { get; set; }

        [Column("InfoCreated")]
        public DateTime? CreateDate
        {
            get { return this.CreateDate; }

            set
            {
                this.CreateDate = new DateTime();
            }
        }
    }
}
