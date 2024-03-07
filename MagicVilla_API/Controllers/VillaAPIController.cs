using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogger<VillaAPIController> _logger;
        private readonly ApplicationDbContext _db;

        public VillaAPIController(ApplicationDbContext db)
        {
            this._db = db;
        }

        [HttpGet]
        [Route("Get")]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            //_logger.LogInformation("Getting all the Villas ");
            return Ok(_db.villas.ToList());

        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        //[Route("Get by ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetbyId(int id)
        {
            if (id == 0)
            {
                //_logger.LogError("Get Villa Error with Id" + id);
                return BadRequest();
            }
            var villa = _db.villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        //[Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villadto)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (_db.villas.FirstOrDefault(i => i.Name.ToLower() == villadto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exists");
                return BadRequest(ModelState);

            }

            if (villadto == null)
            {
                return BadRequest(villadto);
            }
            if (villadto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Villa model = new()
            {
                Name = villadto.Name,
                Details = villadto.Details,
                Rate = villadto.Rate,
                Occupancy = villadto.Occupancy,
                Sqft = villadto.Sqft,
                ImageUrl = villadto.ImageUrl,
                Amenity = villadto.Amenity,
            };
            _db.villas.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = villadto.Id }, villadto);
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = _db.villas.FirstOrDefault(i => i.Id == id);
            if (villa == null)
            {
                return NotFound();

            }

            _db.villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villadto)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            Villa model = new()
            {
                Name = villadto.Name,
                Details = villadto.Details,
                Rate = villadto.Rate,
                Occupancy = villadto.Occupancy,
                Sqft = villadto.Sqft,
                ImageUrl = villadto.ImageUrl,
                Amenity = villadto.Amenity,
            };
            _db.villas.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
        //[HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]

        //public IActionResult UpdatePartialVilla(int id , JsonPatchDocument<VillaDTO> patchDTO)
        //{
        //    if(patchDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    var villa = _db.villas.FirstOrDefault(i => i.Id == id);
        //    if (villa == null)
        //    {
        //        return BadRequest();
        //    }

        //    patchDTO.ApplyTo(villa, ModelState);
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    return NoContent();
        //}






    }
}

