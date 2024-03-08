using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            this._db = db;

        }

        [HttpGet]
        [Route("Get")]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            return Ok(_db.products.ToList());
        }

        [HttpGet("{id:int}", Name = "Get Product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<ProductDTO> GetProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }

            var product = _db.products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductDTO productdto)
        {
            if (_db.products.FirstOrDefault(i => i.Product_Name.ToLower() == productdto.Product_Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Product already exists");
                return BadRequest(ModelState);
            }
            if (productdto == null)
            {
                return BadRequest(productdto);
            }

            if (productdto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Product model = new()
            {
                Product_Name = productdto.Product_Name,
                Product_Details = productdto.Product_Details,
                Product_Rate = productdto.Product_Rate,
                ImageUrl = productdto.ImageUrl,
            };
            _db.products.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetProduct", new { id= productdto.Id }, productdto);
            




    }
    }
}
