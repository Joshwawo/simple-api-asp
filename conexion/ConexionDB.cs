namespace tienda.conexion
{
    public class ConexionDB
    {
        private string connectionString = string.Empty;
        public  ConexionDB()
        {
            var constructor = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            connectionString = constructor.GetSection("ConnectionStrings:conexionMaestra").Value;
        }
        public string cadenaSql()
        {
            return connectionString;
        }
    }
}
