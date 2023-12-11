namespace Almacen.Data.DTOs;

public class ProductDailyReportDTO{

    public int IdProducto {get; set;}
    public string? NombreProducto {get; set;}
    public double PrecioCompra {get; set;}
    public double PrecioVenta {get; set;}
    public int CantidadVendida {get; set;}
    public double Ganancia {get; set;}
}