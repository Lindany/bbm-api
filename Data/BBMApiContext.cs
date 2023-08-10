using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BBMApi.Model;

namespace BBMApi.Data
{
    public class BBMApiContext : DbContext
    {
        public BBMApiContext (DbContextOptions<BBMApiContext> options)
            : base(options)
        {
        }

        public DbSet<BBMApi.Model.Church> Church { get; set; } = default!;

        public DbSet<BBMApi.Model.Leader> Leader { get; set; } = default!;

        public DbSet<BBMApi.Model.Person> Person { get; set; } = default!;

        public DbSet<BBMApi.Model.User> User { get; set; } = default!;

        public DbSet<BBMApi.Model.Stats> Stats { get; set; } = default!;
    }
}
