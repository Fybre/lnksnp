using lnksnp.Models;
using Microsoft.EntityFrameworkCore;

namespace lnksnp.Context
{
    public class LinkSnipContext : DbContext
    {
        public LinkSnipContext(DbContextOptions options) : base(options) { }

        public DbSet<LinkSnip> LinkSnips { get; set; }

        public DbSet<LinkClick> LinkClicks { get; set; }
    }
}
