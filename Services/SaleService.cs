using Almacen.Data.Models;
using Almacen.Data;
using Microsoft.EntityFrameworkCore;
using Almacen.Data.DTOs;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Almacen.Services;

public class SaleService{
    private readonly StoreContext _context;
    private readonly ProductService _productService;
    private readonly ClientService _clientService;

    public SaleService(StoreContext context, ProductService productService, ClientService clientService){
        _context = context;
        _productService = productService;
        _clientService = clientService;
    }
    
    public async Task<IEnumerable<Sale>> GetAllSales(){
        return await _context.Sales.ToListAsync();
    }

    public async Task<bool> RegisterSale(Sale sale){

        if(!await _productService.CheckStock((int)sale.IdProducto!, sale.Cantidad) || (await _clientService.GetClientById(sale.IdCliente) == null))
        {
            return false;
        }

        StockModificationDTO stockModification = new ();
        stockModification.IdProducto = (int)sale.IdProducto;
        stockModification.Cantidad = sale.Cantidad;
        stockModification.TipoModificacion = TipoModificacion.sustraccion;

        var result = await _productService.UpdateStock(stockModification);
        

        await _context.Sales.AddAsync(sale);
        var saveResult = await _context.SaveChangesAsync();
        if(saveResult <= 0){
            stockModification.TipoModificacion = TipoModificacion.adicion;
            await _productService.UpdateStock(stockModification);
            return false;
        }

        return true;
    }

    public async Task<DailyReportDTO> DailyReport(){
        DateTime today = DateTime.Today.Date;

        DailyReportDTO report = new();

        Dictionary<int,int> ventasPorProducto = new();

        report.gananciaTotal = 0;
        report.Productos = new Collection<ProductDailyReportDTO>();

        var products = await _productService.GetAllProducts();
        foreach(Product product in products){
            ventasPorProducto.Add(product.IdProducto,0);
        }
        
        var ventas = _context.Sales.Where(sale => DateTime.Compare(((DateTime)sale.Fecha!).Date, today) == 0);
        foreach(Sale venta in ventas){
            report.gananciaTotal += (venta.IdProductoNavigation!.PrecioVenta-venta.IdProductoNavigation.PrecioCompra)*venta.Cantidad;
            ventasPorProducto[(int)venta.IdProducto!] += venta.Cantidad;
        }
        
        foreach(Product product in products){
            if(ventasPorProducto[product.IdProducto] == 0){
                continue;
            }
            ProductDailyReportDTO currentProductToReport = new()
            {
                IdProducto = product.IdProducto,
                NombreProducto = product.Nombre,
                PrecioVenta = product.PrecioVenta,
                PrecioCompra = product.PrecioCompra,
                CantidadVendida = ventasPorProducto[product.IdProducto],
                Ganancia = (product.PrecioVenta - product.PrecioCompra) * ventasPorProducto[product.IdProducto]
            };
            report.Productos.Add(currentProductToReport);
        }

        return report;

    }
}