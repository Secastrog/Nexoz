using System;
using System.Collections.Generic;

namespace BackendNexos.Modelos
{
    //Modulo de parametros para tabla Libros
    public partial class Libro
    {
        public int IdLibros { get; set; }
        public string? Titulo { get; set; }
        public string? Año { get; set; }
        public int? IdGenero { get; set; }
        public string? NumPaginas { get; set; }
        public int? IdAutor { get; set; }

        public virtual Autor? IdAutorNavigation { get; set; }
        public virtual Genero? IdGeneroNavigation { get; set; }
    }
}
