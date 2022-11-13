using System.ComponentModel.DataAnnotations;

namespace ElectionBackEnd.Model
{
    public class Elector
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public int DeskId { get; set; }
        public int CandidateId { get; set; }

        [Required(ErrorMessage = "Renseignez votre prénom!!!")]
        [MinLength(3,ErrorMessage ="Un prénom est forcément supérieur à 2 caractères")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Renseignez votre nom de famille!!!")]
        [MinLength(3, ErrorMessage = "Un prénom est forcément supérieur à 2 caractères")]
        public string LastName { get; set; } = string.Empty;

        public bool Voted { get; set; } = false;

        //----------------------------------

        public virtual Address? Address { get; set; }
        public virtual Desk? Desk { get; set; }
        public virtual Candidate? Candidate { get; set; }
        
    }
}
