using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomersAPI.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly DataDBContext _context;
        private ILogger<CustomersController> _logger;

        public CustomersController(DataDBContext context, ILogger<CustomersController> logger)
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
                _context.Customers.Select(customer => new {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    Company = customer.Company,
                    Email = customer.Email,
       


                })
            );


            //return Ok(await _context.Products.ToListAsync());

        }



        [HttpGet("{id}")]
        public async Task<ActionResult<List<Customer>>> Get(int CustomerId)
        {

            var customer = await _context.Customers.FindAsync(CustomerId);


            if (customer == null)
                return BadRequest("No se encontro Customer");

            return Ok(customer);

        }



        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer c)
        {
            _context.Customers.Add(c);

            await _context.SaveChangesAsync();
            return Ok(await _context.Customers.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateProduct(Customer c)
        {
            var dbCustomer = await _context.Customers.FindAsync(c.CustomerId);
            if (dbCustomer == null)
                return BadRequest("No se encontro el Customer");

            dbCustomer.FirstName = c.FirstName;
            dbCustomer.LastName = c.LastName;
            dbCustomer.Phone = c.Phone;
            dbCustomer.Address = c.Address;
            dbCustomer.Company = c.Company;
            dbCustomer.Email = c.Email;





            await _context.SaveChangesAsync();
            return Ok(await _context.Customers.ToListAsync());



        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Customer>>> DeleteProduct(int id)
        {

            var dbCustomers = await _context.Customers.FindAsync(id);

            if (dbCustomers == null)
                return BadRequest("No se encontro el producto");



            _context.Customers.Remove(dbCustomers);

            await _context.SaveChangesAsync();

            //imprime el estado y el json
            return Ok(await _context.Customers.ToListAsync());

        }



    







}
}
