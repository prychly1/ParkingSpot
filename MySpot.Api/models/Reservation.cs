namespace MySpot.Api.models
{
    public class Reservation
    {

        public int Id { get; set; }

        public string EmployeeName { get; set; }

        public string ParkingSpotNAme { get; set; }

        public string LicensePlate { get; set; }

        public DateTime date { get; set; }
    }
        
}
