using AuthService.Core.DomainExceptions;
using Microsoft.Extensions.Localization;
using System;

namespace AuthService.API.ActionFilters
{
    public class AspNetCoreDomainLocalizationProvider : IDomainLocalizationProvider
    {
        private readonly IStringLocalizer<AspNetCoreDomainLocalizationProvider> localizer;

        public AspNetCoreDomainLocalizationProvider(IStringLocalizer<AspNetCoreDomainLocalizationProvider> localizer)
        {
            this.localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        public bool TryGetLocalizedMessageTemplate(string messageCode, out string localizedMessage)
        {
            localizedMessage = null;
            var value = localizer.GetString(messageCode);
            if (value.ResourceNotFound) return false;

            localizedMessage = value.Value;
            return true;
        }
    }
}
