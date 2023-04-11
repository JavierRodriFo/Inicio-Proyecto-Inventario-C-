using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaPrentacion
{
    public partial class VistaGeneral : Form
    {
        public VistaGeneral()
        {
            InitializeComponent();
        }

        private void VistaGeneral_Load(object sender, EventArgs e)
        {
            List<Cliente> lista = new CN_Cliente().Listar();

            foreach (Cliente item in lista)
            {

                dgvdata.Rows.Add(new object[] { "",item.IdCliente, item.Nombres, item.Apellidos, item.Direccion });



            



            }

        }








        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Cliente objcliente = new Cliente()
            {

                IdCliente = Convert.ToInt32(txtid.Text),
                Nombres = txtnombre.Text,
                Apellidos = txtapellido.Text,
                Direccion = txtdireccion.Text,
                



            };

            if (objcliente.IdCliente == 0)
            {

                int idgenerado = new CN_Cliente().Registrar(objcliente, out mensaje);

                if (idgenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] { "", txtid.Text, txtnombre.Text, txtapellido.Text, txtdireccion.Text });

                    limpiar();

                }
                else
                {
                    MessageBox.Show(mensaje);

                }



            }
            else
            {
                bool resultado = new CN_Cliente().Editar(objcliente, out mensaje);

                if (resultado)
                {

                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtxindice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Nombres"].Value = txtnombre.Text;
                    row.Cells["Apellidos"].Value = txtapellido.Text;
                    row.Cells["direccion"].Value = txtdireccion.Text;
                    
                    limpiar();

                }
                else
                {
                    MessageBox.Show(mensaje);

                }
            }


        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eleiminar el cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Cliente objcliente = new Cliente()
                    {

                        IdCliente = Convert.ToInt32(txtid.Text),



                    };

                    bool respuesta = new CN_Cliente().Eliminar(objcliente, out mensaje);
                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtxindice.Text));
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    }
                }



            }

        }

        private void limpiar() {
            txtxindice.Text= "-1";
            txtid.Text = "0";
            txtnombre.Text= "";
            txtapellido.Text = "";
            txtdireccion.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {


                int indice = e.RowIndex;
                if (indice >= 0)
                {

                    txtxindice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["id"].Value.ToString();
                    txtnombre.Text = dgvdata.Rows[indice].Cells["nombres"].Value.ToString();
                    txtapellido.Text = dgvdata.Rows[indice].Cells["apellidos"].Value.ToString();
                    txtdireccion.Text = dgvdata.Rows[indice].Cells["direccion"].Value.ToString();
                    




                    


                }
            }
        }

       
    }
}
