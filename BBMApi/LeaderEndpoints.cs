using Microsoft.EntityFrameworkCore;
using BBMApi.Data;
using BBMApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace BBMApi;

public static class LeaderEndpoints
{
    public static void MapLeaderEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Leader").WithTags(nameof(Leader));

        group.MapGet("/", async (BBMApiContext db) =>
        {
            return await db.Leader.ToListAsync();
        })
        .WithName("GetAllLeaders")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Leader>, NotFound>> (int leaderid, BBMApiContext db) =>
        {
            return await db.Leader.AsNoTracking()
                .FirstOrDefaultAsync(model => model.leaderId == leaderid)
                is Leader model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetLeaderById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int leaderid, Leader leader, BBMApiContext db) =>
        {
            var affected = await db.Leader
                .Where(model => model.leaderId == leaderid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.ministry, leader.ministry)
                  .SetProperty(m => m.office, leader.office)
                  .SetProperty(m => m.personId, leader.personId)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateLeader")
        .WithOpenApi();

        group.MapPost("/", async (Leader leader, BBMApiContext db) =>
        {
            db.Leader.Add(leader);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Leader/{leader.leaderId}",leader);
        })
        .WithName("CreateLeader")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int leaderid, BBMApiContext db) =>
        {
            var affected = await db.Leader
                .Where(model => model.leaderId == leaderid)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteLeader")
        .WithOpenApi();
    }
}
