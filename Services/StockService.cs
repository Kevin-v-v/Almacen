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
        if(restock.Cantidad <= 0){
            return false;
        }

        Product? product = await _productService.GetProductById(restock.Producto);

        if(product == null){
            return false;
        }

        StockModificationDTO stockModification = new()
        {
            IdProducto = restock.Producto,
            Cantidad = restock.Cantidad,
            TipoModificacion = TipoModificacion.adicion
        };

        var productResult = await _productService.UpdateStock(stockModification);
        if(!productResult)
        {
            return false;
        }

        Restock restockToDb = new()
        {
            IdProducto = product.IdProducto,
            Cantidad = restock.Cantidad,
            Fecha = restock.Fecha
        };

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
        return await _context.Products.Include(product => product.CategoriaNavigation)
        .Where(product => product.CantidadStock <= product.CategoriaNavigation!.PuntoSurtir).ToListAsync();
    }

    public async Task<IEnumerable<Restock>> GetAllRestocks(){
        return await _context.Restocks.ToListAsync();
    }

}