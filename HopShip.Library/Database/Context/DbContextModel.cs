using HopShip.Library.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Library.Database.Context
{
    public class DbContextModel : DbContext
    {
        public DbContextModel(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ottieni tutti gli assembly caricati nell'AppDomain
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Cerca tutte le classi che ereditano da BaseEntity
            var entityTypes = assemblies
                .SelectMany(assembly => {
                    try
                    {
                        // GetTypes() può lanciare eccezioni se ci sono problemi di caricamento
                        return assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        // Recupera i tipi che sono stati caricati con successo
                        return ex.Types.Where(t => t != null);
                    }
                    catch (Exception)
                    {
                        // Gestisci altre eccezioni di caricamento
                        return Type.EmptyTypes;
                    }
                })
                .Where(type =>
                    type != null &&
                    type.IsClass &&
                    !type.IsAbstract &&
                    typeof(BaseEntity).IsAssignableFrom(type))
                .ToList(); // Materializza la lista per facilitare il debug

            // Registra ciascun tipo come entità nel modello
            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
