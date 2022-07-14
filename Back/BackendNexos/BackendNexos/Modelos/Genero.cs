using System;
using System.Collections.Generic;

namespace BackendNexos.Modelos
{
    //Modulo de parametros para tabla GEnero
    public partial class Genero
    {
        public Genero()
        {
            Libros = new HashSet<Libro>();
        }

        public int IdGenero { get; set; }
        public string? Descipcion { get; set; }

        public virtual ICollection<Libro> Libros { get; set; }
    }
}
