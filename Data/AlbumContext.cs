using Microsoft.EntityFrameworkCore;
using RecordsMvcEf.Models;

namespace RecordsMvcEf.Data {
    public class AlbumContext : DbContext
    {
    public AlbumContext(DbContextOptions<AlbumContext> options): base(options){}
    protected override void OnModelCreating(ModelBuilder builder)
    {
    builder.Entity<Artist>()
        .HasIndex(u => u.ArtistName)
        .IsUnique();
    }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Loan> Loans { get; set; }
    }
}