using System.ComponentModel.DataAnnotations;

namespace BikeRentalApp.Domain.Entities {
    public class Entregador {
        [Key]
        public string Identificador { get; set; }
        public string Nome { get; set; } 
        public string CNPJ { get; set; } 
        public DateTime Data_Nascimento { get; set; } 
        public string Numero_CNH { get; set; } 
        public string Tipo_CNH { get; set; } 
        public string Imagem_CNH { get; set; }
        public ICollection<Locacao> Locacoes { get; set; } = new List<Locacao>();
    }
}
