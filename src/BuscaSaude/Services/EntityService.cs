using System;
using BuscaSaude.Data;
using BuscaSaude.Models;
using Microsoft.EntityFrameworkCore;

namespace BuscaSaude.Services;

public class EntityService
{
    private readonly DatabaseContext dbContext;

    public EntityService(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Unidade>> GetAll()
    {
        return await dbContext.Unidades!.ToListAsync();
    }

    public async Task<List<Unidade>> Contains(string text)
    {
        var unidades = dbContext.Unidades!.Where(u => u.Nome!.Contains(text));
        return await unidades.ToListAsync();
    }

    public async Task<List<Unidade>> GetByUf(int uf)
    {
        var unidades = dbContext.Unidades!.Where(u => u.Uf.Equals(uf));
        return await unidades.ToListAsync();
    }

    public async Task<List<Unidade>> GetByIbge(string ibge)
    {
        var unidades = dbContext.Unidades!.Where(u => u.Ibge!.Equals(ibge));
        return await unidades.ToListAsync();
    }

    public async Task<Unidade?> GetByCnes(string cnes)
    {
        var unidade = await dbContext.Unidades!.SingleOrDefaultAsync(
            u => u.Cnes!.Equals(cnes));
        return unidade;
    }
}