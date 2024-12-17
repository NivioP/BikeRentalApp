using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRentalApp.Application.DTOs {
    public class LocacaoDto {
        public string Identificador { get; set; }
        public decimal Valor_Diaria { get; set; }
        public string Entregador_Id { get; set; }
        public string Moto_Id { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Termino { get; set; }
        public DateTime Data_Previsao_Termino { get; set; }
        public DateTime? Data_Devolucao { get; set; }
    }
}
