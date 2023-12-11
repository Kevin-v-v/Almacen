using System;
using System.Collections.Generic;

namespace Almacen.Data.Models;

public partial class Restock
{
    public int IdResurtido { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Product IdProductoNavigation { get; set; } = null!;
}
