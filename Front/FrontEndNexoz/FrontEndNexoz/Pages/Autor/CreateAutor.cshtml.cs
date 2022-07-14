using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndNexoz.Pages.Autor
{
    public class CreateAutorModel : PageModel
    {
        public getData ObjAutor = new getData();
        public String errorMessage = "";
        public String succesMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            //Se realiza conexion con parametros de dll de proyecto backend para insertar autor
            BackendNexos.Clases.Autores.Dtos.insert ObjInsert = new BackendNexos.Clases.Autores.Dtos.insert();
            //Se conecta con Controladores del dll
            BackendNexos.Controladores.AutorsControllers ObjMethod = new BackendNexos.Controladores.AutorsControllers();
            //Se genera objeto para controlar respuesta
            BackendNexos.Clases.ResponseParameters ObjResponse = new BackendNexos.Clases.ResponseParameters();

            //Se traen datos del formulario
            ObjAutor.Nombre = Request.Form["name"];
            ObjAutor.FechaNacimiento = Request.Form["nacimiento"];
            ObjAutor.CiudadProcedencia = Request.Form["ciudad"];
            ObjAutor.Correo = Request.Form["correo"];
            if (ObjAutor.Nombre.Length == 0 || ObjAutor.FechaNacimiento.Length == 0 || ObjAutor.CiudadProcedencia.Length == 0 || ObjAutor.Correo.Length == 0)
            {
                //Se devuelve mensaje por error
                errorMessage = "Ingresa todos los datos solicitados";
                return; 
            }
            //Se llena objeto insertar
            ObjInsert.Nombre = ObjAutor.Nombre;
            ObjInsert.FechaNacimiento = Convert.ToDateTime(ObjAutor.FechaNacimiento);
            ObjInsert.CiudadProcedencia = ObjAutor.CiudadProcedencia;
            ObjInsert.Correo = ObjAutor.Correo;

            //Se va a guardar nuevo Autor
            ObjResponse = ObjMethod.Insert(ObjInsert);
            if(ObjResponse.Code == 1)
            {
                succesMessage = "Autor ingresado con exito";
            }
            else
            {
                errorMessage = ObjResponse.Description;
            }

        }
    }
}
