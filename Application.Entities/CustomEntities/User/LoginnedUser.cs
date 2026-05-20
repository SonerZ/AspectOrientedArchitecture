namespace Application.Entities.CustomEntities.User;

public class LoginnedUser
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public DateTime LastLoginDate { get; set; }
}