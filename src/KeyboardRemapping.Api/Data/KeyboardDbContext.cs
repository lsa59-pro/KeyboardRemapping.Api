using KeyboardRemapping.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyboardRemapping.Api.Data;

/// <summary>
/// Classe d'accés à la base de données
/// </summary>
public class KeyboardDbContext : DbContext
{
    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="options"></param>
    public KeyboardDbContext(DbContextOptions<KeyboardDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    ///  Liste des claviers
    /// </summary>
    public DbSet<Keyboard> Keyboards => Set<Keyboard>();

    /// <summary>
    /// Liste des touches des claviers
    /// </summary>
    public DbSet<KeyboardKey> KeyboardKeys => Set<KeyboardKey>();

    /// <summary>
    /// Liste des mappings de touches
    /// </summary>
    public DbSet<KeyMapping> KeyMappings => Set<KeyMapping>();

    /// <summary>
    /// Configuration des entités et définition de leur contraintes (Clé primaire, supression)
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Le nom d'un clavier doit être unique
        modelBuilder.Entity<Keyboard>()
            .HasIndex(x => x.Name)
            .IsUnique();

        // Une même touche ne peut pas avoir deux fois le même code HID sur un clavier
        modelBuilder.Entity<KeyboardKey>()
            .HasIndex(x => new
            {
                x.KeyboardId,
                x.HidUsageCode
            })
            .IsUnique();

        // Une touche ne peut avoir qu'un seul mapping sur un clavier
        modelBuilder.Entity<KeyMapping>()
            .HasIndex(x => new
            {
                x.KeyboardId,
                x.SourceKeyId
            })
            .IsUnique();

        // Suppression du clavier : suppression de ses touches
        modelBuilder.Entity<KeyboardKey>()
            .HasOne(x => x.Keyboard)
            .WithMany(x => x.Keys)
            .HasForeignKey(x => x.KeyboardId)
            .OnDelete(DeleteBehavior.Cascade);

        // Suppression du clavier : suppression de ses mappings
        modelBuilder.Entity<KeyMapping>()
            .HasOne(x => x.Keyboard)
            .WithMany()
            .HasForeignKey(x => x.KeyboardId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Précise à EF Core quelle clé étrangère correspond à chaque navigation
        modelBuilder.Entity<KeyMapping>()
            .HasOne(x => x.SourceKey)
            .WithMany(x => x.SourceMappings)
            .HasForeignKey(x => x.SourceKeyId);

        modelBuilder.Entity<KeyMapping>()
            .HasOne(x => x.TargetKey)
            .WithMany(x => x.TargetMappings)
            .HasForeignKey(x => x.TargetKeyId);
    }
}