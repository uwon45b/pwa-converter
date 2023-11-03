namespace pwa_converter.Models.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class IndexViewModel
{
    [Required]
    [DisplayName("URL")]
    public string Url { get; set; }
}
