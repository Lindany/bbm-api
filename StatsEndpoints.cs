using Microsoft.EntityFrameworkCore;
using BBMApi.Data;
using BBMApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace BBMApi;

public static class StatsEndpoints
{
    public static void MapStatsEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Stats").WithTags(nameof(Stats));

        group.MapGet("/", async (BBMApiContext db) =>
        {
            return await db.Stats.ToListAsync();
        })
        .WithName("GetAllStats")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Stats>, NotFound>> (int statsid, BBMApiContext db) =>
        {
            return await db.Stats.AsNoTracking()
                .FirstOrDefaultAsync(model => model.statsId == statsid)
                is Stats model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStatsById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int statsid, Stats stats, BBMApiContext db) =>
        {
            var affected = await db.Stats
                .Where(model => model.statsId == statsid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.adult, stats.adult)
                  .SetProperty(m => m.car, stats.car)
                  .SetProperty(m => m.fk, stats.fk)
                  .SetProperty(m => m.saved, stats.saved)
                  .SetProperty(m => m.offering, stats.offering)
                  .SetProperty(m => m.visitors, stats.visitors)
                  .SetProperty(m => m.date, stats.date)
                  .SetProperty(m => m.churchId, stats.churchId)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateStats")
        .WithOpenApi();

        group.MapPost("/", async (Stats stats, BBMApiContext db) =>
        {
            db.Stats.Add(stats);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Stats/{stats.statsId}",stats);
        })
        .WithName("CreateStats")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int statsid, BBMApiContext db) =>
        {
            var affected = await db.Stats
                .Where(model => model.statsId == statsid)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteStats")
        .WithOpenApi();
    }
}
