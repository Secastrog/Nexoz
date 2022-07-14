using BackendNexos.Modelos;
using BackendNexos.Clases;
namespace BackendNexos.Clases.Autor
{
    //Espacio dedicado para estructuracion y creacion de metodos
    public class Autor
    {
        //Conexion con NexozContext para solicitudes a la bd
        private readonly NexozContext _context;
        public Autor()
        {
            _context = new NexozContext();
        }
        //Metodo para listar los autores registrados
        public List<Autores.Dtos.getData> GetAllAutores()
        {
            //Se genera objeto para brindar una respuesta
            List<Autores.Dtos.getData> ObjResponse = new List<Autores.Dtos.getData>();
            try
            {
                //Se genera objeto para obtener datos de la tabla consultado a traves del context
                var ResponseTabla = _context.Autors.ToList();
                //Se ingresa a foreach para recorrer los datos obtenidos
                foreach (var dato in ResponseTabla)
                {
                    //Se genera objeto para soportar los datos recibidos
                    Autores.Dtos.getData ObjItem = new Autores.Dtos.getData();
                    ObjItem.IdAutor = dato.IdAutor;
                    ObjItem.Nombre = dato.Nombre;
                    ObjItem.FechaNacimiento = dato.FechaNacimiento;
                    ObjItem.CiudadProcedencia = dato.CiudadProcedencia;
                    ObjItem.Correo = dato.Correo;
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
        //Metodo para insertar autores
        public ResponseParameters Insert(Autores.Dtos.insert ObjInsert)
        {
            //Se genera objeto para brindar una respuesta controlada
            ResponseParameters ObjResponse = new ResponseParameters();
            try
            {
                //Se crea variable para anidarle la informacion a insertar
                var insert = new Modelos.Autor()
                {
                    Nombre = ObjInsert.Nombre,
                    FechaNacimiento = ObjInsert.FechaNacimiento,
                    CiudadProcedencia = ObjInsert.CiudadProcedencia,
                    Correo = ObjInsert.Correo,
                };
                //Se procede a insertar informacion a la tabla
                _context.Autors.Add(insert);
                _context.SaveChanges();

                //Se valida que insertara registro
                if (insert.IdAutor > 0)
                {
                    //El registro fue generado con exito
                    //Se devolver codigo exitoso 1 junto al id del registro
                    return ObjResponse.SelectDescription(1, Convert.ToInt32(insert.IdAutor));
                }
                else
                {
                    //No se inserto el registro
                    //Se devolvera codigo de error 90 de response parameters (Ha ocurrido algo inesperado, intenta mas tarde)
                    return ObjResponse.SelectDescription(90);
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
namespace BackendNexos.Clases.Autores.Dtos
{
     
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
    public class insert
    {
        //Parametros para insertar
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
