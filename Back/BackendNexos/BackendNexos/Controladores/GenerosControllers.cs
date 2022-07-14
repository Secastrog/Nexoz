using Microsoft.AspNetCore.Mvc;
namespace BackendNexos.Controladores
{
    //Se configura ruta de api
    //Se indica que es un controlador
    [Route("Api/Models/[controller]")]
    [ApiController]
    public class GenerosControllers : ControllerBase
    {
        //Se realiza conexion con la clase correspondiente
        private BackendNexos.Clases.Genero.Genero _ObjMetodo;
        public GenerosControllers()
        {
            _ObjMetodo = new BackendNexos.Clases.Genero.Genero();
        }

        //Controlador para llamar a la clase GetAllGeneros
        [HttpGet("GetAll")]
        public List<Clases.Generos.Dtos.getData> ListaGeneros()
        {
            //Se crea objeto para dar respuesta
            List<Clases.Generos.Dtos.getData> objresponse = new List<Clases.Generos.Dtos.getData>();
            try
            {
                //Se envia peticion a la clase GetAllGeneros
                objresponse = _ObjMetodo.GetAllGeneros();
                return objresponse;
            }
            catch (Exception ex)
            {
                //Hubo un error en la clase, se devolveran los datos nulos
                return null;

            }
        }

        //Controlador para insertar los generos de libros
        [HttpPost("Insert")]
        public Clases.ResponseParameters Insert(Clases.Generos.Dtos.insert objInsert)
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
                //No se logro insertar el genero del genero
                //Se devolvera codigo de error 90 de response parameters (Ha ocurrido algo inesperado, intenta mas tarde)
                return ObjResponse.SelectDescription(90);

            }
        }
    }
}
