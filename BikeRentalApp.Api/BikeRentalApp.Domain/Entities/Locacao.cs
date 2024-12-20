﻿using System.ComponentModel.DataAnnotations;

namespace BikeRentalApp.Domain.Entities {
    public class Locacao {
        [Key]
        public string Identificador { get; set; }
        public decimal Valor_Diaria { get; set; }
        public string Entregador_Id { get; set; }
        public string Moto_Id { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Termino { get; set; }
        public DateTime Data_Previsao_Termino { get; set; }
        public DateTime? Data_Devolucao { get; set; }
        public Entregador Entregador { get; set; }
        public Moto Moto { get; set; }
    }
}
