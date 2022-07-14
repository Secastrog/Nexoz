using System;
using System.Collections.Generic;

namespace BackendNexos.Modelos
{
    //Modulo de parametros para tabla Autor
    public partial class Autor
    {
        public Autor()
        {
            Libros = new HashSet<Libro>();
        }

        public int IdAutor { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? CiudadProcedencia { get; set; }
        public string? Correo { get; set; }

        public virtual ICollection<Libro> Libros { get; set; }
    }
}
