using BackendNexos.Modelos;
using BackendNexos.Clases;
namespace BackendNexos.Clases.libro
{
    //Espacio dedicado para estructuracion y creacion de metodos
    public class Libro
    {
        //Conexion con NexozContext para solicitudes a la bd
        private readonly NexozContext _context;
         
        public Libro()
        {
            _context = new NexozContext();
            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _Configuration = builder.Build();
        }

        //Conexion con el appsetthings
        private IConfiguration _Configuration { get; }
        

        //Metodo para listar los Libros registrados
        public List<Libros.Dtos.getData> GetAllLibros()
        {
            //Se genera objeto para brindar una respuesta
            List<Libros.Dtos.getData> ObjResponse = new List<Libros.Dtos.getData>();
            try
            {
                //Se genera objeto para obtener datos de la tabla consultado a traves del context
                var ResponseTabla = _context.Libros.ToList();
                //Se ingresa a foreach para recorrer los datos obtenidos
                foreach (var dato in ResponseTabla)
                {
                    //Se genera objeto para soportar los datos recibidos
                    Libros.Dtos.getData ObjItem = new Libros.Dtos.getData();
                    ObjItem.IdLibros = dato.IdLibros;
                    ObjItem.Titulo = dato.Titulo;
                    ObjItem.Año = dato.Año;
                    ObjItem.IdGenero = dato.IdGenero;
                    ObjItem.NumPaginas = dato.NumPaginas;
                    ObjItem.IdAutor = dato.IdAutor;
                    ObjResponse.Add(ObjItem);
                }
                //Se devuelve los datos obtenidos en forma de lista
                return ObjResponse;
            }
            catch (Exception ex)
            {
                //Algo fallo al consultar la base de datos, se devolveran los datos nulos
                return null;
            }
        }

        //Metodo para listar los libros por autor, titulo, año o listar todos los libros
        public List<Libros.Dtos.getData> GetDetailLibros(int Tipo, string detalle)
        {
            //----
            //Tipos de busqueda : int Tipo
            //1: Autor
            //2: Titulo
            //3: Año
            //4: Todos los libros
            //----
            //----
            //string Detalle: Hace referencia al valor buscado
            //----
            //Se genera objeto para brindar una respuesta
            List<Libros.Dtos.getData> ObjResponse = new List<Libros.Dtos.getData>();
            try
            {
                //Variable para controlar parametors desde el _context
                var ResponseTablaAux = _context.Libros.ToList();
                //Se genera objeto switch para ver el tipo de solciitud de libro
                switch (Tipo)
                {
                    case 1://Libros filtrados por autor
                        var ResponseTablaAutor = _context.Libros.Where(x => x.IdAutor == Convert.ToInt32(detalle)).ToList();
                        ResponseTablaAux = ResponseTablaAutor;
                        break;
                    case 2://Libros filtrados por titulo
                        var ResponseTablaTitulo = _context.Libros.Where(x => x.Titulo == detalle).ToList();
                        ResponseTablaAux = ResponseTablaTitulo;
                        break;
                    case 3://Libros filtrados por Año
                        var ResponseTablaAño = _context.Libros.Where(x => x.Año == detalle).ToList();
                        ResponseTablaAux = ResponseTablaAño;
                        break;
                    case 4://Todos los libros regitrados
                        var ResponseTablaTodo = _context.Libros.ToList();
                        ResponseTablaAux = ResponseTablaTodo;
                        break;
                }
                //Se ingresa a foreach para recorrer los datos obtenidos
                foreach (var dato in ResponseTablaAux)
                {
                    //Se genera objeto para soportar los datos recibidos
                    Libros.Dtos.getData ObjItem = new Libros.Dtos.getData();
                    ObjItem.IdLibros = dato.IdLibros;
                    ObjItem.Titulo = dato.Titulo;
                    ObjItem.Año = dato.Año;
                    ObjItem.IdGenero = dato.IdGenero;
                    ObjItem.NumPaginas = dato.NumPaginas;
                    ObjItem.IdAutor = dato.IdAutor;
                    ObjResponse.Add(ObjItem);
                }
                //Se devuelve los datos obtenidos en forma de lista
                return ObjResponse;
            }
            catch (Exception ex)
            {
                //Algo fallo al consultar la base de datos, se devolveran los datos nulos
                return null;
            }
        }

