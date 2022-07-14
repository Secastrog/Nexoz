namespace BackendNexos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
                //Una vez se inicie el host se debe agregar /swagger para que inicie una libreria de swagger la cual permitira tener un control grafico de las apis rest generadas 
            }
            catch (Exception ex)
            {
                //error
                throw;
            }
            finally
            {
                //pausa
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            }

        }
        //creacion de host
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            });
    }

}