using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DAFA.Infra.CrossCutting.Identity;
using DAFA.Infra.CrossCutting.Identity.Configuration;
using DAFA.Presentation.UI.MVC.ViewModels.Account;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using DAFA.Infra.CrossCutting.Logging;
using DAFA.Presentation.UI.MVC.Util;

namespace DAFA.Presentation.UI.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = await userManager.FindAsync(model.Email, model.Password);

                    if (!user.EmailConfirmed)
                    {
                        TempData["AvisoEmail"] = "User not confirmed. Please check your e-mail.";
                    }
                    await SignInAsync(user, model.RememberMe);

                    Log.Info($"Login usuário: {UtilString.AddEmailMask(user.Email)}");

                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    Log.Info($"Usuário bloqueado: {UtilString.AddEmailMask(model.Email)}");
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    Log.Info($"Usuário a ser verificado: {UtilString.AddEmailMask(model.Email)}");
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Incorrect login or password.");
                    return View(model);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            var clientKey = Request.Browser.Type;
            await userManager.SignInClientAsync(user, clientKey);
            // Setting incorrect login attempt counter back to zero
            await userManager.ResetAccessFailedCountAsync(user.Id);

            // Collecting external claims
            var ext = await AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie,
                DefaultAuthenticationTypes.TwoFactorCookie, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn
                (
                    new AuthenticationProperties { IsPersistent = isPersistent },
                    // Creating Identity instance and setting claims
                    await user.GenerateUserIdentityAsync(userManager, ext)
                );
        }

        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, string userId)
        {
            // Requerys that the user has made um login using his password
            if (!await signInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await userManager.FindByIdAsync(await signInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "DEMO: In case the code has not arrived through " + provider + " the code is: ";
                ViewBag.CodigoAcesso = await userManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, UserId = userId });
        }

        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await signInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false,
                rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = userManager.FindByIdAsync(model.UserId);
                    await SignInAsync(user.Result, false);
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(
            [Bind(Include = "Email,Name,Lastname,Password,ConfirmPassword")]
            RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Lastname = model.Lastname
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await AddRegistrationClaimsToUser(user);
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account",
                        new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await userManager.SendEmailAsync(user.Id, "Confirm your account",
                        "Please confirm your account by clicking here: <a href='" + callbackUrl + "'></a>");
                    ViewBag.Link = callbackUrl;
                    return View("DisplayEmail");
                }
                AddErrors(result);
            }

            // In case of error show the same view
            return View(model);
        }

        private async Task AddRegistrationClaimsToUser(ApplicationUser user)
        {
            const string CLAIM_IS_PROVIDER_TYPE = "IsProvider";
            const string CLAIM_IS_PROVIDER_VALUE = "False";

            await userManager.AddClaimAsync(user.Id, new Claim(CLAIM_IS_PROVIDER_TYPE, CLAIM_IS_PROVIDER_VALUE));
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Não revelar se o usuario nao existe ou nao esta confirmado
                    return View(nameof(ForgotPasswordConfirmation));
                }

                var code = await userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action(nameof(ResetPassword), "Account",
                    new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await userManager.SendEmailAsync(user.Id, "Forgot my password",
                    "Please change your password by clicking here: <a href='" + callbackUrl + "'></a>");
                ViewBag.Link = callbackUrl;
                ViewBag.Status = "DEMO: In case the link does not arrive: ";
                ViewBag.LinkAcesso = callbackUrl;
                return View(nameof(ForgotPasswordConfirmation));
            }

            // In case of error show the same view
            return View(model);
        }

        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Do not show if the user exists or is not confirmed
                return RedirectToAction(nameof(ResetPasswordConfirmation), "Account");
            }
            var result = await userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation), "Account");
            }
            AddErrors(result);
            return View();
        }

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Redirect to external login provider
            return new ChallengeResult(provider, Url.Action(nameof(ExternalLoginCallback), "Account",
                new { ReturnUrl = returnUrl }));
        }

        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var userId = await signInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await userManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose })
                .ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, UserId = userId });
        }

        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generates and sends the token
            if (!await signInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction(nameof(VerifyCode),
                new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, userId = model.UserId });
        }

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var user = await userManager.FindAsync(loginInfo.Login);

            // Creates a log entry if an external login exists and the user is already logged on this login provider
            var result = await signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);

            switch (result)
            {
                case SignInStatus.Success:
                    var userext = userManager.FindByEmailAsync(user.Email);
                    await SignInAsync(userext.Result, false);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account request that an account is created

                    var externalIdentity = HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(
                        DefaultAuthenticationTypes.ExternalCookie);
                    var email = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:email")
                        .Value;
                    var firstName = externalIdentity.Result.Claims
                        .FirstOrDefault(c => c.Type == "urn:facebook:first_name").Value;
                    var lastName = externalIdentity.Result.Claims
                        .FirstOrDefault(c => c.Type == "urn:facebook:last_name").Value;

                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View(nameof(ExternalLoginConfirmation),
                        new ExternalLoginConfirmationViewModel { Email = loginInfo.Email, LastName = lastName,
                            Name = firstName });
            }
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Pegar a informação do login externo.
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View(nameof(ExternalLoginFailure));
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        var userext = userManager.FindByEmailAsync(model.Email);
                        await SignInAsync(userext.Result, false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            await SignOutAsync();
            //AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private readonly static string XSRF_KEY = ConfigurationManager.AppSettings["Internet"];

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XSRF_KEY] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        private async Task SignOutAsync()
        {
            var clientKey = Request.Browser.Type;
            var user = userManager.FindById(User.Identity.GetUserId());
            await userManager.SignOutClientAsync(user, clientKey);
            AuthenticationManager.SignOut();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignOutEverywhere()
        {
            userManager.UpdateSecurityStamp(User.Identity.GetUserId());
            await SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOutClient(int clientId)
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            var client = user.Clients.SingleOrDefault(c => c.Id == clientId);
            if (client != null)
            {
                user.Clients.Remove(client);
            }
            userManager.Update(user);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (userManager != null)
                {
                    userManager.Dispose();
                    userManager = null;
                }

                if (signInManager != null)
                {
                    signInManager.Dispose();
                    signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}