namespace SymphTagsApp.Application.Interfaces
{
    public interface ICurrentUser
    {
        int Id { get; }
        string DisplayName { get; }
        string Email { get; }
    }
}
