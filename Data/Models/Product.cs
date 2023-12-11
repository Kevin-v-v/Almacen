using System;
using System.Collections.Generic;

namespace Almacen.Data.Models;

public partial class Product
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripción { get; set; } = null!;

    public double PrecioCompra { get; set; }

    public double PrecioVenta { get; set; }

    public int CantidadStock { get; set; }

    public int? Categoria { get; set; }

    public virtual Category? CategoriaNavigation { get; set; }

    public virtual ICollection<Restock> Restocks { get; set; } = new List<Restock>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
