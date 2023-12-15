using Almacen.Data.DTOs;
using Almacen.Data.Models;
using Almacen.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Almacen.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SalesController: ControllerBase{
    private readonly SaleService _saleService;
    private readonly ProductService _productService;

    public SalesController(SaleService saleService, ProductService productService){
        _saleService = saleService;
        _productService = productService;
    }
    [HttpPost("RegisterSale")]
    public async Task<IActionResult> RegisterSale([FromBody] SaleDTO sale){
        Product product = await _productService.GetProductById(sale.IdProducto);
        Sale saleToDb = new(){
            IdVenta = 0,
            IdCliente = sale.IdCliente,
            IdProducto = sale.IdProducto,
            Cantidad = sale.Cantidad,
            PrecioVenta = product.PrecioVenta,
            Total = sale.Cantidad * product.PrecioVenta,
            Fecha = DateTime.Now
        };
        var result = await _saleService.RegisterSale(saleToDb);

        return result ? Ok() : StatusCode(500);


    }
    [HttpGet("DailyReport")]
    public async Task<IActionResult> DailyReport(){
        var result = await _saleService.DailyReport();
        return Ok(result);
    }


}