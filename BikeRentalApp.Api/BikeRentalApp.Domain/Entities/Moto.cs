using System.ComponentModel.DataAnnotations;

namespace BikeRentalApp.Domain.Entities {
    public class Moto {
        [Key]
        public string Identificador { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        public string Placa { get; set; }
    }
}
