using System;

namespace TodoList.ViewModel
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}