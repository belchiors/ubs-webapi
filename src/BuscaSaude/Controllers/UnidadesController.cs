using System.Globalization;
using System.Net;
using BuscaSaude.Data;
using BuscaSaude.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuscaSaude.Controllers;

[ApiController]
[Route("api/unidades")]
[Produces("application/json")]
public class UnidadeController : ControllerBase
{
    private readonly ILogger<UnidadeController> _logger;
    private readonly EntityService service;

    public UnidadeController(ILogger<UnidadeController> logger, EntityService service)
    {
        _logger = logger;
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string? q)
    {
        return (q != null && q!.Trim() != "")
            ? Ok(await service.Contains(q))
            : Ok(await service.GetAll());
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