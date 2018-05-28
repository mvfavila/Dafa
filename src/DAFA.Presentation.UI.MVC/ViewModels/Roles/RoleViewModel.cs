using System.ComponentModel.DataAnnotations;

namespace DAFA.Presentation.UI.MVC.ViewModels.Roles
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Role name")]
        public string Name { get; set; }
    }
}