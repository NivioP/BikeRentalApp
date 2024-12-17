using System.ComponentModel.DataAnnotations;

namespace BikeRentalApp.Application.DTOs {
    public class EntregadorUpdateCNHDto {
        [Required(ErrorMessage = "A imagem da CNH é obrigatória.")]
        [DataType(DataType.Text, ErrorMessage = "A imagem deve ser fornecida como Base64.")]
        public string Imagem_CNH { get; set; }
    }
}

