namespace AuthService.Core.DomainExceptions
{
    public interface IDomainExceptionLocalizer
    {
        string LocalizeMessage(DomainException ex);
    }
}
