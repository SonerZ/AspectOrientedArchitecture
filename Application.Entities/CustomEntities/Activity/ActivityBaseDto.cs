namespace Application.Entities.CustomEntities.User;

public class ActivityBaseDto
{
    public string Id { get; set; }
    public string ActivityType { get; set; }
    public DateTime ActivityDate { get; set; }
    public string Description { get; set; }
    public int Count{get;set;}
    public bool CheckValue{get;set;}
}