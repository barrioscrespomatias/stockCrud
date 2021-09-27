using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Sql;
using System.Data.SqlClient;

//custom class
using crudMj;

namespace conexionDb
{
    public class VincularDb
    {
        SqlConnection conexion;
        SqlCommand comando;
        SqlDataReader lector;

        public VincularDb(SqlConnection cn)
        {
            this.conexion = cn;
        }

        public bool Agregar(Producto nuevoProducto)
        {
            bool retorno = false;

            string sql = "INSERT INTO productos(nombre, descripcion, cantidad) ";
            sql += "VALUES (@nombre,@descripcion,@cantidad)";
            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.Connection = conexion;

                this.comando.Parameters.AddWithValue("@nombre", nuevoProducto.nombre);
                this.comando.Parameters.AddWithValue("@descripcion", nuevoProducto.descripcion);
                this.comando.Parameters.AddWithValue("@cantidad", nuevoProducto.cantidad);

                this.comando.CommandText = sql;
                conexion.Open();

                int filasAfectadas = comando.ExecuteNonQuery();
                if (filasAfectadas > 0)
                {
                    retorno = true;
                }
            }
            catch (Exception e)
            {
                retorno = false;
                throw e;

            }
            finally
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                    conexion.Close();
            }

            return retorno;
        }

        public List<Producto> ObtenerListado()
        {

            List<Producto> listaProductos = new List<Producto>();

            string sql = "SELECT * FROM productos";

            this.comando = new SqlCommand();
            this.conexion = new SqlConnection(Properties.Settings.Default.conexionDb);

            try
            {
                this.comando.Connection = conexion;
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.CommandText = sql;

                conexion.Open();
                lector = comando.ExecuteReader();


                Producto auxiliar;

                int id;
                string nombre;
                string descripcion;
                int cantidad;

                while (lector.Read())
                {
                    id = lector.GetInt32(0);
                    nombre = lector.GetString(1);
                    descripcion = lector.GetString(2);
                    cantidad = lector.GetInt32(3);                    

                    auxiliar = new Producto(id, nombre, descripcion, cantidad);
                    listaProductos.Add(auxiliar);
                }

            }
            catch (Exception)
            {
                throw new Exception("Error en la conexion a la base de datos");

            }
            finally
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                    conexion.Close();
            }

            return listaProductos;
        }

        public bool Modificar(Producto productoModificar)
        {
            bool retorno = false;
            string sql = "UPDATE productos SET nombre=@nombre, descripcion=@descripcion," +
                " cantidad=@cantidad WHERE id=@id";


            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.Connection = conexion;

                this.comando.Parameters.AddWithValue("@id", productoModificar.id);
                this.comando.Parameters.AddWithValue("@nombre", productoModificar.nombre);
                this.comando.Parameters.AddWithValue("@descripcion", productoModificar.descripcion);
                this.comando.Parameters.AddWithValue("@cantidad", productoModificar.cantidad);

                this.comando.CommandText = sql;
                conexion.Open();
                int filasAfectadas = comando.ExecuteNonQuery();
                if (filasAfectadas > 0)
                {
                    retorno = true;
                }
            }
            catch (Exception e)
            {
                retorno = false;
                throw e;
            }
            finally
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                    conexion.Close();
            }

            return retorno;
        }

        public bool Eliminar(int id)
        {
            bool retorno = false;
            string sql = "DELETE FROM productos WHERE id=@id";


            try
            {
                this.comando = new SqlCommand();
                this.comando.CommandType = System.Data.CommandType.Text;
                this.comando.Connection = conexion;

                this.comando.Parameters.AddWithValue("@id", id);

                this.comando.CommandText = sql;
                conexion.Open();
                int filasAfectadas = comando.ExecuteNonQuery();
                if (filasAfectadas > 0)
                {
                    retorno = true;
                }
            }
            catch (Exception e)
            {
                retorno = false;
                throw e;
            }
            finally
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                    conexion.Close();
            }

            return retorno;
        }
    }
}
