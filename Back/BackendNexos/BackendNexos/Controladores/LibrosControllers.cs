using Microsoft.AspNetCore.Mvc;
namespace BackendNexos.Controladores
{
    //Se configura ruta de api
    //Se indica que es un controlador
    [Route("Api/Models/[controller]")]
    [ApiController]
    public class LibrosControllers : ControllerBase
    {
        //Se realiza conexion con la clase correspondiente
        private BackendNexos.Clases.libro.Libro _ObjMetodo;
        public LibrosControllers()
        {
            _ObjMetodo = new BackendNexos.Clases.libro.Libro();
        }

        //Controlador para llamar a la clase GetAllLibros
        [HttpGet("GetAll")]
        public List<Clases.Libros.Dtos.getData> ListaLibros()
        {
            //Se crea objeto para dar respuesta
            List<Clases.Libros.Dtos.getData> objresponse = new List<Clases.Libros.Dtos.getData>();
            try
            {
                //Se envia peticion a la clase GetAllLibros
                objresponse = _ObjMetodo.GetAllLibros();
                return objresponse;
            }
            catch (Exception ex)
            {
                //Hubo un error en la clase, se devolveran los datos nulos
                return null;

            }
        }

        //Controlador para llamar a la clase GetDetailLibros
        [HttpGet("GetDetail/Tipo/detalle")]
        public List<Clases.Libros.Dtos.getData> ListaLibroDetalle(int Tipo, string detalle)
        {
            //Se crea objeto para dar respuesta
            List<Clases.Libros.Dtos.getData> objresponse = new List<Clases.Libros.Dtos.getData>();
            try
            {
                //Se envia peticion a la clase GetDetailLibros
                objresponse = _ObjMetodo.GetDetailLibros(Tipo,detalle);
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
        public Clases.ResponseParameters Insert(Clases.Libros.Dtos.insert objInsert)
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
                //No se logro insertar el genero del libro
                //Se devolvera codigo de error 90 de response parameters (Ha ocurrido algo inesperado, intenta mas tarde)
                return ObjResponse.SelectDescription(90);

            }
        }
    }
}
