using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence
{
    public class eventoContext : DbContext
    {
        public eventoContext(DbContextOptions<eventoContext> options) : base(options){}

        public DbSet<Evento> Eventos{ get; set; }
        public DbSet<Lote> Lotes{ get; set; }
        public DbSet<Palestrante> Palestrantes{ get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos{ get; set; }
        public DbSet<RedeSocial> RedesSociais{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PalestranteEvento>()
                .HasKey(x => new {x.EventoId, x.PalestranteId});
            
            builder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Palestrante>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}