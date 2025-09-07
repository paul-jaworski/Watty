using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class DeviceDTO
{
    [Required]
    public string Id { get; set; } = "";
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public required string Type { get; set; } = "";

}
