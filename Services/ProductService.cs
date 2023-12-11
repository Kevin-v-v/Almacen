using Almacen.Data.Models;
using Almacen.Data;
using Microsoft.EntityFrameworkCore;
using Almacen.Data.DTOs;
namespace Almacen.Services;

public class ProductService
{
    private readonly StoreContext _context;

    public ProductService(StoreContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }
    public async Task<bool> CheckStock(int id, int cantidad)
    {
        Product? product = await GetProductById(id);
        if (product == null)
        {
            return false;
        }
        return product.CantidadStock >= cantidad;
    }
    public async Task<bool> UpdateStock(StockModificationDTO stockUpdate)
    {
        Product? product = await GetProductById(stockUpdate.IdProducto);
        int result;
        switch (stockUpdate.TipoModificacion)
        {
            case TipoModificacion.sustraccion:

                if (await CheckStock(stockUpdate.IdProducto, stockUpdate.Cantidad))
                {
                    product.CantidadStock -= stockUpdate.Cantidad;
                    result = await _context.SaveChangesAsync();
                    return result > 0;
                }
                else
                {
                    return false;
                }
                break;
            case TipoModificacion.adicion:

                product.CantidadStock += stockUpdate.Cantidad;
                result = await _context.SaveChangesAsync();
                return result > 0;
            break;

            default:
                return false;
        }
    }
    public async Task<Product> GetProductById(int id)
    {
        return await _context.Products.FindAsync(id);
    }

}