using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndNexoz.Pages.Libros
{
    public class IndexModel : PageModel
    {
        public List<getDataLibros> ObjLibro = new List<getDataLibros>();
        public List<ListGenero> Objgenero = new List<ListGenero>();
        public List<ListAutores> ObjAutor = new List<ListAutores>();
        
        public bool filterAutor = false;
        public bool FilterTitulo = false;
        public bool FilterAño = false;
        public string TipoFiltro { get; set; }

        public void OnGet()
        {
            
            //Se realiza conexion con apis creadas en proyecto de back (BackendNexoz)
            //Se genera objeto para soportar respuesta de apis de tabla Libro
            List<BackendNexos.Clases.Libros.Dtos.getData> ObjGetData = new List<BackendNexos.Clases.Libros.Dtos.getData>();
            //Se crea objeto para conectar con los controladores de libro
            BackendNexos.Controladores.LibrosControllers ObjMethod = new BackendNexos.Controladores.LibrosControllers();
            //Se envia solicitud para traer todos los libros registrados
            ObjGetData = ObjMethod.ListaLibros();
            //Se valida que venga informacion
            if (ObjGetData.Count > 0)
            {
                //Trae datos para mostrar en tabla generada
                foreach (var data in ObjGetData)
                {
                    //Se recorren dats llenando objeto ObjLibro para mostrar en el html 
                    getDataLibros get = new getDataLibros();
                    get.IdLibros = data.IdLibros;
                    get.Titulo = data.Titulo;
                    get.Año = data.Año;
                    get.IdGenero = data.IdGenero;
                    get.NumPaginas = data.NumPaginas;
                    get.IdAutor = data.IdAutor;
                    ObjLibro.Add(get);
                }
            }
            else
            {
                //no trae datos
            }  
        }
        public void OnPost()
        {
            //Se realiza conexion con apis creadas en proyecto de back (BackendNexoz)
            //Se genera objeto para soportar respuesta de apis de tabla Libro
            List<BackendNexos.Clases.Libros.Dtos.getData> ObjGetData = new List<BackendNexos.Clases.Libros.Dtos.getData>();
            //Se crea objeto para conectar con los controladores de libro
            BackendNexos.Controladores.LibrosControllers ObjMethod = new BackendNexos.Controladores.LibrosControllers();
            //Se envia solicitud para traer todos los libros registrados
            ObjGetData = ObjMethod.ListaLibros();
            //Se valida que venga informacion
            if (ObjGetData.Count > 0)
            {
                //Trae datos para mostrar en tabla generada
                foreach (var data in ObjGetData)
                {
                    //Se recorren dats llenando objeto ObjLibro para mostrar en el html 
                    getDataLibros get = new getDataLibros();
                    get.IdLibros = data.IdLibros;
                    get.Titulo = data.Titulo;
                    get.Año = data.Año;
                    get.IdGenero = data.IdGenero;
                    get.NumPaginas = data.NumPaginas;
                    get.IdAutor = data.IdAutor;
                    ObjLibro.Add(get);
                }
            }
            else
            {
                //no trae datos
            }
            var tipo_filtro = Request.Form["gen"];
        
            switch (Convert.ToInt32(tipo_filtro))
            {
                case 1: //Filtro por Autor
                    //se generan objetos de parametros y metodos para poder traer los Autores de lo libros de la tabla autor
                    List<BackendNexos.Clases.Autores.Dtos.getData> ObjGetAutor = new List<BackendNexos.Clases.Autores.Dtos.getData>();
                    BackendNexos.Controladores.AutorsControllers ObjMethoDAut = new BackendNexos.Controladores.AutorsControllers();

                    //Se envia solicitud para traer los autores
                    ObjGetAutor = ObjMethoDAut.ListaAutores();
                    //Se valida que lleguen datos
                    if (ObjGetAutor.Count > 0)
                    {
                        foreach (var dato in ObjGetAutor)
                        {
                            ListAutores get = new ListAutores();
                            get.Nombre = dato.Nombre;
                            get.IdAutor = dato.IdAutor;
                            ObjAutor.Add(get);
                        }

                    }
                    filterAutor = true;
                    FilterTitulo = false;
                    FilterAño = false;
                    break;
                case 2: //Filtro por Titulo
                    filterAutor = false;
                    FilterTitulo = true;
                    FilterAño = false;
                    break;
                case 3: //Filtro por Año
                    filterAutor = false;
                    FilterTitulo = false;
                    FilterAño = true;
                    break;
                default:
                    filterAutor = false;
                    FilterTitulo = false;
                    FilterAño = false;
                    break;
            }
        }


        public async Task<IActionResult> OnPostJoinListAsync()
        {
            //Se conecta con api por detalle
            List<BackendNexos.Clases.Libros.Dtos.getData> ObjLibroDetalles = new List<BackendNexos.Clases.Libros.Dtos.getData>();
            BackendNexos.Controladores.LibrosControllers Objmethod = new BackendNexos.Controladores.LibrosControllers();
            //Se validara el tipo de filtro para enviar los datos pertinenetes al api
            string autor = Request.Form["aut"];
            string titulo = Request.Form["titulo"];
            string año = Request.Form["año"];
            if (autor == null)
            {
                if (titulo == null)
                {
                    if (año == null)
                    {
                        //Todos los campos llegaron nulos
                    }
                    else
                    {
                        if (año.Length > 0 || año != "") //Filtro por Titulo
                        {
                            //var año = Request.Form["año"];
                            //se envia solicitud a api
                            ObjLibroDetalles = Objmethod.ListaLibroDetalle(3, año);
                            if (ObjLibroDetalles.Count > 0)
                            {
                                foreach (var data in ObjLibroDetalles)
                                {
                                    getDataLibros get = new getDataLibros();
                                    get.IdLibros = data.IdLibros;
                                    get.Titulo = data.Titulo;
                                    get.Año = data.Año;
                                    get.IdGenero = data.IdGenero;
                                    get.NumPaginas = data.NumPaginas;
                                    get.IdAutor = data.IdAutor;
                                    ObjLibro.Add(get);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (titulo.Length > 0 || titulo != "")//Filtro por Titulo
                    {
                        //var titulo = Request.Form["titulo"];
                        //se envia solicitud a api
                        ObjLibroDetalles = Objmethod.ListaLibroDetalle(2, titulo);
                        if (ObjLibroDetalles.Count > 0)
                        {
                            foreach (var data in ObjLibroDetalles)
                            {
                                getDataLibros get = new getDataLibros();
                                get.IdLibros = data.IdLibros;
                                get.Titulo = data.Titulo;
                                get.Año = data.Año;
                                get.IdGenero = data.IdGenero;
                                get.NumPaginas = data.NumPaginas;
                                get.IdAutor = data.IdAutor;
                                ObjLibro.Add(get);
                            }
                        }
                    }
                }
            }

            else
            {
                if (autor.Length > 0 || autor != "") //Filtro por Titulo
                {
                    //var autor = Request.Form["aut"];
                    //se envia solicitud a api
                    ObjLibroDetalles = Objmethod.ListaLibroDetalle(1, autor);
                    if (ObjLibroDetalles.Count > 0)
                    {
                        foreach (var data in ObjLibroDetalles)
                        {
                            getDataLibros get = new getDataLibros();
                            get.IdLibros = data.IdLibros;
                            get.Titulo = data.Titulo;
                            get.Año = data.Año;
                            get.IdGenero = data.IdGenero;
                            get.NumPaginas = data.NumPaginas;
                            get.IdAutor = data.IdAutor;
                            ObjLibro.Add(get);
                        }
                    }
                }
            }
           
            return null;
        }


    }

    public class getDataLibros
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
        /// <para>DescripcionGenero: Se muestra descripcion del genero de libro</para>
        /// </summary>
        public int? DescripcionGenero { get; set; }
        /// <summary>
        /// <para>NumPaginas: Numero de paginas que contiene el libro</para>
        /// </summary>
        public string? NumPaginas { get; set; }
        /// <summary>
        /// <para>IdAutor: Indice primario de tabla Autor donde se identifica los datos del autor del libro</para>
        /// </summary>
        public int? IdAutor { get; set; }
    }
    public class ListGenero
    {
        //Sirve para traer datos de la tabla
        /// <summary>
        /// <para>IdGenero: Identificador auto incremental de los generos de libros</para>
        /// </summary> 
        public int IdGenero { get; set; }
        /// <summary>
        /// <para>IdGenero: Definicion del genero, ejemplo: Accion, Aventuras, terror</para>
        /// </summary>
        public string? Descipcion { get; set; }
    }
    public class ListAutores
    {
        //parametros para listar datos
        /// <summary>
        /// <para>IdAutor: Identificador auto incremental de autores</para>
        /// </summary>   
        public int IdAutor { get; set; }
        /// <summary>
        /// <para>Nombre: Nombre completo del autor</para>
        /// </summary> 
        public string? Nombre { get; set; }
        /// <summary>
        /// <para>FechaNacimiento: Fecha nacimiento del autor</para>
        /// </summary> 
        public DateTime? FechaNacimiento { get; set; }
        /// <summary>
        /// <para>CiudadProcedencia: Ciudad de nacimiento de autor</para>
        /// </summary> 
        public string? CiudadProcedencia { get; set; }
        /// <summary>
        /// <para>Correo: Correo de autor</para>
        /// </summary> 
        public string? Correo { get; set; }
    }


}
