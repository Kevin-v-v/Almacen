using Almacen.Data.Models;
using Almacen.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Almacen.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SalesController: ControllerBase{
    private readonly ClientService _clientService;

    public SalesController(ClientService clientService){
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> getClients(){
        return Ok(await _clientService.GetAllClients());
    }

}