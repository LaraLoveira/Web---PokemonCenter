using Microsoft.EntityFrameworkCore;

namespace LoveiraNoresLaraTarea4.Models.Context
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> options) : base(options)
        {
        }

        //DbSet
        public DbSet<EfectoSecundario> EfectoSecundario { get; set; }
        public DbSet<EstadisticasBase> EstadisticasBase { get; set; }
        public DbSet<EvolucionaDe> EvolucionaDe { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Movimiento> Movimiento { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
        public DbSet<TipoAtaque> TipoAtaque { get; set; }
        public DbSet<PokemonMovimientoForma> PokemonMovimientoForma { get; set; }
        public DbSet<FormaAprendizaje> FormaAprendizaje { get; set; }
        public DbSet<FormaEvolucion> FormaEvolucion { get; set; }
        public DbSet<MO> MO { get; set; }
        public DbSet<MovimientoEfectoSecundario> MovimientoEfectoSecundario { get; set; }
        public DbSet<MT> MT { get; set; }
        public DbSet<NivelAprendizaje> NivelAprendizaje { get; set; }
        public DbSet<NivelEvolucion> NivelEvolucion { get; set; }
        public DbSet<Piedra> Piedra { get; set; }
        public DbSet<PokemonFormaEvolucion> PokemonFormaEvolucion { get; set; }
        public DbSet<PokemonTipo> PokemonTipo { get; set; }
        public DbSet<TipoEvolucion> TipoEvolucion { get; set; }
        public DbSet<TipoFormaAprendizaje> TipoFormaAprendizaje { get; set; }
        public DbSet<TipoPiedra> TipoPiedra { get; set; }


        //Configuración de claves y relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //tabla "pokemon"
            modelBuilder.Entity<Pokemon>()
                .HasKey(p => p.PokemonId);

            modelBuilder.Entity<Pokemon>()
                .HasMany(p => p.PokemonMovimientoForma)
                .WithOne(pm => pm.Pokemon)
                .HasForeignKey(pm => pm.PokemonId);

            modelBuilder.Entity<Pokemon>()
                .HasMany(p => p.PokemonTipo)
                .WithOne(pt => pt.Pokemon)
                .HasForeignKey(pt => pt.PokemonId);

            modelBuilder.Entity<Pokemon>()
                .HasMany(p => p.Evoluciones)
                .WithOne(e => e.PokemonEvolucionado)
                .HasForeignKey(e => e.pokemon_evolucionado);

            modelBuilder.Entity<Pokemon>()
                .HasMany(p => p.EvolucionesOrigen)
                .WithOne(e => e.PokemonOrigen)
                .HasForeignKey(e => e.pokemon_origen);


            //tabla "estadisticas_base"
            modelBuilder.Entity<EstadisticasBase>()
                .HasKey(e => e.PokemonId);

            modelBuilder.Entity<EstadisticasBase>()
                .HasOne(e => e.Pokemon)
                .WithOne(p => p.EstadisticasBase)
                .HasForeignKey<EstadisticasBase>(e => e.PokemonId);


            //tabla "movimiento"
            modelBuilder.Entity<Movimiento>()
                .HasKey(m => m.id_movimiento);

            modelBuilder.Entity<Movimiento>()
                .HasMany(m => m.MovimientoEfectoSecundario)
                .WithOne(mes => mes.Movimiento)
                .HasForeignKey(mes => mes.id_movimiento);

            modelBuilder.Entity<Movimiento>()
                .HasMany(m => m.PokemonMovimientoForma)
                .WithOne(pm => pm.Movimiento)
                .HasForeignKey(pm => pm.id_movimiento);


            //tabla "efecto_secundario"
            modelBuilder.Entity<EfectoSecundario>()
                .HasKey(e => e.id_efecto_secundario);

            modelBuilder.Entity<EfectoSecundario>()
                .HasMany(es => es.MovimientoEfectoSecundario)
                .WithOne(mes => mes.EfectoSecundario)
                .HasForeignKey(mes => mes.id_efecto_secundario);


            //tabla "pokemon_tipo"
            modelBuilder.Entity<PokemonTipo>()
                .HasKey(pt => new { pt.PokemonId, pt.id_tipo });

            modelBuilder.Entity<PokemonTipo>()
                .HasOne(pt => pt.Pokemon)
                .WithMany(p => p.PokemonTipo)
                .HasForeignKey(pt => pt.PokemonId);

            modelBuilder.Entity<PokemonTipo>()
                .HasOne(pt => pt.Tipo)
                .WithMany(t => t.PokemonTipo)
                .HasForeignKey(pt => pt.id_tipo);


            //tabla "tipo"
            modelBuilder.Entity<Tipo>()
                .HasKey(t => t.id_tipo);

            modelBuilder.Entity<Tipo>()
                .HasOne(t => t.TipoAtaque)
                .WithMany(ta => ta.Tipo)
                .HasForeignKey(t => t.id_tipo_ataque);


            //tabla "tipo_ataque"
            modelBuilder.Entity<TipoAtaque>()
                .HasKey(ta => ta.id_tipo_ataque);


            //tabla "pokemon_movimiento_forma"
            modelBuilder.Entity<PokemonMovimientoForma>()
                .HasKey(pmf => new { pmf.PokemonId, pmf.id_movimiento, pmf.id_forma_aprendizaje });

            modelBuilder.Entity<PokemonMovimientoForma>()
                .HasOne(pmf => pmf.Pokemon)
                .WithMany(p => p.PokemonMovimientoForma)
                .HasForeignKey(pmf => pmf.PokemonId);

            modelBuilder.Entity<PokemonMovimientoForma>()
                .HasOne(pmf => pmf.Movimiento)
                .WithMany(m => m.PokemonMovimientoForma)
                .HasForeignKey(pmf => pmf.id_movimiento);

            modelBuilder.Entity<PokemonMovimientoForma>()
                .HasOne(pmf => pmf.FormaAprendizaje)
                .WithMany(fa => fa.PokemonMovimientoForma)
                .HasForeignKey(pmf => pmf.id_forma_aprendizaje);


            //tabla "evoluciona_de"
            modelBuilder.Entity<EvolucionaDe>()
                .HasKey(e => new { e.pokemon_evolucionado, e.pokemon_origen });

            modelBuilder.Entity<EvolucionaDe>()
                .HasOne(e => e.PokemonEvolucionado)
                .WithMany(p => p.Evoluciones)
                .HasForeignKey(e => e.pokemon_evolucionado);

            modelBuilder.Entity<EvolucionaDe>()
                .HasOne(e => e.PokemonOrigen)
                .WithMany(p => p.EvolucionesOrigen)
                .HasForeignKey(e => e.pokemon_origen);


            //tabla "MovimientoEfectoSecundario"
            modelBuilder.Entity<MovimientoEfectoSecundario>()
                .HasKey(mes => new { mes.id_movimiento, mes.id_efecto_secundario });


            //demás entidades
            modelBuilder.Entity<FormaEvolucion>()
                .HasKey(fe => fe.id_forma_evolucion);

            modelBuilder.Entity<FormaAprendizaje>()
                .HasKey(fa => fa.id_forma_aprendizaje);

            modelBuilder.Entity<TipoFormaAprendizaje>()
                .HasKey(tfa => tfa.id_tipo_aprendizaje);

            modelBuilder.Entity<Piedra>()
                .HasKey(p => p.id_forma_evolucion);

            modelBuilder.Entity<NivelEvolucion>()
                .HasKey(ne => ne.id_forma_evolucion);

            modelBuilder.Entity<NivelAprendizaje>()
                .HasKey(na => na.id_forma_aprendizaje);

            modelBuilder.Entity<MT>()
                .HasKey(mt => mt.id_forma_aprendizaje);

            modelBuilder.Entity<MO>()
                .HasKey(mo => mo.id_forma_aprendizaje);
        }
    }
}