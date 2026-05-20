namespace Application.Entities.CustomEntities;

public class GenericResponse<T>
{
    public T Model { get; set; }
    public int PageCount { get; set; }
}