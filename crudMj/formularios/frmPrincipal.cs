using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//my using
using System.Data.Sql;
using System.Data.SqlClient;

//custom class
using conexionDb;
using crudMj;

namespace formularios
{
    public partial class frmPrincipal : Form
    {

        DataTable dt;
        Manejadora manejadora;
        

        public frmPrincipal()
        {
            InitializeComponent();
            this.manejadora = new Manejadora();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigurarDataGridView();
            ConfigurarDataTable();
            this.dataGridView.DataSource = this.dt;

            RefrecarDataGridView();
        }

        public static VincularDb GenerarConexionDb()
        {
            SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexionDb);
            VincularDb db = new VincularDb(conexion);
            return db;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAlta frmAlta = new frmAlta();
            frmAlta.StartPosition = FormStartPosition.CenterParent;
            frmAlta.ShowDialog();

            VincularDb db = GenerarConexionDb();
            this.manejadora.listaProductos = db.ObtenerListado(); 
            
            RefrecarDataGridView();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView.CurrentCell.OwningRow;
            string id = row.Cells["Id"].Value.ToString();
            Producto productoModificar = this.manejadora.BuscarPorId(int.Parse(id));

            frmModificar frmModificar = new frmModificar(this.manejadora, productoModificar);

            frmModificar.StartPosition = FormStartPosition.CenterParent;
            frmModificar.ShowDialog();

            RefrecarDataGridView();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView.CurrentCell.OwningRow;
            string id = row.Cells["Id"].Value.ToString();
            VincularDb db = GenerarConexionDb();
            bool eliminado = db.Eliminar(int.Parse(id));

            if (eliminado)
                MessageBox.Show("Se ha eliminado de la base de datos!");
            else
                MessageBox.Show("Se no se ha podido eliminar de la base de datos.");

            this.RefrecarDataGridView();
        }


        public void ConfigurarDataTable()
        {
            this.dt = new DataTable("Productos");
            this.dt.Columns.Add("Id", typeof(int));
            this.dt.Columns.Add("Nombre", typeof(string));
            this.dt.Columns.Add("Descripcion", typeof(string));
            this.dt.Columns.Add("Cantidad", typeof(int));

            this.dt.PrimaryKey = new DataColumn[] { this.dt.Columns[0] };
        }

        public void ConfigurarDataGridView()
        {            
            this.dataGridView.MultiSelect = false;
            this.dataGridView.ReadOnly = true;
            this.dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }       

        public void RefrecarDataGridView()
        {
            //Limpia dataGrid
            this.dataGridView.Columns.Clear();
            this.ConfigurarDataTable();

            try
            {
                VincularDb db = GenerarConexionDb();
                this.manejadora.listaProductos = db.ObtenerListado();
            }
            catch(Exception)
            {
                this.btnAgregar.Enabled = false;
                this.btnModificar.Enabled = false;
                this.btnEliminar.Enabled = false;
                MessageBox.Show("No se ha podido conectar con la base de datos");
            }

            

            foreach (Producto aux in this.manejadora.listaProductos)
            {

                DataRow fila = this.dt.NewRow();
                fila["Id"] = aux.id;
                fila["Nombre"] = aux.nombre;
                fila["Descripcion"] = aux.descripcion;
                fila["Cantidad"] = aux.cantidad;
                this.dt.Rows.Add(fila);
            }

            //Carga dataGridView con los valores del dataTable.
            this.dataGridView.DataSource = this.dt;

        }
    }
}
