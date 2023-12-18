using System.ComponentModel.DataAnnotations;

namespace SalesOrderManagement.DataAccess.DTO
{
    public record WindowDto([Required] string name, [Required] int quantity, [Required] List<SubElementDto> subElementsDto);
}
