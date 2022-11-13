using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElectionBackEnd.Model
{
    [Index(nameof(Longitude), nameof(Latitude), IsUnique =true)]
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Renseignez le libellé de l'adresse!!!")]
        public string Label { get; set; } = string.Empty;

        [Required(ErrorMessage = "Renseignez la région!!!")]
        public string Region { get; set; } = string.Empty;

        [Precision(2, 7)]
        [Required(ErrorMessage = "Renseignez la latitude!!!")]
        public double Latitude { get; set; }


        [Precision(3, 7)]
        [Required(ErrorMessage = "Renseignez la longitude!!!")]
        public double Longitude { get; set; }

        //------------------------

        [JsonIgnore]
        public virtual List<Elector> Electers { get; set; }

        public Address()
        {
            Electers = new List<Elector>();
        }
    }
}
