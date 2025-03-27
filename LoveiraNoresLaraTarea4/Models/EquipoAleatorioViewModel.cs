namespace LoveiraNoresLaraTarea4.Models
{
    internal class EquipoAleatorioViewModel
    {
        public required List<Pokemon> Equipo { get; set; }
        public int Cantidad { get; set; }
        public required string TipoPredominante { get; set; }
        public double PesoMedio { get; set; }
        public double AlturaMedia { get; set; }
    }
}