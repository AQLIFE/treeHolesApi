using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace treeHolesApi.Model
{
    [Table("TreeHolesInfo")]
    public class TreeInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Key,Column("InfoId")]
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
