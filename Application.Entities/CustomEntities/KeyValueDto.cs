namespace Application.Entities.CustomEntities;

public class KeyValueDTO<TCode, TName>
{
    public TCode Code { get; set; }
    public TName Name { get; set; }

    public KeyValueDTO(TCode code, TName name)
    {
        Code = code;
        Name = name;
    }
}