using System.ComponentModel.DataAnnotations;

namespace BikeRentalApp.Application.DTOs {
    public class EntregadorCreateDto {
        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string Numero_CNH { get; set; }
        [RegularExpression(@"^(A|B|A+B)$", ErrorMessage = "Tipo_CNH must be 'A', 'B' or 'A+B'")]
        public string Tipo_CNH { get; set; }
        public string Imagem_CNH { get; set; }
    }
}
