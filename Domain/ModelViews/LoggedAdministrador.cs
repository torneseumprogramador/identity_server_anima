namespace Identity.Domain.ModelViews;

public struct LoggedAdministrador
{
    public SimpleAdministrator Administrator { get;set;}
    public string Token {get;set;}
}