namespace pwa_converter.Models.ViewModels;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


public class ManifestViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    [DisplayName("Short Name")]
    public string ShortName { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [DisplayName("ID")]
    public string Id { get; set; }

    [Required]
    [DisplayName("Start URL")]
    public string StartUrl { get; set; }

    [Required]
    public string Scope { get; set; }

    [Required]
    public string Language { get; set; }

    [Required]
    public string Display { get; set; }

    [Required]
    [DisplayName("Theme Color")]
    public string ThemeColor { get; set; }

    [Required]
    [DisplayName("Background Color")]
    public string BackgroundColor { get; set; }
}
