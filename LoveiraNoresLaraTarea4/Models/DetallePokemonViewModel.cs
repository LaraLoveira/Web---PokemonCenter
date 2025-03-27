namespace LoveiraNoresLaraTarea4.Models
{
    public class DetallePokemonViewModel
    {
        public required Pokemon Pokemon { get; set; }
        public required List<Pokemon> Evoluciones { get; set; }
        public List<Pokemon>? Involuciones { get; set; }
        public required List<Movimiento> Movimientos { get; set; }
        public required List<Tipo> Tipo { get; set; }
    }
}