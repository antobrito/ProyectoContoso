using Contoso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace ProductsAPI.Controllers
{


    [Route("api/v1/[controller]")]
    [ApiController]
   
        public class ProductsController : ControllerBase
        {
            private readonly DataDBContext _context;
            private ILogger<ProductsController> _logger;

            public ProductsController(DataDBContext context, ILogger<ProductsController> logger)
            {

                _context = context;
                _logger = logger;

                _logger.LogInformation("DI correcto");
            }




            [HttpGet]
            //   public async Task<ActionResult<List<Product>>> Get()
            public ActionResult Get()
            {

                _logger.LogInformation("Llamado del GET ejecutandose");


                // lambda
                return Ok(
                    _context.Products.Select(product => new
                    {
                        id = product.ProductId,
                        Name = product.Name,
                        ListPrice = product.ListPrice,

                    })
                );


                //return Ok(await _context.Products.ToListAsync());

            }



            [HttpGet("{id}")]
            public async Task<ActionResult<List<Product>>> Get(int id)
            {

                var product = await _context.Products.FindAsync(id);


                if (product == null)
                    return BadRequest("No se encontro Producto");

                return Ok(product);

            }



            [HttpPost]
            public async Task<ActionResult<List<Product>>> AddProduct(Product p)
            {
                _context.Products.Add(p);

                await _context.SaveChangesAsync();
                return Ok(await _context.Products.ToListAsync());
            }


            [HttpPut]
            public async Task<ActionResult<List<Product>>> UpdateProduct(Product p)
            {
                var dbProduct = await _context.Products.FindAsync(p.ProductId);
                if (dbProduct == null)
                    return BadRequest("No se encontro el porducto");

                dbProduct.Name = p.Name;
                dbProduct.Color = p.Color;
                dbProduct.ListPrice = p.ListPrice;
                dbProduct.DaysToManufacture = p.DaysToManufacture;
                dbProduct.StandardCost = p.StandardCost;

                await _context.SaveChangesAsync();
                return Ok(await _context.Products.ToListAsync());



            }


            [HttpDelete("{id}")]
            public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
            {

                var dbProduct = await _context.Products.FindAsync(id);

                if (dbProduct == null)
                    return BadRequest("No se encontro el producto");



                _context.Products.Remove(dbProduct);

                await _context.SaveChangesAsync();

                //imprime el estado y el json
                return Ok(await _context.Products.ToListAsync());

            }

        }
    }


        
