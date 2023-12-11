namespace Almacen.Data.DTOs;

public class StockModificationDTO{
    public int IdProducto {get; set;}
    public int Cantidad {get; set;}
    public TipoModificacion TipoModificacion {get; set;}
}