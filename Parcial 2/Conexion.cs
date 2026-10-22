using System;
using System.Collections.Generic;
using System.Data; //Esta libreria es para usar bases de datos
using System.Data.SqlClient; //Esta libreria permite usar SQLServer
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace miPrimeaAplicacion {
    internal class Conexion {
        //Definir los miembros, atributos y metodos de la clase
        public SqlConnection objConexion = new SqlConnection(); //Conectarme a la BD
        public SqlCommand objComando = new SqlCommand(); //Ejecutar consultas (Insert, update, delete, select) SQL en la BD
        public SqlDataAdapter objDataAdapter = new SqlDataAdapter();//un puente entre la BD y la aplicacion.
        DataSet objDs = new DataSet(); //Representa una copia en memoria de la arquitectura de la BD

        public Conexion() {//Constructor e inicializador de los miembros de la clase
            String cadenaCoenxion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_academica.mdf;Integrated Security=True";
            objConexion.ConnectionString = cadenaCoenxion;
            objConexion.Open(); //abrir la BD
        }
        public DataSet obtenerDatos() {
            objDs.Clear();//Limpiar el datase.
            objComando.Connection = objConexion;//Establcer la conexion para ejecutar consultas a la BD

            objDataAdapter.SelectCommand = objComando;

            objComando.CommandText = "SELECT * FROM alumnos";
            objDataAdapter.Fill(objDs, "alumnos"); //tomamos los datos de la BD y llenamos el ds

            objComando.CommandText = "SELECT * FROM materias";
            objDataAdapter.Fill(objDs, "materias");

            return objDs;
        }
        public string administrarDatosAlumnos(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo"){
                sql = "INSERT INTO alumnos(codigo,nombre,direccion,telefono,email) VALUES ('" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "', '" + datos[4] + "', '" + datos[5] + "')";
            }else if (accion == "modificar"){
                sql = "UPDATE alumnos SET codigo='" + datos[1] + "', nombre='" + datos[2] + "', direccion='" + datos[3] + "', telefono='" + datos[4] + "', email='" + datos[5] + "' WHERE idAlumno='" + datos[0] + "'";
            }else if (accion == "eliminar"){
                sql = "DELETE FROM alumnos WHERE idAlumno='" + datos[0] + "'";
            }
            return ejecutarSQL(sql);
        }
        public string administrarDatosMaterias(String[] datos, String accion){
            String sql = "";
            if (accion == "nuevo"){
                sql = "INSERT INTO materias(codigo,nombre,uv) VALUES ('" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "')";
            }else if (accion == "modificar"){
                sql = "UPDATE materias SET codigo='" + datos[1] + "', nombre='" + datos[2] + "', uv='" + datos[3] + "' WHERE idMateria='" + datos[0] + "'";
            }else if (accion == "eliminar"){
                sql = "DELETE FROM materias WHERE idMateria='" + datos[0] + "'";
            }
            return ejecutarSQL(sql);
        }
        public String ejecutarSQL(String sql)
        {
            try
            {
                objComando.Connection = objConexion;
                objComando.CommandText = sql;
                return objComando.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        internal DataSet crearDs(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                objComando.Connection = objConexion;
                objComando.CommandText = sql;
                objDataAdapter.SelectCommand = objComando;
                objDataAdapter.Fill(ds, "usuarios_usss002026");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta: " + ex.Message);
            }
            return ds;
        }

        internal int mantenimiento_usuarios_usss002026(string v1, int v2, string v3, string text1, string text2, string text3, string text4)
        {
            throw new NotImplementedException();
        }
    }
}
