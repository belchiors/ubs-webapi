using System.Globalization;
using System.Net;
using BuscaSaude.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuscaSaude.Controllers;

[ApiController]
[Route("api/unidades")]
[Produces("application/json")]
public class UnidadeController : ControllerBase
{
    private readonly ILogger<UnidadeController> _logger;
    private readonly DatabaseContext _database;

    public UnidadeController(ILogger<UnidadeController> logger, DatabaseContext database)
    {
        _logger = logger;
        _database = database;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var unidades = await _database.Unidades!.ToListAsync();
            if (!unidades.Any())
            {
                return NotFound("Unidade não encontrada");
            }
            return Ok(unidades);
        }
        catch(Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
    }

    [HttpGet]
    [Route("search/{query}")]
    public IActionResult SearchFilter(string? query)
    {
        var unidades = _database.Unidades!.AsQueryable();
        if (!string.IsNullOrEmpty(query))
        {
            var result = unidades.Where(u => u.Nome!.Contains(query));
            return result.Any() ? Ok(result) : NotFound("Unidade não encontrada");
        }
        return NotFound("Unidade não encontrada");
    }

    [HttpGet]
    [Route("uf/{uf:int}")]
    public async Task<IActionResult> GetByUf(int uf)
    {
        try
        {
            var unidades = await _database.Unidades!.Where(u => u.Uf.Equals(uf)).ToListAsync();
            if (!unidades.Any())
            {
                return NotFound("Unidade não encontrada");
            }
            return Ok(unidades);
        }
        catch(Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("ibge/{ibge:int}")]
    public async Task<IActionResult> GetByIbge(int ibge)
    {
        try
        {
            var unidades = await _database.Unidades!.Where(u => u.Ibge!.Equals(ibge)).ToListAsync();
            if (!unidades.Any())
            {
                return NotFound("Unidade não encontrada");
            }
            return Ok(unidades);
        }
        catch(Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("cnes/{cnes}")]
    public async Task<IActionResult> GetByCnes(string cnes)
    {
        try
        {
            var unidade = await _database.Unidades!.SingleOrDefaultAsync(u => u.Cnes!.Equals(cnes));
            if (unidade == null)
            { 
                return NotFound("Unidade não encontrada");
            }
            return Ok(unidade);
        }
        catch(Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}