namespace pwa_converter.Models.ViewModels;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class OptimiseViewModel
{
    [Required(ErrorMessage= "Please at least choose one image")]
    [DisplayName("Choose Images")]
    public IFormFileCollection Images { get; set; }
}
