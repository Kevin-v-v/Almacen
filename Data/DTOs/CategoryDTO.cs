namespace Almacen.Data.DTOs;

public class CategoryDTO{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int PuntoSurtir { get; set; }

    public int CantidadSurtir { get; set; }
}