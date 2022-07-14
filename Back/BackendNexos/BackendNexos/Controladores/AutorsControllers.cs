using Microsoft.AspNetCore.Mvc;
namespace BackendNexos.Controladores
{
    //Se configura ruta de api
    //Se indica que es un controlador
    [Route("Api/Models/[controller]")]
    [ApiController]
    public class AutorsControllers : ControllerBase
    {
        //Se realiza conexion con la clase correspondiente
        private BackendNexos.Clases.Autor.Autor _ObjMetodo;
        public AutorsControllers()
        {
            _ObjMetodo = new BackendNexos.Clases.Autor.Autor();
        }

        //Controlador para llamar a la clase GetAllAutores
        [HttpGet("GetAll")]
        public List<Clases.Autores.Dtos.getData> ListaAutores()
        {
            //Se crea objeto para dar respuesta
            List<Clases.Autores.Dtos.getData> objresponse = new List<Clases.Autores.Dtos.getData>();
            try
            {
                //Se envia peticion a la clase GetAllAutores
                objresponse = _ObjMetodo.GetAllAutores();
                return objresponse;
            }
            catch (Exception ex)
            {
                //Hubo un error en la clase, se devolveran los datos nulos
                return null;
            }
        }

        //Controlador para insertar los autores
        [HttpPost("Insert")]
        public Clases.ResponseParameters Insert(Clases.Autores.Dtos.insert objInsert)
        {
            //Se genera objeto para dar respuesta
            Clases.ResponseParameters ObjResponse = new Clases.ResponseParameters();
            try
            {
                //Se envia la peticion
                ObjResponse = _ObjMetodo.Insert(objInsert);
                //Se retorna respuesta obtenida de la clase Insert
                return ObjResponse;
            }
            catch (Exception ex)
            {
                //No se logro insertar el Autor
                //Se devolvera codigo de error 90 de response parameters (Ha ocurrido algo inesperado, intenta mas tarde)
                return ObjResponse.SelectDescription(90);

            }
        }
    }
}
