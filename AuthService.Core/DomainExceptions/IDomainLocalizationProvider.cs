namespace AuthService.Core.DomainExceptions
{
    public interface IDomainLocalizationProvider
    {
        bool TryGetLocalizedMessageTemplate(string messageCode, out string localizedMessage);
    }
}
