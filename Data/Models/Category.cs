using System;
using System.Collections.Generic;

namespace Almacen.Data.Models;

public partial class Category
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int PuntoSurtir { get; set; }

    public int CantidadSurtir { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
