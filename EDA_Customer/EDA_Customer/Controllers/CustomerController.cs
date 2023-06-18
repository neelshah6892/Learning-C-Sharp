using EDA_Customer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDA_Customer.Controllers;

[ApiController]
[Route(template:"[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerDbContext _customerDbContext;

    public CustomerController(CustomerDbContext customerDbContext)
    {
        _customerDbContext = customerDbContext;
    }

    [HttpGet]
    [Route(template: "/customers")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        return await _customerDbContext.Customers.ToListAsync();
    }

    [HttpGet]
    [Route(template: "/products")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetProduct()
    {
        return await _customerDbContext.Customers.ToListAsync();
    }
    
    
    [HttpPost]
    public async Task PostCustomer(Customer customer)
    {
        _customerDbContext.Customers.Add(customer);
        await _customerDbContext.SaveChangesAsync();
    }
}