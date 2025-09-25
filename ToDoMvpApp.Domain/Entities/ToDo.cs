using ToDoMvpApp.Domain.Enums;

namespace ToDoMvpApp.Domain.Entities
{
    internal class ToDo : ToDoMvpApp.Common.BaseEntities.Abstracts.EntityID
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsImportant { get; set; } = false;
        public RepeatFrequency Repeat { get; set; } = RepeatFrequency.None;
        public DateTime? ReminderAt { get; set; }
    }
}
