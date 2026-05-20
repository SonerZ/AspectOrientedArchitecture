
namespace Application.Entities.CustomEntities.User;

public class UserCreateDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword  { get; set; }
}