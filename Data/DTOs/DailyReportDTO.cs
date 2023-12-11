namespace Almacen.Data.DTOs;

public class DailyReportDTO{
    public double gananciaTotal {get; set;}
    public ICollection<ProductDailyReportDTO>? Productos {get; set;}
}