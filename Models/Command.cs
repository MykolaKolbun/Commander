using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Command
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string? HowTo { get; set; }
        [Required]
        public string? Line { get; set; }
        [Required]
        public string?Platform { get; set; }

        //After model is complited next step is creating repository
        // Folder Data. Interface file (ICommanderRepo) - just to separate data engine from implementation
    }
}