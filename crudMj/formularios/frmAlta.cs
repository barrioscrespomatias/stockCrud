using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//custom class
using crudMj;
using conexionDb;

namespace formularios
{
    public partial class frmAlta : frmTomaDatos
    {
        public frmAlta()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = this.txtNombre.Text;
            string descripcion = this.txtDescripcion.Text;
            int cantidad = int.Parse(this.txtCantidad.Text);

            Producto nuevoProducto = new Producto(nombre, descripcion, cantidad);
            VincularDb db =  frmPrincipal.GenerarConexionDb();
            bool agregado =  db.Agregar(nuevoProducto);

            if (agregado)
                MessageBox.Show("Se ha agregado a la base de datos!");
            else
                MessageBox.Show("Se no se ha podido agregar a la base de datos.");
            this.Close();

        }

    }
}
