using System.Data.SqlClient;
using tienda.conexion;
using tienda.Models;
using System.Data;

namespace tienda.Data
{
    public class Dproductos {   
        readonly ConexionDB cadenaConexion =  new ConexionDB();

        public async Task <List<Mproducto>> MostrarProductos(){
            var lista = new List<Mproducto>();

            using (var sql = new SqlConnection(cadenaConexion.cadenaSql())){
                //Esto esta hecho con procedimientos almacenados
                using (var cmd = new SqlCommand("mostrarProductos", sql)){
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync()){
                        while (await item.ReadAsync()){
                            var modeloProductos = new Mproducto();
                            modeloProductos.id = (int)item["id"];
                            modeloProductos.descripcion = (string)item["descripcion"];
                            modeloProductos.precio = (decimal)item["precio"];
                            lista.Add(modeloProductos);

                        }
                    }
                }
            }
            return lista;
        }

        public async Task<Mproducto?> MostarProductoxId(Mproducto parametros)
        {
            using (var sql = new SqlConnection(cadenaConexion.cadenaSql()))
            {
                using (var cmd = new SqlCommand("mostrarProductoId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", parametros.id);
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if( await reader.ReadAsync())
                        {
                            var result = new Mproducto()
                            {
                                id = reader.GetInt32(0),
                                descripcion=reader.GetString(1),
                                precio = reader.GetDecimal(2),

                                
                            };
                            return result;
                        }
                    }
                }
            }
            return null;
        }

        public async Task InsertarProductos(Mproducto parametros)
        {
            using (var sql = new SqlConnection(cadenaConexion.cadenaSql()))
            {
                using (var cmd = new SqlCommand("insertarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descripcion", parametros.descripcion);
                    cmd.Parameters.AddWithValue("@precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();



                }
            }
            
        }
        public async Task editarProductos(Mproducto parametros)
        {
            using (var sql = new SqlConnection(cadenaConexion.cadenaSql()))
            {
                using (var cmd = new SqlCommand("editarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", parametros.id);
                    cmd.Parameters.AddWithValue("@precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();



                }
            }

        }
        public async Task eliminarProductos(Mproducto parametros)
        {
            using (var sql = new SqlConnection(cadenaConexion.cadenaSql()))
            {
                using (var cmd = new SqlCommand("eliminarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", parametros.id);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();



                }
            }

        }




    }
}
