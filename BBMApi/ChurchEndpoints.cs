using Microsoft.EntityFrameworkCore;
using BBMApi.Data;
using BBMApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace BBMApi;

public static class ChurchEndpoints
{
    public static void MapChurchEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Church").WithTags(nameof(Church));

        group.MapGet("/", async (BBMApiContext db) =>
        {
            return await db.Church.ToListAsync();
        })
        .WithName("GetAllChurches")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Church>, NotFound>> (int churchid, BBMApiContext db) =>
        {
            return await db.Church.AsNoTracking()
                .FirstOrDefaultAsync(model => model.churchId == churchid)
                is Church model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetChurchById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int churchid, Church church, BBMApiContext db) =>
        {
            var affected = await db.Church
                .Where(model => model.churchId == churchid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.churchName, church.churchName)
                  .SetProperty(m => m.location, church.location)
                  .SetProperty(m => m.branch, church.branch)
                  .SetProperty(m => m.province, church.province)
                  .SetProperty(m => m.city, church.city)
                  .SetProperty(m => m.region, church.region)
                  .SetProperty(m => m.pastorId, church.pastorId)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateChurch")
        .WithOpenApi();

        group.MapPost("/", async (Church church, BBMApiContext db) =>
        {
            db.Church.Add(church);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Church/{church.churchId}",church);
        })
        .WithName("CreateChurch")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int churchid, BBMApiContext db) =>
        {
            var affected = await db.Church
                .Where(model => model.churchId == churchid)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteChurch")
        .WithOpenApi();
    }
}
