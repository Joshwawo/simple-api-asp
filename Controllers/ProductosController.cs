using Microsoft.AspNetCore.Mvc;
using tienda.Data;
using tienda.Models;

namespace tienda.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController:ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Mproducto>>> GetProducts()
        {
            var productos = new Dproductos();
            var lista = await productos.MostrarProductos();
            return lista;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mproducto?>> GetProducto(int id)
        {
            var producto = new Dproductos();
            var parametros = new Mproducto();
            parametros.id = id;
            var result = await producto.MostarProductoxId(parametros);
            if(result == null)
            {
                return NotFound(new { message= "No se encontro el producto", statusCode = 404});
            }
            return result;
            
            
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Mproducto parametros)
        {
            var productos = new Dproductos();
            await productos.InsertarProductos(parametros);
            return Ok(parametros);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Mproducto parametros)
        {
            var productos = new Dproductos();
            parametros.id = id;
            await productos.editarProductos(parametros);
            return Ok(new { message = "Producto actualizado correctamente", statusCode = 200 });


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var producto = new Dproductos();
            var parametros = new Mproducto();
            parametros.id = id;
            await producto.eliminarProductos(parametros);
            return Ok(new { message = "Producto eliminado correctamente", statusCode = 200 });
        }
    }

}
