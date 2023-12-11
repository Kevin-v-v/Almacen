using Almacen.Data.Models;
using Almacen.Data;
using Microsoft.EntityFrameworkCore;
using Almacen.Data.DTOs;
using Almacen.Services;

namespace Almacen.Services;

public class StockService{
    private readonly StoreContext _context;
    private readonly ProductService _productService;

    public StockService(StoreContext context, ProductService productService){
        _context = context;
        _productService = productService;
    }
    
    public async Task<bool> Restock(RestockEventDTO restock)
    {
        if(restock.cantidad <= 0){
            return false;
        }

        Product? product = await _productService.GetProductById(restock.producto);

        if(product == null){
            return false;
        }

        StockModificationDTO stockModification = new ();
        stockModification.IdProducto = restock.producto;
        stockModification.Cantidad = restock.cantidad;
        stockModification.TipoModificacion = TipoModificacion.adicion;

        var productResult = await _productService.UpdateStock(stockModification);
        if(!productResult)
        {
            return false;
        }

        Restock restockToDb = new();
        restockToDb.IdProducto = product.IdProducto;
        restockToDb.Cantidad  = restock.cantidad;
        restockToDb.Fecha = restock.fecha;

        var result = await _context.Restocks.AddAsync(restockToDb);
        var saveResult = await _context.SaveChangesAsync();
        
        if(saveResult <= 0){
            stockModification.TipoModificacion = TipoModificacion.sustraccion;
            await _productService.UpdateStock(stockModification);
            return false;
        }
        return saveResult > 0;

    }

    public async Task<IEnumerable<Product>> GetProductsForRestock(){
        return _context.Products.Include(product => product.CategoriaNavigation)
        .Where(product => product.CategoriaNavigation.PuntoSurtir <= product.CantidadStock);
    }

    public async Task<IEnumerable<Restock>> GetAllRestocks(){
        return await _context.Restocks.ToListAsync();
    }

}