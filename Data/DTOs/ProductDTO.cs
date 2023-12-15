namespace Almacen.Data.DTOs;

public class ProductDTO{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public double PrecioCompra { get; set; }

    public double PrecioVenta { get; set; }

    public int CantidadStock { get; set; }

    public CategoryDTO? Categoria { get; set; }

}