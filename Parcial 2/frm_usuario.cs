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
    public partial class frm_usuario : Form
    {
        Conexion objConexion = new Conexion();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        int posicion = 0;

        public frm_usuario()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            obtenerDatos();
        }

        private void obtenerDatos()
        {
            ds.Clear();
            ds = objConexion.crearDs("SELECT * FROM usuarios_usss002026");
            dt = ds.Tables["usuarios_usss002026"];

            if (dt.Rows.Count > 0)
            {
                dt.PrimaryKey = new DataColumn[] { dt.Columns["idUsuario"] };
                dataGridView1.DataSource = dt.DefaultView;
                mostrarDatos();
            }
        }

        private void mostrarDatos()
        {
            if (dt.Rows.Count > 0 && posicion >= 0 && posicion < dt.Rows.Count)
            {
                txtUsuario.Text = dt.Rows[posicion]["usuario"].ToString();
                txtClave.Text = dt.Rows[posicion]["clave"].ToString();
                txtNombre.Text = dt.Rows[posicion]["nombre"].ToString();
                txtDireccion.Text = dt.Rows[posicion]["dirección"].ToString();
                txtTelefono.Text = dt.Rows[posicion]["teléfono"].ToString();
            }
        }

        private bool ValidarRestricciones()
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text;

            if (usuario.Length < 6 || usuario.Length > 16)
            {
                MessageBox.Show("El usuario debe tener entre 6 y 16 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            bool tieneLetras = false;
            bool tieneNumeros = false;

            foreach (char c in clave)
            {
                if (char.IsLetter(c)) tieneLetras = true;
                if (char.IsDigit(c)) tieneNumeros = true;
            }

            if (!tieneLetras || !tieneNumeros)
            {
                MessageBox.Show("La contraseña debe contener obligatoriamente letras y números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarRestricciones()) return;

            try
            {
                int resultado = objConexion.mantenimiento_usuarios_usss002026(
                    "INSERT",
                    0,
                    txtUsuario.Text.Trim(),
                    txtClave.Text,
                    txtNombre.Text,
                    txtDireccion.Text,
                    txtTelefono.Text
                );

                if (resultado > 0)
                {
                    MessageBox.Show("¡Usuario registrado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    obtenerDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (posicion < dt.Rows.Count - 1)
            {
                posicion++;
                mostrarDatos();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion--;
                mostrarDatos();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE usuarios_usss002026 SET clave='" + txtClave.Text + "' WHERE usuario='" + txtUsuario.Text + "'";
            objConexion.crearDs(sql);
            MessageBox.Show("Registro modificado correctamente");
            mostrarDatos();
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtClave.Text = "";
           
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM usuarios_usss002026 WHERE usuario='" + txtUsuario.Text + "'";
            objConexion.crearDs(sql);
            MessageBox.Show("Registro eliminado correctamente");
            mostrarDatos();
        }

        private void btnPrimeroAlumno_Click(object sender, EventArgs e)
        {
            posicion = 0;
            mostrarDatos();
        }

        private void btnAnteriorAlumno_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion--;
                mostrarDatos();
            }
        }

        private void btnSiguienteAlumno_Click(object sender, EventArgs e)
        {
            if (posicion < dt.Rows.Count - 1)
            {
                posicion++;
                mostrarDatos();
            }
        }

        private void btnUltimoAlumno_Click(object sender, EventArgs e)
        {
            posicion = dt.Rows.Count - 1;
            mostrarDatos();
        }
    }
}