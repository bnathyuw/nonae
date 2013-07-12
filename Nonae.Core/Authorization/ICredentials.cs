namespace Nonae.Core.Authorization
{
    public interface ICredentials
    {
        string Username { get; }
        bool IsAnonymous { get; }
        string FailureMessage { get; }
        bool IsAuthenticated { get; }
    }
}