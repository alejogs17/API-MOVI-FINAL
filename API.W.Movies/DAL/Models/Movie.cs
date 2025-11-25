using System.ComponentModel.DataAnnotations;

namespace API.W.Movies.DAL.Models
{
    public class Movie : AuditBase
    {
        [Required(ErrorMessage = "El nombre de la película es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [Display(Name = "Nombre de la película")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria")]
        [Display(Name = "Duración (minutos)")]
        public int Duration { get; set; }

        [MaxLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "La clasificación es obligatoria")]
        [MaxLength(10, ErrorMessage = "La clasificación no puede exceder los 10 caracteres")]
        [Display(Name = "Clasificación")]
        public string Clasification { get; set; }
    }
}
