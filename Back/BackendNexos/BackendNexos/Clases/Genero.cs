using BackendNexos.Modelos;
using BackendNexos.Clases;
namespace BackendNexos.Clases.Genero
{
    //Espacio dedicado para estructuracion y creacion de metodos
    public class Genero
    {
        //Conexion con NexozContext para solicitudes a la bd
        private readonly NexozContext _context;
        public Genero()
        {
            _context = new NexozContext();
        }

        //Metodo para listar los distintos generos de libros
        public List<Generos.Dtos.getData> GetAllGeneros()
        {
            //Se genera objeto para brindar una respuesta
            List<Generos.Dtos.getData> ObjResponse = new List<Generos.Dtos.getData>();
            try
            {
                //Se genera objeto para obtener datos de la tabla consultado a traves del context
                var ResponseTabla = _context.Generos.ToList();
                //Se ingresa a foreach para recorrer los datos obtenidos
                foreach (var dato in ResponseTabla)
                {
                    //Se genera objeto para soportar los datos recibidos
                    Generos.Dtos.getData ObjItem = new Generos.Dtos.getData();
                    ObjItem.IdGenero = dato.IdGenero;
                    ObjItem.Descipcion = dato.Descipcion;
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

        //Metodo para insertar Generos de libros
        public ResponseParameters Insert(Generos.Dtos.insert ObjInsert)
        {
            //Se genera objeto para brindar una respuesta controlada
            ResponseParameters ObjResponse = new ResponseParameters();
            try
            {
                //Se crea variable para anidarle la informacion a insertar
                var insert = new Modelos.Genero()
                {
                    Descipcion = ObjInsert.Descipcion,
                };
                //Se procede a insertar informacion a la tabla
                _context.Generos.Add(insert);
                _context.SaveChanges();

                //Se valida que insertara registro
                if (insert.IdGenero > 0)
                {
                    //El registro fue generado con exito
                    //Se devolver codigo exitoso 1 junto al id del registro
                    return ObjResponse.SelectDescription(1, Convert.ToInt32(insert.IdGenero));
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
namespace BackendNexos.Clases.Generos.Dtos
{
    //Espacio dedicado para la parametrizacion de la tabla Genero
    public class getData
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
    public class insert
    {
        //Sirve para insertar registros en la tabla
        /// <summary>
        /// <para>IdGenero: Definicion del genero, ejemplo: Accion, Aventuras, terror</para>
        /// </summary>
        public string? Descipcion { get; set; }
    }
}
