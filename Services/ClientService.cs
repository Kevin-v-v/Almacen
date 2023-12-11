using Almacen.Data.Models;
using Almacen.Data;
using Microsoft.EntityFrameworkCore;
namespace Almacen.Services;

public class ClientService{
    private readonly StoreContext _context;

    public ClientService(StoreContext context){
        _context = context;
    }
    
    public async Task<IEnumerable<Client>> GetAllClients(){
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client> GetClientById(int id){
        return await _context.Clients.FindAsync(id);
    }

}