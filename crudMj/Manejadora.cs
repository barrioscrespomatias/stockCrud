using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace crudMj
{
    public class Manejadora
    {
        public List<Producto> listaProductos;

        public Manejadora()
        {
            this.listaProductos = new List<Producto>();
        }

        public Producto BuscarPorId(int id)
        {            
            Producto aux = new Producto(null, null, 0);

            foreach(Producto item in this.listaProductos)
            {
                if (item.id == id)
                {
                    aux = item;
                }                    
            }
            return aux;            
        }

        public static bool operator == (Manejadora manejadora, Producto nuevoProducto)
        {
            bool retorno = false;            

            foreach(Producto productoExistente in manejadora.listaProductos)
            {
                if(productoExistente == nuevoProducto)
                {
                    retorno = true;
                    break;
                }
            }
            
            return retorno;
        }

        public static bool operator !=(Manejadora manejadora, Producto nuevoProducto)
        {
            return !(manejadora == nuevoProducto);
        }
    }
}
