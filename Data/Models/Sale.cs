using System;
using System.Collections.Generic;

namespace Almacen.Data.Models;

public partial class Sale
{
    public int IdVenta { get; set; }

    public int IdCliente { get; set; }

    public int Cantidad { get; set; }

    public double PrecioVenta { get; set; }

    public double Total { get; set; }

    public DateTime? Fecha { get; set; }

    public int? IdProducto { get; set; }

    public virtual Client IdClienteNavigation { get; set; } = null!;

    public virtual Product? IdProductoNavigation { get; set; }
}
