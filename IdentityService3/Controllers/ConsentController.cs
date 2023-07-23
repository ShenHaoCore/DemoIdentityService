using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using IdentityService3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService3.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [SecurityHeaders]
    [Authorize]
    public class ConsentController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly ILogger<ConsentController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interaction"></param>
        /// <param name="events"></param>
        /// <param name="logger"></param>
        public ConsentController(IIdentityServerInteractionService interaction,
            IEventService events,
            ILogger<ConsentController> logger)
        {
            _interaction = interaction;
            _events = events;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var vm = await BuildViewModelAsync(returnUrl);
            if (vm != null) { return View("Index", vm); }

            return View("Error");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ConsentInputModel model)
        {
            var result = await ProcessConsent(model);
            if (result.IsRedirect)
            {
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context?.IsNativeClient() == true) { return this.LoadingPage("Redirect", result.RedirectUri); }
                return Redirect(result.RedirectUri);
            }
            if (result.HasValidationError) { ModelState.AddModelError(string.Empty, result.ValidationError); }
            if (result.ShowView) { return View("Index", result.ViewModel); }
            return View("Error");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<ProcessConsentResult> ProcessConsent(ConsentInputModel model)
        {
            var result = new ProcessConsentResult();
            var request = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            if (request == null) return result;

            ConsentResponse? grantedConsent = null;
            if (model?.Button == "no")
            {
                grantedConsent = new ConsentResponse { Error = AuthorizationError.AccessDenied };
                await _events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues));
            }
            else if (model?.Button == "yes")
            {
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    var scopes = model.ScopesConsented;
                    if (ConsentOptions.EnableOfflineAccess == false) { scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess); }
                    grantedConsent = new ConsentResponse { RememberConsent = model.RememberConsent, ScopesValuesConsented = scopes.ToArray(), Description = model.Description };
                    await _events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues, grantedConsent.ScopesValuesConsented, grantedConsent.RememberConsent));
                }
                else
                {
                    result.ValidationError = ConsentOptions.MustChooseOneErrorMessage;
                }
            }
            else
            {
                result.ValidationError = ConsentOptions.InvalidSelectionErrorMessage;
            }

            if (grantedConsent != null)
            {
                await _interaction.GrantConsentAsync(request, grantedConsent);
                result.RedirectUri = model!.ReturnUrl;
                result.Client = request.Client;
            }
            else
            {
                result.ViewModel = await BuildViewModelAsync(model!.ReturnUrl, model);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<ConsentViewModel?> BuildViewModelAsync(string returnUrl, ConsentInputModel? model = null)
        {
            var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (request != null)
            {
                return CreateConsentViewModel(model, returnUrl, request);
            }
            else
            {
                _logger.LogError("No consent request matching request: {0}", returnUrl);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private ConsentViewModel CreateConsentViewModel(ConsentInputModel? model, string returnUrl, AuthorizationRequest request)
        {
            var vm = new ConsentViewModel
            {
                RememberConsent = model?.RememberConsent ?? true,
                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),
                Description = model?.Description!,
                ReturnUrl = returnUrl,
                ClientName = request.Client.ClientName ?? request.Client.ClientId,
                ClientUrl = request.Client.ClientUri,
                ClientLogoUrl = request.Client.LogoUri,
                AllowRememberConsent = request.Client.AllowRememberConsent
            };

            vm.IdentityScopes = request.ValidatedResources.Resources.IdentityResources.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();

            var apiScopes = new List<ScopeViewModel>();
            foreach (var parsedScope in request.ValidatedResources.ParsedScopes)
            {
                var apiScope = request.ValidatedResources.Resources.FindApiScope(parsedScope.ParsedName);
                if (apiScope != null)
                {
                    var scopeVm = CreateScopeViewModel(parsedScope, apiScope, vm.ScopesConsented.Contains(parsedScope.RawValue) || model == null);
                    apiScopes.Add(scopeVm);
                }
            }
            if (ConsentOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
            {
                apiScopes.Add(GetOfflineAccessScope(vm.ScopesConsented.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) || model == null));
            }
            vm.ApiScopes = apiScopes;
            return vm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        private ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
        {
            return new ScopeViewModel
            {
                Value = identity.Name,
                DisplayName = identity.DisplayName ?? identity.Name,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parsedScopeValue"></param>
        /// <param name="apiScope"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public ScopeViewModel CreateScopeViewModel(ParsedScopeValue parsedScopeValue, ApiScope apiScope, bool check)
        {
            var displayName = apiScope.DisplayName ?? apiScope.Name;
            if (!String.IsNullOrWhiteSpace(parsedScopeValue.ParsedParameter)) { displayName += ":" + parsedScopeValue.ParsedParameter; }
            return new ScopeViewModel { Value = parsedScopeValue.RawValue, DisplayName = displayName, Description = apiScope.Description, Emphasize = apiScope.Emphasize, Required = apiScope.Required, Checked = check || apiScope.Required };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        private ScopeViewModel GetOfflineAccessScope(bool check)
        {
            return new ScopeViewModel { Value = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess, DisplayName = ConsentOptions.OfflineAccessDisplayName, Description = ConsentOptions.OfflineAccessDescription, Emphasize = true, Checked = check };
        }
    }
}
