using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElectionBackEnd.Model
{
    [Index(nameof(Parti), IsUnique = true)]
    public class Candidate
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Renseignez votre prénom!!!")]
        [MinLength(3, ErrorMessage = "Un prénom est forcément supérieur à 2 caractères")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Renseignez votre nom de famille!!!")]
        [MinLength(2, ErrorMessage = "Un Nom est forcément supérieur à 1 caractère")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Renseignez votre nom de parti politique!!!")]
        public string Parti { get; set; } = string.Empty;
        //=====================================
        [JsonIgnore]
        public virtual List<Elector> Electors { get; set; }
        public Candidate()
        {
            Electors = new List<Elector>(); 
        }
    }
}