        //Metodo para insertar libros controlando la cantidad permitida por autores
        public ResponseParameters Insert(Libros.Dtos.insert ObjInsert)
        {
            //Se genera objeto para brindar una respuesta controlada
            ResponseParameters ObjResponse = new ResponseParameters();
            try
            {
                //Se busca cantidad de libros registrada por autor sin exceder el maximo permitido, este maximo permitido esta confugirado en appsetthings
                var ResponseTabla = _context.Libros.Where(x => x.IdAutor == ObjInsert.IdAutor).ToList();
                //Se traer la cantida maxima de libros a poder insertar
                var ObjAppSetthings = _Configuration.GetSection("MaximoLibros");
                var cantidad = ObjAppSetthings.Value;
                if (ResponseTabla.Count >= Convert.ToInt32(cantidad))
                {
                    //El autor ya cargo la cantidad maxima de libros permitidos
                    //Se devolvera codigo de error 3 de response parameters (No es posible registrar el libro, se alcanzo el maximo permitido)
                    return ObjResponse.SelectDescription(3);
                }
                else
                {
                    //Se crea variable para anidarle la informacion a insertar
                    var insert = new Modelos.Libro()
                    {
                        Titulo = ObjInsert.Titulo,
                        Año = ObjInsert.Año,
                        IdGenero = ObjInsert.IdGenero,
                        NumPaginas = ObjInsert.NumPaginas,
                        IdAutor = ObjInsert.IdAutor,
                    };
                    //Se procede a insertar informacion a la tabla
                    _context.Libros.Add(insert);
                    _context.SaveChanges();

                    //Se valida que insertara registro
                    if (insert.IdLibros > 0)
                    {
                        //El registro fue generado con exito
                        //Se devolver codigo exitoso 1 junto al id del registro
                        return ObjResponse.SelectDescription(1, Convert.ToInt32(insert.IdLibros));
                    }
                    else
                    {
                        //No se inserto el registro
                        //Se devolvera codigo de error 90 de response parameters (Ha ocurrido algo inesperado, intenta mas tarde)
                        return ObjResponse.SelectDescription(90);
                    }
                }
            }
            catch (Exception ex)
            {
                //No se logro insertar el genero del libro
                //Se devolvera codigo de error 90 de response parameters (Ha ocurrido algo inesperado, intenta mas tarde)
                return ObjResponse.SelectDescription(90);
            }
        }


    }
}
namespace BackendNexos.Clases.Libros.Dtos
{
    public class getData
    {
        //Parametros para lstar datos
        /// <summary>
        /// <para>IdLibros: Identificador auto incremental de los libros registrados</para>
        /// </summary>
        public int IdLibros { get; set; }
        /// <summary>
        /// <para>Titulo: Titulo del libro</para>
        /// </summary>
        public string? Titulo { get; set; }
        /// <summary>
        /// <para>Año: Año de publicacion del libro</para>
        /// </summary>
        public string? Año { get; set; }
        /// <summary>
        /// <para>IdGenero: Indice primario de tabla genero donde se identificara el genero del libro</para>
        /// </summary>
        public int? IdGenero { get; set; }
        /// <summary>
        /// <para>NumPaginas: Numero de paginas que contiene el libro</para>
        /// </summary>
        public string? NumPaginas { get; set; }
        /// <summary>
        /// <para>IdAutor: Indice primario de tabla Autor donde se identifica los datos del autor del libro</para>
        /// </summary>
        public int? IdAutor { get; set; }
    }
    public class insert
    {
        //Parametros para insertar registros
        /// <summary>
        /// <para>Titulo: Titulo del libro</para>
        /// </summary>
        public string? Titulo { get; set; }
        /// <summary>
        /// <para>Año: Año de publicacion del libro</para>
        /// </summary>
        public string? Año { get; set; }
        /// <summary>
        /// <para>IdGenero: Indice primario de tabla genero donde se identificara el genero del libro</para>
        /// </summary>
        public int? IdGenero { get; set; }
        /// <summary>
        /// <para>NumPaginas: Numero de pagonas que contiene el libro</para>
        /// </summary>
        public string? NumPaginas { get; set; }
        /// <summary>
        /// <para>IdAutor: Indice primario de tabla Autor donde se identifica los datos del autor del libro</para>
        /// </summary>
        public int? IdAutor { get; set; }
    }
}

