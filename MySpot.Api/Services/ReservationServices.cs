using Microsoft.AspNetCore.Mvc.Infrastructure;
using MySpot.Api.models;

namespace MySpot.Api.Services
{
    public class ReservationServices
    {
        private static int _id;
        private static readonly List<Reservation> Reservations = new();
        private static readonly List<string> ParkingSpotName = new()
        {
            "P1", "P2", "P3", "P4", "P5"
        };

        public Reservation Get(int id)
            => Reservations.SingleOrDefault(x => x.Id == id);
          
        public IEnumerable<Reservation> GetAll() => Reservations;

        public int? Create(Reservation reservation)
        {
            if (ParkingSpotName.All(x => x != reservation.ParkingSpotNAme))
            {
                return default;
            }
            reservation.date = DateTime.UtcNow.AddDays(1).Date;
            var reservationAlreadyExists = Reservations.Any(x => x.ParkingSpotNAme == reservation.ParkingSpotNAme
            && x.date.Date == reservation.date.Date);

            if (reservationAlreadyExists)
            {

                return default;

            }

            reservation.Id = _id;
            _id++;
            Reservations.Add(reservation);
            return reservation.Id;

            
        }
        
        public bool Update(int id,Reservation reservation)
        {
            var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);
            if (existingReservation == null)
            {
                return false;
            }
            existingReservation.LicensePlate = reservation.LicensePlate;

            return true;
        }

        public bool Delete(int id)
        {
            var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);
            if (existingReservation == null)
            {
                return false;
            }

            Reservations.Remove(existingReservation);
            return true;

        }
    }
}
