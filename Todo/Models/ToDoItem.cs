using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
