using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndNexoz.Pages.Autor
{
    public class IndexModel : PageModel
    {
        public List<getData> ObjAutor = new List<getData>();
        public void OnGet()
        {
            //Se realiza conexion con apis creadas en proyecto de back (BackendNexoz)
            //Se genera objeto para soportar respuesta de apis de tabla autor
            List<BackendNexos.Clases.Autores.Dtos.getData> ObjGetData = new List<BackendNexos.Clases.Autores.Dtos.getData>();
            //Se crea objeto para conectar con los controladores de autores
            BackendNexos.Controladores.AutorsControllers ObjMethod = new BackendNexos.Controladores.AutorsControllers();
            //Se envia solicitud para traer todos los autores
            ObjGetData = ObjMethod.ListaAutores();
            //Se valida que venga informacion
            if(ObjGetData.Count > 0)
            {
                //Trae datos para mostrar en tabla generada
                foreach(var data in ObjGetData)
                {
                    //Se recorren dats llenando objeto ObjAutor para mostrar en el html 
                    getData get = new getData();
                    get.IdAutor = data.IdAutor;
                    get.Nombre = data.Nombre;
                    get.FechaNacimiento = data.FechaNacimiento.ToString();
                    get.CiudadProcedencia = data.CiudadProcedencia;
                    get.Correo = data.Correo;
                    ObjAutor.Add(get);
                }
            }
            else
            {
                //no trae datos
            }
        }
    }
    public class getData
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
        public string? FechaNacimiento { get; set; }
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

