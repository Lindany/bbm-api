using Microsoft.EntityFrameworkCore;
using BBMApi.Data;
using BBMApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace BBMApi;

public static class PersonEndpoints
{
    public static void MapPersonEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Person").WithTags(nameof(Person));

        group.MapGet("/", async (BBMApiContext db) =>
        {
            return await db.Person.ToListAsync();
        })
        .WithName("GetAllPeople")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Person>, NotFound>> (int personid, BBMApiContext db) =>
        {
            return await db.Person.AsNoTracking()
                .FirstOrDefaultAsync(model => model.personId == personid)
                is Person model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPersonById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int personid, Person person, BBMApiContext db) =>
        {
            var affected = await db.Person
                .Where(model => model.personId == personid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.address, person.address)
                  .SetProperty(m => m.comments, person.comments)
                  .SetProperty(m => m.contactNumber, person.contactNumber)
                  .SetProperty(m => m.gender, person.gender)
                  .SetProperty(m => m.maritalStatus, person.maritalStatus)
                  .SetProperty(m => m.name, person.name)
                  .SetProperty(m => m.surname, person.surname)
                  .SetProperty(m => m.churchId, person.churchId)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePerson")
        .WithOpenApi();

        group.MapPost("/", async (Person person, BBMApiContext db) =>
        {
            db.Person.Add(person);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Person/{person.personId}",person);
        })
        .WithName("CreatePerson")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int personid, BBMApiContext db) =>
        {
            var affected = await db.Person
                .Where(model => model.personId == personid)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePerson")
        .WithOpenApi();
    }
}
