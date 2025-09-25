using ToDoMvpApp.Common.BaseEntities.Interfaces;

namespace ToDoMvpApp.Common.BaseEntities.Abstracts
{
    public abstract class EntityCode : BaseEntity, IEntityCode
    {
        public byte Code { get; set; }
    }
}