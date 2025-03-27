using LoveiraNoresLaraTarea4.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace LoveiraNoresLaraTarea4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Configurar el contexto de la base de datos
            builder.Services.AddDbContext<PokemonContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionMontecastelo")));


            //Configurar servicios para la sesión
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true;
            });



            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //use session hay que añadirlo tambien para que la variable de sesion funcione.
            app.UseSession();
            app.UseAuthorization();

            //Ruta por defecto
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}