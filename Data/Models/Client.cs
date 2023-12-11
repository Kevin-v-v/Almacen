using System;
using System.Collections.Generic;

namespace Almacen.Data.Models;

public partial class Client
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
