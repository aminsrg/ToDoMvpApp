namespace ToDoMvpApp.Domain.Entities;

public class User : ToDoMvpApp.Common.BaseEntities.Abstracts.EntityID
{
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public ICollection<ToDo> Todos { get; set; } = new List<ToDo>();
}
