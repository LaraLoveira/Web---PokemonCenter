using LoveiraNoresLaraTarea4.Models;
using LoveiraNoresLaraTarea4.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveiraNoresLaraTarea4.Extensions;

namespace LoveiraNoresLaraTarea4.Controllers
{
    public class CombateController : Controller
    {
        private readonly PokemonContext _context;

        public CombateController(PokemonContext context)
        {
            _context = context;
        }

        //Vista inicial
        public IActionResult Index()
        {
            return View();
        }

        //Generar un equipo aleatorio y luchar contra mi equipo
        public IActionResult CombateContraMiEquipo()
        {
            //Recuperar mi equipo
            var miEquipo = HttpContext.Session.GetObject<List<Pokemon>>("MiEquipo") ?? new List<Pokemon>();

            if (!miEquipo.Any())
            {
                TempData["Mensaje"] = "No hay Pokémon en tu equipo aún.";
                return RedirectToAction("DetallePokemon", "Pokemon");
            }

            //Generar equipo aleatorio
            var equipoAleatorio = _context.Pokemon
                .Include(p => p.PokemonTipo)
                    .ThenInclude(pt => pt.Tipo)
                .OrderBy(r => Guid.NewGuid())
                .Take(miEquipo.Count)
                .ToList();

            //Lista resultados
            var resultados = new List<dynamic>();

            //Combate Pokemon
            for (int i = 0; i < miEquipo.Count; i++)
            {
                var miPokemon = miEquipo[i];
                var rivalPokemon = equipoAleatorio.ElementAtOrDefault(i);

                if (rivalPokemon != null)
                {
                    var resultado = EvaluarCombate(miPokemon, rivalPokemon);
                    resultados.Add(new
                    {
                        MiPokemon = miPokemon,
                        Rival = rivalPokemon,
                        Resultado = resultado
                    });
                }
            }
            return View(resultados);
        }



        //Generar dos equipos aleatorios y simular el combate
        public IActionResult CombateEntreEquipos()
        {
            //Generar dos equipos aleatorios
            var equipo1 = _context.Pokemon
                .Include(p => p.PokemonTipo)
                    .ThenInclude(pt => pt.Tipo)
                .OrderBy(r => Guid.NewGuid())
                .Take(6)
                .ToList();

            var equipo2 = _context.Pokemon
                .Include(p => p.PokemonTipo)
                    .ThenInclude(pt => pt.Tipo)
                .OrderBy(r => Guid.NewGuid())
                .Take(6)
                .ToList();

            //Combate Pokemon
            var resultados = equipo1.Zip(equipo2, (poke1, poke2) => new
            {
                Pokemon1 = poke1,
                Pokemon2 = poke2,
                Resultado = EvaluarCombate(poke1, poke2)
            }).ToList();

            return View(resultados);
        }



        //Función para evaluar un combate entre dos Pokémon
        private string EvaluarCombate(Pokemon poke1, Pokemon poke2)
        {
            if (poke1 == null || poke2 == null) return "Empate por falta de datos";

            //Evaluar por tipos
            var tipoFuerte = DeterminarTipoFuerte(poke1.PokemonTipo.Select(pt => pt.Tipo).ToList(),
                                                  poke2.PokemonTipo.Select(pt => pt.Tipo).ToList());
            if (tipoFuerte != 0)
            {
                return tipoFuerte > 0 ? "El equipo izquierda gana" : "El equipo derecha gana";
            }

            //Empate de tipo: evaluar por peso
            if (poke1.peso != poke2.peso)
            {
                return poke1.peso > poke2.peso ? "El equipo izquierda gana" : "El equipo derecha gana";
            }

            //Empate de peso: evaluar por altura
            if (poke1.altura != poke2.altura)
            {
                return poke1.altura > poke2.altura ? "El equipo izquierda gana" : "El equipo derecha gana";
            }
            return "Empate";
        }



        //Función para determinar el tipo más fuerte
        private int DeterminarTipoFuerte(List<Tipo> tipos1, List<Tipo> tipos2)
        {
            var fortalezas = new Dictionary<string, List<string>>
    {
        { "Acero", new List<string> { "Lucha", "Fuego", "Tierra" } },
        { "Agua", new List<string> { "Planta", "Eléctrico" } },
        { "Bicho", new List<string> { "Volador", "Fuego", "Roca" } },
        { "Dragón", new List<string> { "Hada", "Hielo", "Dragón" } },
        { "Eléctrico", new List<string> { "Tierra" } },
        { "Fantasma", new List<string> { "Fantasma", "Siniestro" } },
        { "Fuego", new List<string> { "Tierra", "Agua", "Roca" } },
        { "Hada", new List<string> { "Acero", "Veneno" } },
        { "Hielo", new List<string> { "Lucha", "Acero", "Roca", "Fuego" } },
        { "Lucha", new List<string> { "Psíquico", "Volador", "Hada" } },
        { "Normal", new List<string> { "Lucha" } },
        { "Planta", new List<string> { "Volador", "Bicho", "Veneno", "Hielo", "Fuego" } },
        { "Psíquico", new List<string> { "Bicho", "Fantasma", "Siniestro" } },
        { "Roca", new List<string> { "Lucha", "Tierra", "Acero", "Agua", "Planta" } },
        { "Siniestro", new List<string> { "Lucha", "Hada", "Bicho" } },
        { "Tierra", new List<string> { "Agua", "Planta", "Hielo" } },
        { "Veneno", new List<string> { "Tierra", "Psíquico" } },
        { "Volador", new List<string> { "Roca", "Hielo", "Eléctrico" } }
    };

            //Contadores para determinar la ventaja
            int ventajaTipos1 = 0;
            int ventajaTipos2 = 0;

            //Comparar cada tipo de tipos1 con cada tipo de tipos2
            foreach (var tipo1 in tipos1)
            {
                foreach (var tipo2 in tipos2)
                {
                    if (fortalezas.ContainsKey(tipo2.nombre) && fortalezas[tipo2.nombre].Contains(tipo1.nombre))
                    {
                        ventajaTipos1++; //tipo1 tiene ventaja sobre tipo2
                    }
                    else if (fortalezas.ContainsKey(tipo1.nombre) && fortalezas[tipo1.nombre].Contains(tipo2.nombre))
                    {
                        ventajaTipos2++; //tipo2 tiene ventaja sobre tipo1
                    }
                }
            }

            //Determinar el resultado
            if (ventajaTipos1 > ventajaTipos2) return 1;
            if (ventajaTipos2 > ventajaTipos1) return -1;
            return 0; //Es un empate
        }
    }
}