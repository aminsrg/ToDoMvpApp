using ToDoMvpApp.Common.BaseEntities.Interfaces;

namespace ToDoMvpApp.Common.BaseEntities.Abstracts
{
    public abstract class BaseEntity : IBaseEntitys
    {
        public DateTime InsertDateTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDateTime { get; set; } = DateTime.UtcNow;
    }
}
