using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementApp.DataAccess.Entities
{
    public class SubElement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public int Element { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public virtual Window Window { get; set; }
    }
}
