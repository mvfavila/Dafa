using System.ComponentModel.DataAnnotations;

namespace DAFA.Presentation.UI.MVC.ViewModels.Claims
{
    public class ClaimViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Claim name")]
        public string Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Claim value")]
        public string Value { get; set; }
    }
}