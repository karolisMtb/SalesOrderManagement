using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementApp.DataAccess.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string State { get; set; }
        public List<Window> Windows { get; set; }
    }
}
