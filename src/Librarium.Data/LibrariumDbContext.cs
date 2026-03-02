using Librarium.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data;

public class LibrariumDbContext : DbContext {
    public LibrariumDbContext(DbContextOptions<LibrariumDbContext> options) : base(options){}

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<Author> Authors => Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Member>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Book>()
            .HasIndex(x => x.ISBN)
            .IsUnique();

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Member)
            .WithMany(m => m.Loans)
            .HasForeignKey(l => l.MemberId);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId);

            modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(j =>
                j.ToTable("BookAuthors"));
    }
}