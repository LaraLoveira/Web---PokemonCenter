using LoveiraNoresLaraTarea4.Models;
using LoveiraNoresLaraTarea4.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveiraNoresLaraTarea4.Extensions;

namespace LoveiraNoresLaraTarea4.Controllers
{
    public class PokemonController : Controller
    {
        private readonly PokemonContext _context;

        public PokemonController(PokemonContext context)
        {
            _context = context;
        }

        //ListaPokemon
        public IActionResult ListaPokemon(string tipo, float? peso, float? altura)
        {

            //Filtramos la lista según los parámetros
            var pokemonQuery = _context.Pokemon.AsQueryable();

            if (!string.IsNullOrEmpty(tipo))
            {
                pokemonQuery = pokemonQuery.Where(p => p.PokemonTipo.Any(pt => pt.Tipo.nombre == tipo));
            }

            if (peso.HasValue)
            {
                pokemonQuery = pokemonQuery.Where(p => p.peso == peso.Value);
            }

            if (altura.HasValue)
            {
                pokemonQuery = pokemonQuery.Where(p => p.altura == altura.Value);
            }

            //Obtener la lista final
            var pokemon = pokemonQuery.Include(p => p.PokemonTipo)
                                      .ThenInclude(pt => pt.Tipo)
                                      .ToList();

            return View(pokemon);
        }



        //Detalles ListaPokemon
        public IActionResult Detalles(int id)
        {
            //Recuperamos el Pokemon con sus evoluciones e involuciones, y movimientos
            var pokemon = _context.Pokemon
                .Include(p => p.EstadisticasBase)
                .Include(p => p.PokemonTipo!)
                    .ThenInclude(pt => pt.Tipo!)
                .Include(p => p.PokemonMovimientoForma!)
                    .ThenInclude(pm => pm.Movimiento)
                .Where(p => p.PokemonId == id)
                .FirstOrDefault();

            if (pokemon == null)
            {
                return NotFound();
            }

            //evoluciones Pokemon
            var evoluciones = _context.EvolucionaDe
                .Include(e => e.PokemonEvolucionado)
                .Where(e => e.pokemon_origen == id)
                .Select(e => e.PokemonEvolucionado)
                .ToList();

            //involuciones Pokemon
            var involuciones = _context.EvolucionaDe
                .Include(e => e.PokemonOrigen)
                .Where(e => e.pokemon_evolucionado == id)
                .Select(i => i.PokemonOrigen)
                .ToList();

            //ViewModel para la vista de detalles
            var viewModel = new DetallePokemonViewModel
            {
                Pokemon = pokemon,
                Evoluciones = evoluciones,
                Involuciones = involuciones,
                Movimientos = pokemon.PokemonMovimientoForma
                                   ?.Select(pm => pm.Movimiento)
                                   .ToList() ?? new List<Movimiento>(),
                Tipo = pokemon.PokemonTipo
                            ?.Select(pt => pt.Tipo)
                            .ToList() ?? new List<Tipo>()
            };

            return View(viewModel);
        }



        //Añadir un Pokemon al equipo
        public IActionResult AñadirAEquipo(int id)
        {
            var miEquipo = HttpContext.Session.GetObject<List<Pokemon>>("MiEquipo") ?? new List<Pokemon>();

            if (miEquipo.Count >= 6)
            {
                TempData["Error"] = "¡No puedes añadir más de 6 Pokemon al equipo!";
                return RedirectToAction("DetallePokemon");
            }

            var pokemon = _context.Pokemon
                .Include(p => p.PokemonTipo)
                    .ThenInclude(pt => pt.Tipo)
                .FirstOrDefault(p => p.PokemonId == id);

            if (pokemon != null)
            {
                miEquipo.Add(pokemon);
                HttpContext.Session.SetObject("MiEquipo", miEquipo);
                TempData["Success"] = $"{pokemon.nombre} ha sido añadido al equipo.";
            }
            else
            {
                TempData["Error"] = "No se pudo añadir el Pokemon al equipo :(";
            }

            return RedirectToAction("DetallePokemon");
        }


        //Mostrar el equipo Pokemon
        public IActionResult DetallePokemon()
        {
            var miEquipo = HttpContext.Session.GetObject<List<Pokemon>>("MiEquipo") ?? new List<Pokemon>();

            if (!miEquipo.Any())
            {
                TempData["Mensaje"] = "No hay ningún Pokemon en el equipo aún.";
            }

            var equipo = miEquipo.Select(p => _context.Pokemon
                .Include(p => p.PokemonTipo)
                    .ThenInclude(pt => pt.Tipo)
                .Include(p => p.PokemonMovimientoForma)
                    .ThenInclude(pm => pm.Movimiento)
                .Include(p => p.EstadisticasBase)
                .FirstOrDefault(pokemon => pokemon.PokemonId == p.PokemonId))
                .Where(p => p != null)
                .ToList();

            return View(equipo);
        }



        //Generar un equipo aleatorio
        [HttpGet]
        [Route("Pokemon/RandomTeam")]
        public IActionResult RandomTeam()
        {
            //Obtener 6 Pokémon aleatorios
            var equipoAleatorio = _context.Pokemon
                .Include(p => p.PokemonTipo)
                    .ThenInclude(pt => pt.Tipo)
                .OrderBy(p => Guid.NewGuid())
                .Take(6)
                .ToList();

            //Resumen del equipo
            var cantidad = equipoAleatorio.Count;
            var tipoPredominante = equipoAleatorio
                .SelectMany(p => p.PokemonTipo)
                .GroupBy(pt => pt.Tipo.nombre)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "N/A";
            var pesoMedio = equipoAleatorio.Average(p => p.peso);
            var alturaMedia = equipoAleatorio.Average(p => p.altura);

            //Crear el ViewModel
            var viewModel = new EquipoAleatorioViewModel
            {
                Equipo = equipoAleatorio,
                Cantidad = cantidad,
                TipoPredominante = tipoPredominante,
                PesoMedio = pesoMedio,
                AlturaMedia = alturaMedia
            };

            return View(viewModel);
        }
    }
}