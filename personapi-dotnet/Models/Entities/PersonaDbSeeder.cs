using System;
using System.Linq;

namespace personapi_dotnet.Models.Entities;

public static class PersonaDbSeeder
{
    public static void Seed(PersonaDbContext context)
    {
        if (context.Profesiones.Any() || context.Personas.Any())
        {
            return;
        }

        var profesiones = new[]
        {
            new Profesion { Nom = "Ingeniero de Sistemas", Des = "Diseño y desarrollo de software" },
            new Profesion { Nom = "Medico General", Des = "Atencion primaria en salud" },
            new Profesion { Nom = "Abogado", Des = "Asesoria juridica y litigios" },
            new Profesion { Nom = "Arquitecto", Des = "Diseno de espacios y construcciones" },
            new Profesion { Nom = "Contador Publico", Des = "Gestion contable y tributaria" }
        };

        var profesionIdMap = new[] { 1, 2, 3, 4, 5 };

        var personas = new[]
        {
            new Persona { Cc = 1001, Nombre = "Ana", Apellido = "Lopez", Genero = "F", Edad = 28 },
            new Persona { Cc = 1002, Nombre = "Luis", Apellido = "Gomez", Genero = "M", Edad = 35 },
            new Persona { Cc = 1003, Nombre = "Maria", Apellido = "Perez", Genero = "F", Edad = 24 },
            new Persona { Cc = 1004, Nombre = "Carlos", Apellido = "Rodriguez", Genero = "M", Edad = 42 },
            new Persona { Cc = 1005, Nombre = "Sofia", Apellido = "Martinez", Genero = "F", Edad = 31 }
        };

        var estudios = new[]
        {
            new Estudios { IdProf = 1, CcPer = 1001, Fecha = new DateTime(2018, 6, 15), Univer = "Universidad Nacional" },
            new Estudios { IdProf = 2, CcPer = 1002, Fecha = new DateTime(2015, 12, 1), Univer = "Universidad de los Andes" },
            new Estudios { IdProf = 3, CcPer = 1003, Fecha = new DateTime(2020, 7, 20), Univer = "Universidad Externado" },
            new Estudios { IdProf = 4, CcPer = 1004, Fecha = new DateTime(2010, 5, 10), Univer = "Universidad Javeriana" },
            new Estudios { IdProf = 5, CcPer = 1005, Fecha = new DateTime(2017, 11, 25), Univer = "Universidad del Rosario" }
        };

        var telefonos = new[]
        {
            new Telefono { Num = "3001112233", Oper = "Claro", Duenio = 1001 },
            new Telefono { Num = "3014445566", Oper = "Movistar", Duenio = 1002 },
            new Telefono { Num = "3027778899", Oper = "Tigo", Duenio = 1003 },
            new Telefono { Num = "3030001122", Oper = "WOM", Duenio = 1004 },
            new Telefono { Num = "3043334455", Oper = "ETB", Duenio = 1005 }
        };

        context.Profesiones.AddRange(profesiones);
        context.Personas.AddRange(personas);
        context.SaveChanges();

        for (int i = 0; i < estudios.Length; i++)
        {
            estudios[i].IdProf = profesiones[i].Id;
        }

        context.Estudios.AddRange(estudios);
        context.Telefonos.AddRange(telefonos);
        context.SaveChanges();
    }
}
