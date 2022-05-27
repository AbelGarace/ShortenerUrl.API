using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShortenerUrl.Api.ApplicationCore.Entities;
using ShortenerUrl.Api.Infrastructure.Identity;

namespace ShortenerUrl.Api.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Link>(ConfigureLink);

            base.OnModelCreating(builder);
        }

        private static void ConfigureLink(EntityTypeBuilder<Link> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(x => x.ShortId)
               .HasMaxLength(20)
               .UseCollation("SQL_Latin1_General_CP1_CS_AS");
            builder.Property(l => l.Clicks).IsRequired().HasDefaultValue(0);
            builder.Property(l => l.LastAccessDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(l => l.ShortId).IsRequired();
            builder.Property(l => l.Url).IsRequired().HasMaxLength(500);
            builder.HasIndex(l => l.ShortId).IsUnique();
            builder.Property(l=>l.RequestId).IsRequired(); 
        }
    }
}
