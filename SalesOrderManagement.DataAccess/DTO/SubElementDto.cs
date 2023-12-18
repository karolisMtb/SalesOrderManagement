using System.ComponentModel.DataAnnotations;

namespace SalesOrderManagement.DataAccess.DTO
{
    public record SubElementDto([Required] int Element, [Required] string type, [Required] int width, [Required] int height);
}
