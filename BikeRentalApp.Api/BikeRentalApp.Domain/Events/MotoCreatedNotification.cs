namespace BikeRentalApp.Domain.Events {
    public class MotoCreatedNotification {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Identificador { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public DateTime DataNotificacao { get; set; }
    }
}
