namespace Almacen.Data.DTOs;

public class SaleDTO
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public double PrecioVenta { get; set; }

    public double Total { get; set; }

    public String? Fecha { get; set; }

}