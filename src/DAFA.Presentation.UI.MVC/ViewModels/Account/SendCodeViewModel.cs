using System.Collections.Generic;
using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC.ViewModels.Account
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }

        [HiddenInput]
        public string UserId { get; set; }
    }
}