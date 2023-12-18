using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementApp.DataAccess.Entities
{
    public class Window
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public string Name { get; set; }
        public int QuantityOfWindows { get; set; }
        public List<SubElement> SubElements { get; set; }
        public virtual Order Order { get; set; }
    }
}
