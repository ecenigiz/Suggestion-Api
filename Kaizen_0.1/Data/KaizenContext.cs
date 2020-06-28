
using Kaizen_0._1.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen_0._1.Data
{
    public class KaizenContext : DbContext
    {
        public DbSet<Suggestion> Suggestions { get; set; }

        public KaizenContext(DbContextOptions options) : base(options)
        { }

    }
}
