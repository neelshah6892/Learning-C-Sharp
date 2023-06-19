using System.Text.Json.Serialization;
using EDA_Inventory.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EDA_Inventory.Controllers;

[ApiController]
[Route(template:"[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductDbContext _productDbContext;

    public ProductController(ProductDbContext productDbContext)
    {
        _productDbContext = productDbContext;
    }

    [HttpGet]
    public List<Products> GetProducts()
    {
        return _productDbContext.Products.ToList();
    }

    [HttpPut]
    public async Task<ActionResult<Products>> UpdateProduct(Products products)
    {
        _productDbContext.Products.Update(products);

        await _productDbContext.SaveChangesAsync();

        var product = JsonSerializer.Serialize(new
        {
            products.Id,
            NewName = products.Name,
            products.Quantity
        });
        return CreatedAtAction("GetProducts", routeValues: new { products.Id }, value: products);
    }

    [HttpPost]
    public async Task<ActionResult<Products>> PostProduct(Products products)
    {
        _productDbContext.Products.Add(products);

        await _productDbContext.SaveChangesAsync();

        var product = JsonSerializer.Serialize(new
        {
            products.Id,
            products.ProductId,
            products.Name,
            products.Quantity
        });

        return CreatedAtAction("GetProducts", routeValues: new { products.Id }, value: products);
    }
}