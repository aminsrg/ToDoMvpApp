using ToDoMvpApp.Common.BaseEntities.Interfaces;

namespace ToDoMvpApp.Common.BaseEntities.Abstracts
{
    public abstract class EntityID : BaseEntity, IEntityID
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
