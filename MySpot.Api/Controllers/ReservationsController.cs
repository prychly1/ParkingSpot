using Microsoft.AspNetCore.Mvc;
using MySpot.Api.models;

namespace MySpot.Api.Controllers


{
    [ApiController]
    [Route("reservations")]
    public class ReservationsController : ControllerBase
    {
        private static int _id;
        private static readonly List<Reservation> _reservations = new();
        private static readonly List<string> _parkingSpotName = new()
        {
            "P1", "P2", "P3", "P4", "P5"
        };

        [HttpGet("get")]
        public ActionResult<Reservation> Get() => Ok(_reservations);

        [HttpGet("{_id:int}")]
        public ActionResult<Reservation> Get2(int id)
        {
            var reservation = _reservations.SingleOrDefault(x => x.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }
     
        [HttpPost("apiPost")]
        public ActionResult Post (Reservation reservation) 
        {
            if(_parkingSpotName.All(x=> x != reservation.ParkingSpotNAme))
            {
                return BadRequest();
            }
            reservation.date = DateTime.UtcNow.AddDays(1).Date;
            var reservationAlreadyExists = _reservations.Any(x => x.ParkingSpotNAme == reservation.ParkingSpotNAme
            && x.date.Date == reservation.date.Date);

            if(reservationAlreadyExists)
            {
                
                return BadRequest();

            }

            reservation.Id = _id;
            _id++;
            _reservations.Add(reservation);
            return CreatedAtAction(nameof(Get),new {id = reservation.Id}, null);

        }

        [HttpPut("{id:int}")]

        public ActionResult Put(Reservation reservation, int id)
        {
            var existingReservation = _reservations.SingleOrDefault(x => x.Id == id);
            if (existingReservation == null)
            {
                return NotFound();
            }
            existingReservation.LicensePlate = reservation.LicensePlate;

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var existingReservation = _reservations.SingleOrDefault(x => x.Id == id);
            if (existingReservation == null)
            {
                return NotFound();
            }
            
            _reservations.Remove(existingReservation);

            return NoContent();


        }
    }
}
