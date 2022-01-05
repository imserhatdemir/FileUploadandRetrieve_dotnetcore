using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class imagedbcontext:DbContext
    {
        public imagedbcontext(DbContextOptions<imagedbcontext> options) : base(options)
        {

        }
        public DbSet<imagemodal> images { get; set; }
    }
}
