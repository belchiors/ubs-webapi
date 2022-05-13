using BuscaSaude.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuscaSaude.Controllers;

[ApiController]
[Route("api/entities")]
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
        var entities = await service.GetByUf(uf);
        return (entities.Any())
            ? Ok(entities)
            : NotFound("Unidade não encontrada");
    }

    [HttpGet]
    [Route("ibge/{ibge:int}")]
    public async Task<IActionResult> GetByIbge(string ibge)
    {
        var entities = await service.GetByIbge(ibge);
        return (entities.Any())
            ? Ok(entities)
            : NotFound("Unidade não encontrada");
    }

    [HttpGet]
    [Route("cnes/{cnes}")]
    public async Task<IActionResult> GetByCnes(string cnes)
    {
        var entity = await service.GetByCnes(cnes);
        return (entity != null)
            ? Ok(entity)
            : NotFound("Unidade não encontrada");
    }
}