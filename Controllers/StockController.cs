using Almacen.Services;
using Microsoft.AspNetCore.Mvc;
using Almacen.Data.DTOs;

namespace Almacen.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StockController: ControllerBase{

    private readonly StockService _stockService;

    public StockController(StockService stockService){
        _stockService = stockService;
    }

    [HttpGet("GetProductsForRestock")]
    public async Task<IActionResult> GetProductsForRestock(){
        var products = await _stockService.GetProductsForRestock();
        var result = products.Select(product => new ProductDTO(){
            Id = product.IdProducto,
            Nombre = product.Nombre,
            Descripcion = product.Descripción,
            PrecioCompra = product.PrecioCompra,
            PrecioVenta = product.PrecioVenta,
            CantidadStock = product.CantidadStock,
            Categoria = new CategoryDTO(){
                Id = product.CategoriaNavigation!.IdCategoria,
                Nombre = product.CategoriaNavigation.Nombre,
                Descripcion = product.CategoriaNavigation.Descripcion,
                PuntoSurtir = product.CategoriaNavigation.PuntoSurtir,
                CantidadSurtir = product.CategoriaNavigation.CantidadSurtir
            }
        });
        return Ok(result);
    }

    [HttpPost("Restock")]
    public async Task<IActionResult> Restock([FromBody] RestockEventDTO restock){
        restock.Fecha = DateTime.Now;
        var result = await _stockService.Restock(restock);
        
        return result ? Ok() : StatusCode(500);

    }
    
}