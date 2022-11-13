using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElectionBackEnd.Model
{
    public class Center
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        [Required(ErrorMessage = "Renseignez le nom du centre!!!")]
        public string Name { get; set; } = string.Empty;
        public int Nb_Desk { get; set; }

        public bool IsFull { get; set; } = false;

        //----------------------------

        [JsonIgnore]
        public virtual List<Desk> Desks { get; set; }
        public Center()
        {
            Desks = new List<Desk>();
        }
    }
}
