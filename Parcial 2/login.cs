using miPrimeaAplicacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace miPrimerAplicacion
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text;

            // Validación de campos vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Por favor, complete ambos campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Conexion objConexion = new Conexion();
            string sql = "SELECT * FROM usuarios_usss002026 WHERE usuario = '" + usuario + "' AND clave = '" + clave + "'";
            DataSet ds = objConexion.crearDs(sql);

            // Verificación de credenciales en la base de datos
            if (ds.Tables["usuarios_usss002026"].Rows.Count > 0)
            {
                MessageBox.Show("¡Acceso concedido al sistema!", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frm_usuario usuarios = new frm_usuario();
                usuarios.Show();
                this.Hide();
             
            }
            else
            {
                MessageBox.Show("Acceso denegado: Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}