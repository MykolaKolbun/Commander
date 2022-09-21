using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
    public class CommandUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string? HowTo { get; set; }
        [Required]
        public string? Line { get; set; }
        [Required]
        public string?Platform { get; set; }
        //next step to map DTO

        // Annotatios (attributes) allows to control user inputs. In a case of request doesn tot follow these rules API returns 400 Bad request, but not an exception
    }
}