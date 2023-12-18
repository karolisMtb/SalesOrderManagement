using System.ComponentModel.DataAnnotations;

namespace SalesOrderManagement.DataAccess.DTO
{
    public record SalesOrderDto([Required] string name, [Required] string state, [Required] List<WindowDto> windows);
}
