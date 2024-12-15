namespace BikeRentalApp.Domain.Events {
    public class MotoCreatedEvent {
        public string Identificador { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
