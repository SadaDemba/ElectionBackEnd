using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ElectionBackEnd.Model
{
    public class Desk
    {
        public int Id { get; set; }
        public int CenterId { get; set; }

        [Required(ErrorMessage ="Vous devez renseigner la capacité du bureau")]
        public int Capacity { get; set; }

        public int Nb_ElectersAssignable { get; set; }

        //----------------------------
        public virtual Center? Center { get; set; }
        [JsonIgnore]
        public virtual List<Elector>? Electers { get; set; }
        public Desk()
        {
            Nb_ElectersAssignable = Capacity;
        }
    }
}
