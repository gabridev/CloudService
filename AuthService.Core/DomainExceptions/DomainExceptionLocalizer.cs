using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthService.Core.DomainExceptions
{
    public class DomainExceptionLocalizer : IDomainExceptionLocalizer
    {
        private readonly IDomainLocalizationProvider localizer;

        public DomainExceptionLocalizer(IDomainLocalizationProvider localizer)
        {
            this.localizer = localizer;
        }

        public string LocalizeMessage(DomainException ex)
        {
            if (localizer.TryGetLocalizedMessageTemplate(ex.ErrorCode, out var value))
            {
                return ex.MessageParams?.Any() == true ? string.Format(value, ex.MessageParams) : value;
            }
            else
            {
                return ex.MessageTemplate != null && ex.MessageParams?.Any() == true ?
                    string.Format(ex.MessageTemplate, ex.MessageParams)
                     :
                    ex.MessageTemplate ?? ex.GetType().Name;
            }
        }
    }
}

