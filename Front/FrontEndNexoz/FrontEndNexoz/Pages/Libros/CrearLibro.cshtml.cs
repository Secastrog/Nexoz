using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEndNexoz.Pages.Libros
{
    public class CrearLibroModel : PageModel
    {
        public List<ListGenero> Objgenero = new List<ListGenero>();
        public List<ListAutores> ObjAutor = new List<ListAutores>();

        public getDataLibros Objlibros= new getDataLibros();
        public String errorMessage = "";
        public String succesMessage = "";


        public void OnGet()
        {
            //se generan objetos de parametros y metodos para poder traer los generos de lo libros de la tabla genero
            List<BackendNexos.Clases.Generos.Dtos.getData> ObjListaGeneros = new List<BackendNexos.Clases.Generos.Dtos.getData>();
            BackendNexos.Controladores.GenerosControllers Objmethod = new BackendNexos.Controladores.GenerosControllers();

            //Se envia la peticion para traer los generos de libro
            ObjListaGeneros = Objmethod.ListaGeneros();

            //Se valida que lleguen datos
            if(ObjListaGeneros.Count > 0)
            {
                foreach(var dato in ObjListaGeneros)
                {
                    ListGenero get = new ListGenero();
                    get.IdGenero = dato.IdGenero;
                    get.Descipcion = dato.Descipcion;
                    Objgenero.Add(get);
                }

            }

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
        }

        public void OnPost()
        {
            //Se realiza conexion con parametros de dll de proyecto backend para insertar libros 
            BackendNexos.Clases.Libros.Dtos.insert ObjInsert = new BackendNexos.Clases.Libros.Dtos.insert();
            //Se conecta con Controladores del dll
            BackendNexos.Controladores.LibrosControllers ObjMethod = new BackendNexos.Controladores.LibrosControllers();
            //Se genera objeto para controlar respuesta
            BackendNexos.Clases.ResponseParameters ObjResponse = new BackendNexos.Clases.ResponseParameters();

            //Se traen datos del formulario
            Objlibros.Titulo = Request.Form["titulo"];
            Objlibros.Año = Request.Form["año"];
            Objlibros.IdGenero = Convert.ToInt32(Request.Form["gen"]);
            Objlibros.NumPaginas = Request.Form["numpaginas"];
            Objlibros.IdAutor = Convert.ToInt32(Request.Form["aut"]);
            if (Objlibros.Titulo.Length == 0 || Objlibros.Año.Length == 0 || Objlibros.IdGenero == null || Objlibros.NumPaginas.Length == 0 || Objlibros.IdAutor == null)
            {
                //Se devuelve mensaje por error
                errorMessage = "Ingresa todos los datos solicitados";
                return;
            }
            //Se llena objeto insertar
            ObjInsert.Titulo = Objlibros.Titulo;
            ObjInsert.Año = Objlibros.Año;
            ObjInsert.IdGenero = Objlibros.IdGenero;
            ObjInsert.NumPaginas = Objlibros.NumPaginas;
            ObjInsert.IdAutor = Objlibros.IdAutor;

            //Se va a guardar nuevo libro
            ObjResponse = ObjMethod.Insert(ObjInsert);
            if (ObjResponse.Code == 1)
            {
                succesMessage = "Libro ingresado con exito";
            }
            else
            {
                errorMessage = ObjResponse.Description;
            }

        }
    }
  
}
