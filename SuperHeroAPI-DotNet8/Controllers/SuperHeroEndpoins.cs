using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet8.Data;
using SuperHeroAPI_DotNet8.Entities;

namespace SuperHeroAPI_DotNet8.Controllers;

public static class SuperHeroEndpoins
{
    public static void MapSuperHeroEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/superhero");


        group.MapGet("", GetAllHeroes);

        group.MapGet("{id}", GetHero);

        group.MapPost("", AddHero).WithName(nameof(AddHero));

        group.MapPut("", UpdateHero).WithName(nameof(UpdateHero));

        group.MapDelete("", DeleteHero).WithName(nameof(DeleteHero));
    }

    public static async Task<IResult> GetAllHeroes(DataContext context)
    {
        var heroes = await context.SuperHeroes.ToListAsync();

        return Results.Ok(heroes);
    }

    public static async Task<IResult> GetHero(int id, DataContext context)
    {
        var hero = await context.SuperHeroes.FindAsync(id);
        if (hero == null)
            return Results.NotFound("Hero not found");

        return Results.Ok(hero);
    }

    public static async Task<IResult> AddHero(SuperHero hero, DataContext context)
    {
        context.SuperHeroes.Add(hero);
        await context.SaveChangesAsync();

        return Results.Ok(await context.SuperHeroes.ToListAsync());
    }

    public static async Task<IResult> UpdateHero(SuperHero updatedHero, DataContext context)
    {
        var dbHero = await context.SuperHeroes.FindAsync(updatedHero.Id);
        if (dbHero == null)
            return Results.NotFound("Hero not found");

        dbHero.Name = updatedHero.Name;
        dbHero.FirstName = updatedHero.FirstName;
        dbHero.LastName = updatedHero.LastName;
        dbHero.Place = updatedHero.Place;

        await context.SaveChangesAsync();

        return Results.Ok(await context.SuperHeroes.ToListAsync());
    }

    public static async Task<IResult> DeleteHero(int id, DataContext context)
    {
        var dbHero = await context.SuperHeroes.FindAsync(id);
        if (dbHero == null)
            return Results.NotFound("Hero not found");

        context.SuperHeroes.Remove(dbHero);
        await context.SaveChangesAsync();

        return Results.Ok(await context.SuperHeroes.ToListAsync());
    }
}