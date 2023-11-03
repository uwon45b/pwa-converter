namespace pwa_converter.Models.ViewModels;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


public class ServiceWorkerViewModel
{
    [DisplayName("Cache Content")]
    public string? CacheContent { get; set; }

    [Required]
    [DisplayName("Caching Strategy")]
    public string CachingStrategy { get; set; }
}
