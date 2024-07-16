using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet8.Data;
using SuperHeroAPI_DotNet8.Entities;

namespace SuperHeroAPI_DotNet8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
    private readonly DataContext _context;

    public SuperHeroController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<SuperHero>), 200)]
    public async Task<IActionResult> GetAllHeroes()
    {
        var heroes = await _context.SuperHeroes.ToListAsync();

        return Ok(heroes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(List<SuperHero>), 200)]
    public async Task<IActionResult> GetHero(int id)
    {
        var hero = await _context.SuperHeroes.FindAsync(id);
        if (hero == null)
            return NotFound("Hero not found");

        return Ok(hero);
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<SuperHero>), 200)]
    public async Task<IActionResult> AddHero(SuperHero hero)
    {
        _context.SuperHeroes.Add(hero);
        await _context.SaveChangesAsync();

        return Ok(await _context.SuperHeroes.ToListAsync());
    }

    [HttpPut]
    [ProducesResponseType(typeof(List<SuperHero>), 200)]
    public async Task<IActionResult> UpdateHero(SuperHero updatedHero)
    {
        var dbHero = await _context.SuperHeroes.FindAsync(updatedHero.Id);
        if (dbHero == null)
            return NotFound("Hero not found");

        dbHero.Name = updatedHero.Name;
        dbHero.FirstName = updatedHero.FirstName;
        dbHero.LastName = updatedHero.LastName;
        dbHero.Place = updatedHero.Place;

        await _context.SaveChangesAsync();

        return Ok(await _context.SuperHeroes.ToListAsync());
    }

    [HttpDelete]
    [ProducesResponseType(typeof(List<SuperHero>), 200)]
    public async Task<IActionResult> DeleteHero(int id)
    {
        var dbHero = await _context.SuperHeroes.FindAsync(id);
        if (dbHero == null)
            return NotFound("Hero not found");

        _context.SuperHeroes.Remove(dbHero);
        await _context.SaveChangesAsync();

        return Ok(await _context.SuperHeroes.ToListAsync());
    }
}