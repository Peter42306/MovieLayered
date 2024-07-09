using System.ComponentModel.DataAnnotations;

namespace MovieLayered.BLL.DTO
{
    public class MovieDTO
    {
        // Data Transfer Object - специальная модель для передачи данных
        // Класс PlayerDTO должен содержать только те данные, которые нужно передать 
        // на уровень представления или, наоборот, получить с этого уровня.

        public int Id { get; set; }

        [Required(ErrorMessage ="Поле должно быть установлено")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Director { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Genre { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? PosterPath { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Description { get; set; }
    }
}
