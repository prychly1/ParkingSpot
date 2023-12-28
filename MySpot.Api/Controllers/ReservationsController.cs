using Microsoft.AspNetCore.Mvc;
using MySpot.Api.models;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers


{
    [ApiController]
    [Route("reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationServices _service = new();



        [HttpGet("get")]
        public ActionResult<Reservation> Get() => Ok(_service.GetAll());

        [HttpGet("{_id:int}")]
        public ActionResult<Reservation> Get2(int id)
        {

            var resrervation = _service.Get(id);
            if(resrervation == null)
            {
                return NotFound();
            }
            return Ok(resrervation);
        }
     
        [HttpPost("apiPost")]
        public ActionResult Post (Reservation reservation) 
        {
            var id = _service.Create(reservation);
            if(id is null)
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(Get),new {id = reservation.Id}, null);

        }

        [HttpPut("{id:int}")]

        public ActionResult Put(Reservation reservation, int id)
        {
            if(_service.Update(id, reservation))
            {
                return NoContent();
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            
            _service.Delete(id);
            return NoContent();


        }
    }
}
