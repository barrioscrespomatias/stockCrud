using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudMj
{
    public class Producto
    {
        public int id;
        public string nombre;
        public int cantidad;
        public string descripcion;


        public Producto(string nombre, string descripcion, int cantidad)
        {
            this.nombre = nombre;
            this.cantidad = cantidad;
            this.descripcion = descripcion;
        }


        /// <summary>
        /// sobrecarga constructor DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="cantidad"></param>
        /// <param name="descripcion"></param>
        public Producto(int id, string nombre, string descripcion, int cantidad)
            :this(nombre,descripcion,cantidad)
        {
            this.id = id;
        }

        public static bool operator == (Producto p1, Producto p2)
        {
            bool retorno = false;
            if (p1.Equals(p2))
                retorno = true;
            return retorno;
        }

        public static bool operator !=(Producto p1, Producto p2)
        {
            return !(p1 == p2);
        }

        public override bool Equals(object obj)
        {
            
            bool retorno = false;
            if (obj is Producto)
                retorno = this.nombre == ((Producto)obj).nombre && this.descripcion == ((Producto)obj).descripcion;
            return retorno;
        }

    }
}
