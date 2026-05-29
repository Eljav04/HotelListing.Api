using HotelListing.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private static List<Hotel> _hotels = new()
        {
            new Hotel() { Id = 1, Name = "Avanguard", Address = "12, Main Street", Rating = 4.5 },
            new Hotel() { Id = 2, Name = "Mono", Address = "53, Jacob Street", Rating = 4.8 },
        };
        
        // GET: api/<HotelsController>
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get()
        {
            return Ok(_hotels);
        }

        // GET api/<HotelsController>/5
        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            Hotel? searchedHotel =  _hotels.FirstOrDefault(h => h.Id == id);
            if(searchedHotel == null) return NotFound();
            
            return Ok(searchedHotel);
        }
 
        // POST api/<HotelsController>
        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel newHotel)
        {
            if (_hotels.Any(h => h.Id == newHotel.Id))
            {
                return BadRequest("Hotel with  that id already exists");
            }

            _hotels.Add(newHotel);
            return CreatedAtAction(nameof(Get), new { id = newHotel.Id }, newHotel);
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Hotel updatedHotel)
        {
            var existingHotel = _hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel != null) return NotFound();
            
            existingHotel?.Name = updatedHotel.Name;
            existingHotel?.Address = updatedHotel.Address;
            existingHotel?.Rating = updatedHotel.Rating;

            return NoContent();
        }

        // DELETE api/<HotelsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var hotel = _hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null) return NotFound("Hotel with  that id doesn't exists");

            return NoContent();
        }
        
    }

