using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using crudMj;
using conexionDb;

using System.Data.Sql;
using System.Data.SqlClient;

namespace formularios
{
    public partial class frmModificar : frmTomaDatos
    {
        Manejadora _manejadora;
        Producto _producto;
        
        public frmModificar(Manejadora manejadora, Producto producto)
        {
            InitializeComponent();
            this._manejadora = manejadora;
            this._producto = producto;
        }

        public void CargarComponente ()
        {
            this.txtNombre.Text = this._producto.nombre;
            this.txtDescripcion.Text = this._producto.descripcion;
            this.txtCantidad.Text = this._producto.cantidad.ToString();

        }

        private void frmModificar_Load(object sender, EventArgs e)
        {
            this.CargarComponente();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string nombre = this.txtNombre.Text;
            string descripcion = this.txtDescripcion.Text;
            int cantidad = int.Parse(this.txtCantidad.Text);

            Producto nuevoProducto = new Producto(this._producto.id, nombre, descripcion, cantidad);
            VincularDb db = frmPrincipal.GenerarConexionDb();
            bool modificado = db.Modificar(nuevoProducto);

            if (modificado)
                MessageBox.Show("Se ha modificad la base de datos!");
            else
                MessageBox.Show("No se ha podido modificar la base de datos.");
            this.Close();

        }
    }
}
