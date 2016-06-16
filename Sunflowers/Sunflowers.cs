using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sunflowers
{
    public partial class Sunflowers : Form
    {
        //--------------------------------Sunflowers--------------------------------
        public Sunflowers()
        {
            InitializeComponent();

            pPer.Enabled = true;
            pEmp.Enabled = false;

            btPer.Visible = false;
            btArt.Visible = false;

            labResPer.Visible = false;
            labResArt.Visible = false;


            //Antony
            this.sucursalComboBoxSucursal.Visible = false;
            this.insertarRadioButtonSucursal.Select();
            this.mensajeLabelSucursal.Text = "";
            this.cargarProvincias(this.provinciaComboBoxSucursal);
            cargarEmpleadosFuncionesSuscursales();
            cargarEmpleadosHorarios();


            //Brandon
            cargarVentas();
            cargarArticulosLb();
            cargarProvincias(cbProvincia);
            cargarArticulos();
        }
        //-----------------------------------------------------------------------


        //--------------------------------Sunflowers Load--------------------------------
        private void Sunflowers_Load(object sender, EventArgs e)
        {
            cargarProvincias(cBProPer);
            CargarProveedores();
            CargarArticulosDisponibles();
        }
        //-----------------------------------------------------------------------


        ////////////////////////////////////////////////////////////////////////////////////////////
        //                                   JEUDRIN                                              // 
        ////////////////////////////////////////////////////////////////////////////////////////////


        //--------------------------------Interfaz--------------------------------
        private void rBPer_CheckedChanged(object sender, EventArgs e)
        {
            pPer.Enabled = true;
            pEmp.Enabled = false;

            rBMosPer.Checked = true;

            mtBFecIngEmp.Text = "";
            tBSalEmp.Text = "";
            tBJorEmp.Text = "";
            tBNomSucEmp.Text = "";
        }

        private void rBEmp_CheckedChanged(object sender, EventArgs e)
        {
            pPer.Enabled = false;
            pEmp.Enabled = true;

            rBMosPer.Checked = true;

            cBProPer.Text = "";
            cBCanPer.Text = "";
            cBDisPer.Text = "";

            tBNomPer.Text = "";
            tBApe1Per.Text = "";
            tBApe2Per.Text = "";
            cBSexPer.Text = "";
            mtBFecNacPer.Text = "";
            tBDirExaPer.Text = "";
            mtBTelPer.Text = "";
            tBEmaPer.Text = "";
        }

        private void rBMosPer_CheckedChanged(object sender, EventArgs e)
        {
            if (rBPer.Checked)
            {
                pPer.Enabled = true;
                pEmp.Enabled = false;
            }
            else if (rBEmp.Checked)
            {
                pPer.Enabled = false;
                pEmp.Enabled = true;
            }

            btPer.Visible = false;
            labResPer.Visible = false;
        }

        private void rBInsPer_CheckedChanged(object sender, EventArgs e)
        {
            if (rBPer.Checked)
            {
                pPer.Enabled = true;
                pEmp.Enabled = false;
            }
            else if (rBEmp.Checked)
            {
                pPer.Enabled = false;
                pEmp.Enabled = true;
            }

            btPer.Visible = true;
            btPer.Text = "Insertar";

            labResPer.Visible = true;
            labResPer.Text = "Complete todos los campos.";
            labResPer.ForeColor = Color.Black;
        }

        private void rBModPer_CheckedChanged(object sender, EventArgs e)
        {
            if (rBPer.Checked)
            {
                pPer.Enabled = true;
                pEmp.Enabled = false;
            }
            else if (rBEmp.Checked)
            {
                pPer.Enabled = false;
                pEmp.Enabled = true;
            }

            btPer.Visible = true;
            btPer.Text = "Modificar";

            labResPer.Visible = true;
            labResPer.Text = "Ingrese una cedula y modifique los datos deseados.";
            labResPer.ForeColor = Color.Black;
        }

        private void rBEliPer_CheckedChanged(object sender, EventArgs e)
        {
            if (rBPer.Checked)
            {
                pPer.Enabled = false;
                pEmp.Enabled = false;

                labResPer.Text = "Ingrese la cedula de la persona a eliminar.";
                labResPer.ForeColor = Color.Black;
            }
            else if (rBEmp.Checked)
            {
                pPer.Enabled = false;
                pEmp.Enabled = false;

                labResPer.Text = "Ingrese la cedula del empleado a eliminar.";
                labResPer.ForeColor = Color.Black;
            }

            btPer.Visible = true;
            btPer.Text = "Eliminar";

            labResPer.Visible = true;
        }

        private void rBMosArt_CheckedChanged(object sender, EventArgs e)
        {
            labCodArt.Enabled = true;
            tBCodArt.Enabled = true;

            pArt.Enabled = true;

            btArt.Visible = false;
            labResArt.Visible = false;
        }

        private void rBDisArt_CheckedChanged(object sender, EventArgs e)
        {
            cBArt.Text = "";
            tBCodArt.Text = "";
            tBStoArt.Text = "";
            cBProArt.Text = "";
            tBPreArt.Text = "";
            cBTipVenArt.Text = "";
            cBColArt.Text = "";
            tBDesArt.Text = "";
            cBCatArt.Text = "";

            CargarArticulosDisponibles();
        }

        private void rBNoDisArt_CheckedChanged(object sender, EventArgs e)
        {
            cBArt.Text = "";
            tBCodArt.Text = "";
            tBStoArt.Text = "";
            cBProArt.Text = "";
            tBPreArt.Text = "";
            cBTipVenArt.Text = "";
            cBColArt.Text = "";
            tBDesArt.Text = "";
            cBCatArt.Text = "";

            CargarArticulosNoDisponibles();
        }

        private void rBInsArt_CheckedChanged(object sender, EventArgs e)
        {
            labCodArt.Enabled = true;
            tBCodArt.Enabled = true;

            pArt.Enabled = true;

            btArt.Visible = true;
            btArt.Text = "Insertar";

            labResArt.Visible = true;
            labResArt.Text = "Complete todos los campos.";
        }

        private void rBModArt_CheckedChanged(object sender, EventArgs e)
        {
            labCodArt.Enabled = true;
            tBCodArt.Enabled = true;

            pArt.Enabled = true;

            btArt.Visible = true;
            btArt.Text = "Modificar";

            labResArt.Visible = true;
            labResArt.Text = "Ingrese un codigo y modifique los datos deseados.";
        }

        private void rBEliArt_CheckedChanged(object sender, EventArgs e)
        {
            labCodArt.Enabled = true;
            tBCodArt.Enabled = true;

            pArt.Enabled = false;

            btArt.Visible = true;
            btArt.Text = "Eliminar";

            labResArt.Visible = true;
            labResArt.Text = "Ingrese el codigo del articulo a eliminar.";
        }
        //-----------------------------------------------------------------------


        //--------------------------------Combo Provincias Persona-------------------------------
        private void cBProPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCantones(cBProPer.Text.ToString(),cBCanPer);
        }
        //---------------------------------------------------------------------------------------


        //--------------------------------Combo Cantones Persona--------------------------------
        private void cBCanPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDistritos(cBProPer.Text, cBCanPer.Text,cBDisPer);
        }
        //--------------------------------------------------------------------------------------


        //--------------------------------Mask Text Box Cedula Persona---------------------------
        private void mtBCedPer_KeyUp(object sender, KeyEventArgs e)
        {
            if (rBPer.Checked)
                MostrarPersona(mtBCedPer.Text);
            else if (rBEmp.Checked)
                MostrarEmpleado(mtBCedPer.Text);
        }
        //---------------------------------------------------------------------------------------


        //--------------------------------Boton Persona--------------------------------
        private void btPer_Click(object sender, EventArgs e)
        {
            if (rBInsPer.Checked)
            {
                if (rBPer.Checked)
                {
                    if (mtBCedPer.Text == "" || tBNomPer.Text == "" || tBApe1Per.Text == "" || tBApe2Per.Text == "" || cBSexPer.Text == "" || cBProPer.Text == "" || cBCanPer.Text == "" || cBDisPer.Text == "" || tBDirExaPer.Text == "" || mtBFecNacPer.Text == "" || mtBTelPer.Text == "" || tBEmaPer.Text == "")
                    {
                        if (mtBCedPer.Text == "" || mtBCedPer.Text.Length < 11)
                        {
                            labResPer.Text = "La Cedula debe contener 11 Digitos.";
                            labResPer.ForeColor = Color.Red;
                        }
                        else if (mtBFecNacPer.Text == "" || mtBFecNacPer.Text.Length < 10)
                        {
                            labResPer.Text = "La Fecha de Nacimiento debe contener 10 Digitos.";
                            labResPer.ForeColor = Color.Red;
                        }
                        else if (mtBTelPer.Text == "" || mtBTelPer.Text.Length < 9)
                        {
                            labResPer.Text = "El Telefono debe contener 9 Digitos.";
                            labResPer.ForeColor = Color.Red;
                        }
                        else if (tBEmaPer.Text == "" || !verificarEmail(tBEmaPer.Text))
                        {
                            labResPer.Text = "Formato de Email no Valido.";
                            labResPer.ForeColor = Color.Red;
                        }
                        else
                        {
                            labResPer.Text = "Verifique que no queden campos vacios.";
                            labResPer.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        if (InsertarPersona(mtBCedPer.Text, tBNomPer.Text, tBApe1Per.Text, tBApe2Per.Text, cBSexPer.Text, cBProPer.Text, cBCanPer.Text, cBDisPer.Text, tBDirExaPer.Text, mtBFecNacPer.Text, mtBTelPer.Text, tBEmaPer.Text))
                        {
                            labResPer.Text = "La Inserción se realizó con exito.";
                            labResPer.ForeColor = Color.Black;

                        }
                        else
                        {
                            labResPer.Text = "Ya se insertó una persona con los mismos valores.";
                            labResPer.ForeColor = Color.Red;
                        }
                    }
                }
                else
                {
                    if (mtBCedPer.Text == "" || mtBFecIngEmp.Text == "" || tBSalEmp.Text == "" || tBJorEmp.Text == "" || tBNomSucEmp.Text == "")
                    {
                        if (mtBCedPer.Text == "" || mtBCedPer.Text.Length < 11)
                        {
                            labResPer.Text = "La Cedula debe contener 11 Digitos.";
                            labResPer.ForeColor = Color.Red;
                        }
                        else if (mtBFecIngEmp.Text == "" || mtBFecIngEmp.Text.Length < 10)
                        {
                            labResPer.Text = "La Fecha de Ingreso debe contener 10 Digitos.";
                            labResPer.ForeColor = Color.Red;
                        }
                        else
                        {
                            labResPer.Text = "Verifique que no queden campos vacios.";
                            labResPer.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        if (InsertarEmpleado(mtBCedPer.Text, mtBFecIngEmp.Text, tBSalEmp.Text, tBJorEmp.Text, tBNomSucEmp.Text))
                        {
                            labResPer.Text = "La Inserción se realizó con exito.";
                            labResPer.ForeColor = Color.Black;
                            cargarEmpleadosFunciones();
                            cargarEmpleadosHorarios();
                        }
                        else
                        {
                            labResPer.Text = "Ocurrió un error al insertar el empleado.";
                            labResPer.ForeColor = Color.Red;
                        }
                    }
                }
            }
            else if (rBModPer.Checked)
            {
                if (rBPer.Checked)
                {
                    if (mtBCedPer.Text == "" || tBNomPer.Text == "" || tBApe1Per.Text == "" || tBApe2Per.Text == "" || cBSexPer.Text == "" || cBProPer.Text == "" || cBCanPer.Text == "" || cBDisPer.Text == "" || tBDirExaPer.Text == "" || mtBFecNacPer.Text == "" || mtBTelPer.Text == "" || tBEmaPer.Text == "")
                    {
                        if (mtBCedPer.Text.Length == 11 && tBNomPer.Text == "")
                        {
                            labResPer.Text = "Esta persona aún no ha sido registrada.";
                            labResPer.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (mtBCedPer.Text == "" || mtBCedPer.Text.Length < 11)
                            {
                                labResPer.Text = "La Cedula debe contener 11 Digitos.";
                                labResPer.ForeColor = Color.Red;
                            }
                            else if (mtBFecNacPer.Text == "" || mtBFecNacPer.Text.Length < 10)
                            {
                                labResPer.Text = "La Fecha de Nacimiento debe contener 10 Digitos.";
                                labResPer.ForeColor = Color.Red;
                            }
                            else if (mtBTelPer.Text == "" || mtBTelPer.Text.Length < 9)
                            {
                                labResPer.Text = "El Telefono debe contener 9 Digitos.";
                                labResPer.ForeColor = Color.Red;
                            }
                            else if (tBEmaPer.Text == "" || !verificarEmail(tBEmaPer.Text))
                            {
                                labResPer.Text = "Formato de Email no Valido.";
                                labResPer.ForeColor = Color.Red;
                            }
                            else
                            {
                                labResPer.Text = "Verifique que no queden campos vacios.";
                                labResPer.ForeColor = Color.Red;
                            }
                        }
                    }
                    else
                    {
                        ModificarPersona(mtBCedPer.Text, tBNomPer.Text, tBApe1Per.Text, tBApe2Per.Text, cBSexPer.Text, cBProPer.Text, cBCanPer.Text, cBDisPer.Text, tBDirExaPer.Text, mtBFecNacPer.Text, mtBTelPer.Text, tBEmaPer.Text);
                        labResPer.Text = "La Modificación de los datos fué exitosa";
                        labResPer.ForeColor = Color.Black;
                    }
                }
                else
                {
                    if (mtBCedPer.Text == "" || mtBFecIngEmp.Text == "" || tBSalEmp.Text == "" || tBJorEmp.Text == "" || tBNomSucEmp.Text == "")
                    {
                        if (mtBCedPer.Text.Length == 11 && tBSalEmp.Text == "")
                        {
                            labResPer.Text = "Este empleado aún no ha sido registrado.";
                            labResPer.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (mtBCedPer.Text == "" || mtBCedPer.Text.Length < 11)
                            {
                                labResPer.Text = "La Cedula debe contener 11 Digitos.";
                                labResPer.ForeColor = Color.Red;
                            }
                            else if (mtBFecIngEmp.Text == "" || mtBFecIngEmp.Text.Length < 10)
                            {
                                labResPer.Text = "La Fecha de Ingreso debe contener 10 Digitos.";
                                labResPer.ForeColor = Color.Red;
                            }
                            else
                            {
                                labResPer.Text = "Verifique que no queden campos vacios.";
                                labResPer.ForeColor = Color.Red;
                            }
                        }

                    }
                    else
                    {
                        ModificarEmpleado(mtBCedPer.Text, mtBFecIngEmp.Text, tBSalEmp.Text, tBJorEmp.Text, tBNomSucEmp.Text);
                        labResPer.Text = "La Modificación de los datos fué exitosa";
                        labResPer.ForeColor = Color.Black;
                    }
                }
            }
            else if (rBEliPer.Checked)
            {
                if (rBPer.Checked)
                {
                    if (mtBCedPer.Text == "" || mtBCedPer.Text.Length < 11)
                    {
                        labResPer.Text = "La Cedula debe contener 11 Digitos.";
                        labResPer.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (tBNomPer.Text == "")
                        {
                            labResPer.Text = "Esta persona aún no ha sido registrada.";
                            labResPer.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (MessageBox.Show("¿Está seguro que desea Eliminar esta Persona?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                EliminarPersona(mtBCedPer.Text);
                                labResPer.Text = "La Eliminación de los datos fué exitosa";
                                labResPer.ForeColor = Color.Black;
                            }
                        }
                    }
                }
                else
                {
                    if (mtBCedPer.Text == "" || mtBCedPer.Text.Length < 11)
                    {
                        labResPer.Text = "La Cedula debe contener 11 Digitos.";
                        labResPer.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (tBSalEmp.Text == "")
                        {
                            labResPer.Text = "Este empleado aún no ha sido registrado.";
                            labResPer.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (MessageBox.Show("¿Está seguro que desea Eliminar este Empleado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                EliminarEmpleado(mtBCedPer.Text);
                                labResPer.Text = "La Eliminación de los datos fué exitosa";
                                labResPer.ForeColor = Color.Black;
                                cargarEmpleadosFunciones();
                                cargarEmpleadosHorarios();
                            }
                        }
                    }
                }
            }
        }
        //--------------------------------------------------------------------------------------


        //--------------------------------Metodo Mostrar Informacion Persona--------------------------------
        public void MostrarPersona(string cedula)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format("SELECT tp.telefono as telefono, ep.email as email, per.nombre as nombre, per.apellido1 as apellido1, per.apellido2 as apellido2, per.sexo as sexo, per.fechaNacimiento as fechaNacimiento, per.direccionExacta as direccionExacta, pro.nombre as nombreProvincia, can.nombre as nombreCanton, dis.nombre as nombredistrito FROM provincias pro inner join cantones can on pro.idProvincia = can.idProvincia inner join distritos dis on can.idCanton = dis.idCanton inner join personas per on per.idDistrito = dis.idDistrito inner join telefonosPersonas tp on per.cedula = tp.cedula inner join emailPersona ep on per.cedula = ep.cedula where per.cedula = '{0}' ", cedula);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();

                cBProPer.Text = lector["nombreProvincia"].ToString();
                cBCanPer.Text = lector["nombreCanton"].ToString();
                cBDisPer.Text = lector["nombreDistrito"].ToString();
                tBNomPer.Text = lector["nombre"].ToString();
                tBApe1Per.Text = lector["apellido1"].ToString();
                tBApe2Per.Text = lector["apellido2"].ToString();
                cBSexPer.Text = lector["sexo"].ToString();
                mtBFecNacPer.Text = lector["fechaNacimiento"].ToString();
                tBDirExaPer.Text = lector["direccionExacta"].ToString();
                mtBTelPer.Text = lector["telefono"].ToString();
                tBEmaPer.Text = lector["email"].ToString();

                conexion.Close();
            }
            catch (Exception)
            {
                cBProPer.Text = "";
                cBCanPer.Text = "";
                cBDisPer.Text = "";
                tBNomPer.Text = "";
                tBApe1Per.Text = "";
                tBApe2Per.Text = "";
                cBSexPer.Text = "";
                mtBFecNacPer.Text = "";
                tBDirExaPer.Text = "";
                mtBTelPer.Text = "";
                tBEmaPer.Text = "";
            }
        }
        //---------------------------------------------------------------------------------------------------


        //--------------------------------Metodo Insertar Persona--------------------------------
        public bool InsertarPersona(string cedula, string nombre, string ape1, string ape2, string sexo, string provincia, string canton, string distrito, string direccionExacta, string fechaNacimiento, string telefono, string email)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("select d.idDistrito from provincias p inner join cantones c on p.idProvincia=c.idProvincia inner join distritos d on c.idCanton=d.idCanton where p.nombre='{0}' and c.nombre='{1}' and d.nombre='{2}'", provincia, canton, distrito);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();
                consulta = string.Format("EXEC InsertarPersona '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}'", cedula, nombre, ape1, ape2, sexo, lector[0], direccionExacta, fechaNacimiento, telefono, email);
                lector.Close();
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
        //----------------------------------------------------------------------------------------


        //--------------------------------Metodo Modificar Persona--------------------------------
        public void ModificarPersona(string cedula, string nombre, string ape1, string ape2, string sexo, string provincia, string canton, string distrito, string direccionExacta, string fechaNacimiento, string telefono, string email)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
            conexion.Open();
            consulta = string.Format("select d.idDistrito from provincias p inner join cantones c on p.idProvincia=c.idProvincia inner join distritos d on c.idCanton=d.idCanton where p.nombre='{0}' and c.nombre='{1}' and d.nombre='{2}'", provincia, canton, distrito);
            comando = new SqlCommand(consulta, conexion);
            lector = comando.ExecuteReader();
            lector.Read();
            consulta = string.Format("EXEC ModificarPersona '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}'", cedula, nombre, ape1, ape2, sexo, lector[0], direccionExacta, fechaNacimiento, telefono, email);
            lector.Close();
            comando = new SqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        //-----------------------------------------------------------------------------------------


        //--------------------------------Metodo Eliminar Persona--------------------------------
        public void EliminarPersona(string cedula)
        {
            SqlConnection conexion;
            SqlCommand comando;
            string consulta;

            conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
            conexion.Open();
            consulta = string.Format("EXEC EliminarPersona '{0}'", cedula);
            comando = new SqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        //----------------------------------------------------------------------------------------


        //--------------------------------Metodo Mostrar Informacion Empleado--------------------------------
        public void MostrarEmpleado(string cedula)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format("SELECT emp.fechaIngreso as fechaIngreso, emp.Salario as Salario, emp.jornada as jornada, emp.NombreSucursales as NombreSucursales FROM Empleados emp WHERE emp.cedula = '{0}'", cedula);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();

                mtBFecIngEmp.Text = lector["fechaIngreso"].ToString();
                tBSalEmp.Text = lector["Salario"].ToString();
                tBJorEmp.Text = lector["jornada"].ToString();
                tBNomSucEmp.Text = lector["NombreSucursales"].ToString();

                conexion.Close();
            }
            catch (Exception)
            {
                mtBFecIngEmp.Text = "";
                tBSalEmp.Text = "";
                tBJorEmp.Text = "";
                tBNomSucEmp.Text = "";
            }
        }
        //---------------------------------------------------------------------------------------------------


        //--------------------------------Metodo Insertar Empleado--------------------------------
        public bool InsertarEmpleado(string cedula, string fechaIngreso, string Salario, string jornada, string NombreSucursales)
        {
            SqlConnection conexion;
            SqlCommand comando;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("EXEC InsertarEmpleado '{0}','{1}',{2},{3},'{4}'", cedula, fechaIngreso, Salario, jornada, NombreSucursales);
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
        //----------------------------------------------------------------------------------------


        //--------------------------------Metodo Modificar Empleado--------------------------------
        public void ModificarEmpleado(string cedula, string fechaIngreso, string Salario, string jornada, string NombreSucursales)
        {
            SqlConnection conexion;
            SqlCommand comando;
            string consulta;

            conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
            conexion.Open();
            consulta = string.Format("EXEC ModificarEmpleado '{0}','{1}',{2},{3},'{4}'", cedula, fechaIngreso, Salario, jornada, NombreSucursales);
            comando = new SqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        //-----------------------------------------------------------------------------------------


        //--------------------------------Metodo Eliminar Empleado--------------------------------
        public void EliminarEmpleado(string cedula)
        {
            SqlConnection conexion;
            SqlCommand comando;
            string consulta;

            conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
            conexion.Open();
            consulta = string.Format("EXEC EliminarEmpleado '{0}'", cedula);
            comando = new SqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        //----------------------------------------------------------------------------------------


        //--------------------------------Metodo Cargar Empleados Funciones Sucursales--------------------------------
        private void cargarEmpleadosFunciones()
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                limpiarInformacionFunciones();
                conexion = new SqlConnection("Data Source=JEUDRIN\\JEUDRIN;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = "Select cedula  from empleados";
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                this.sucursalComboBoxSucursal.Items.Clear();
                while (lector.Read())
                {
                    this.empleadoComboBoxFunciones.Items.Add(lector[0]);
                }
                conexion.Close();

                
                //empleadoComboBoxFunciones.SelectedIndex = 0;

            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------------------------------------------------------------


        //--------------------------------Combo Box Articulo--------------------------------
        private void cBArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarArticulo(cBArt.Text);
        }
        //--------------------------------Text Box Codigo Articulo--------------------------------


        //--------------------------------Text Box Codigo Articulo--------------------------------
        private void tBCodArt_TextChanged(object sender, EventArgs e)
        {
            MostrarArticulo(tBCodArt.Text);
        }
        //----------------------------------------------------------------------------------------


        //--------------------------------Boton Articulo--------------------------------
        private void btArt_Click(object sender, EventArgs e)
        {
            if (rBInsArt.Checked)
            {
                if (tBCodArt.Text == "" || tBStoArt.Text == "" || cBProArt.Text == "" || tBPreArt.Text == "" || cBTipVenArt.Text == "" || cBColArt.Text == "" || tBDesArt.Text == "" || cBCatArt.Text == "")
                {
                    if (tBCodArt.Text == "")
                    {
                        labResArt.Text = "Introduzca un Código de Artículo.";
                        labResArt.ForeColor = Color.Red;
                    }
                    else
                    {
                        labResArt.Text = "Verifique que no queden campos vacios.";
                        labResArt.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (InsertarArticulo(tBCodArt.Text, tBStoArt.Text, cBProArt.Text, tBPreArt.Text, cBTipVenArt.Text, cBColArt.Text, tBDesArt.Text, cBCatArt.Text))
                    {
                        labResArt.Text = "La Inserción se realizó con exito.";
                        labResArt.ForeColor = Color.Black;
                        CargarArticulosDisponibles();
                        CargarArticulosNoDisponibles();
                    }
                    else
                    {
                        labResArt.Text = "Ya se insertó un artículo con los mismos valores.";
                        labResArt.ForeColor = Color.Red;
                    }
                }
            }
            else if (rBModArt.Checked)
            {
                if (tBCodArt.Text == "" || tBStoArt.Text == "" || cBProArt.Text == "" || tBPreArt.Text == "" || cBTipVenArt.Text == "" || cBColArt.Text == "" || tBDesArt.Text == "" || cBCatArt.Text == "")
                {
                    if (tBCodArt.Text != "" && cBProArt.Text == "")
                    {
                        labResArt.Text = "Este artículo aún no ha sido registrado.";
                        labResArt.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (tBCodArt.Text == "")
                        {
                            labResArt.Text = "Introduzca un Código de Artículo.";
                            labResArt.ForeColor = Color.Red;
                        }
                        else
                        {
                            labResArt.Text = "Verifique que no queden campos vacios.";
                            labResArt.ForeColor = Color.Red;
                        }
                    }
                }
                else
                {
                    ModificarArticulo(tBCodArt.Text, tBStoArt.Text, cBProArt.Text, tBPreArt.Text, cBTipVenArt.Text, cBColArt.Text, tBDesArt.Text, cBCatArt.Text);
                    labResArt.Text = "La Modificación de los datos fué exitosa";
                    labResArt.ForeColor = Color.Black;
                }
            }
            else if (rBEliArt.Checked)
            {
                if (tBCodArt.Text == "")
                {
                    labResArt.Text = "Introduzca un Código de Artículo.";
                    labResArt.ForeColor = Color.Red;
                }
                else
                {
                    if (cBProArt.Text == "")
                    {
                        labResArt.Text = "Este artículo aún no ha sido registrado.";
                        labResArt.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (MessageBox.Show("¿Está seguro que desea Eliminar este Artículo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            EliminarArticulo(tBCodArt.Text);
                            labResArt.Text = "La Eliminación de los datos fué exitosa";
                            labResArt.ForeColor = Color.Black;
                            CargarArticulosDisponibles();
                            CargarArticulosNoDisponibles();
                        }
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------


        //--------------------------------Metodo Cargar Proveedores--------------------------------
        public void CargarProveedores()
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = "Select Nombre from Proveedores";
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                cBProArt.Items.Clear();
                while (lector.Read())
                {
                    cBProArt.Items.Add(lector[0]);
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //---------------------------------------------------------------------------------------------------


        //--------------------------------Metodo Cargar Articulos Disponibles--------------------------------
        public void CargarArticulosDisponibles()
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = "Select Codigo from Articulos where Stock > 0";
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                cBArt.Items.Clear();
                while (lector.Read())
                {
                    cBArt.Items.Add(lector[0]);
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //---------------------------------------------------------------------------------------------------


        //--------------------------------Metodo Cargar Articulos No Disponibles--------------------------------
        public void CargarArticulosNoDisponibles()
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = "Select Codigo from Articulos where Stock = 0";
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                cBArt.Items.Clear();
                while (lector.Read())
                {
                    cBArt.Items.Add(lector[0]);
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //---------------------------------------------------------------------------------------------------


        //--------------------------------Metodo Mostrar Informacion Articulo--------------------------------
        public void MostrarArticulo(string Codigo)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format("SELECT art.Codigo as Codigo, art.Stock as Stock, pro.Nombre as nombreProveedor, art.Precio as Precio, art.TipoVenta as TipoVenta, art.Color as Color, art.Descripcion as Descripcion, art.Categoria as Categoria FROM Articulos art inner join Proveedores pro on art.idProveedor = pro.idProveedor where art.Codigo = {0}", Codigo);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();

                cBArt.Text = lector["Codigo"].ToString();
                tBCodArt.Text = lector["Codigo"].ToString();
                tBStoArt.Text = lector["Stock"].ToString();
                cBProArt.Text = lector["nombreProveedor"].ToString();
                tBPreArt.Text = lector["Precio"].ToString();
                cBTipVenArt.Text = lector["TipoVenta"].ToString();
                cBColArt.Text = lector["Color"].ToString();
                tBDesArt.Text = lector["Descripcion"].ToString();
                cBCatArt.Text = lector["Categoria"].ToString();

                conexion.Close();
            }
            catch (Exception)
            {
                cBArt.Text = "";
                tBStoArt.Text = "";
                cBProArt.Text = "";
                tBPreArt.Text = "";
                cBTipVenArt.Text = "";
                cBColArt.Text = "";
                tBDesArt.Text = "";
                cBCatArt.Text = "";
            }
        }
        //---------------------------------------------------------------------------------------------------


        //--------------------------------Metodo Insertar Articulo--------------------------------
        public bool InsertarArticulo(string Codigo, string Stock, string Proveedor, string Precio, string TipoVenta, string Color, string Descripcion, string Categoria)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("select p.idProveedor from Proveedores p where p.Nombre = '{0}'", Proveedor);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();
                consulta = string.Format("EXEC InsertarArticulo {0},{1},{2},{3},'{4}','{5}','{6}','{7}'", Codigo, Stock, lector[0], Precio, TipoVenta, Color, Descripcion, Categoria);
                lector.Close();
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
        //----------------------------------------------------------------------------------------


        //--------------------------------Metodo Modificar Articulo--------------------------------
        public void ModificarArticulo(string Codigo, string Stock, string Proveedor, string Precio, string TipoVenta, string Color, string Descripcion, string Categoria)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
            conexion.Open();
            consulta = string.Format("select p.idProveedor from Proveedores p where p.Nombre = '{0}'", Proveedor);
            comando = new SqlCommand(consulta, conexion);
            lector = comando.ExecuteReader();
            lector.Read();
            consulta = string.Format("EXEC ModificarArticulo {0},{1},{2},{3},'{4}','{5}','{6}','{7}'", Codigo, Stock, lector[0], Precio, TipoVenta, Color, Descripcion, Categoria);
            lector.Close();
            comando = new SqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        //-----------------------------------------------------------------------------------------


        //--------------------------------Metodo Eliminar Articulo--------------------------------
        public void EliminarArticulo(string Codigo)
        {
            SqlConnection conexion;
            SqlCommand comando;
            string consulta;

            conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
            conexion.Open();
            consulta = string.Format("EXEC EliminarArticulo {0}", Codigo);
            comando = new SqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        //----------------------------------------------------------------------------------------


        ////////////////////////////////////////////////////////////////////////////////////////////
        //                                   ANTONY                                               // 
        ////////////////////////////////////////////////////////////////////////////////////////////


        //--------------------------------Metodo Cargar Provincias--------------------------------
        public void cargarProvincias(ComboBox cB)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = "Select Nombre from provincias";
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                cB.Items.Clear();
                while (lector.Read())
                {
                    cB.Items.Add(lector[0].ToString());
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //----------------------------------------------------------------------------------------


        //--------------------------------Metodo Cargar Cantones--------------------------------
        public void cargarCantones(string provincia, ComboBox cB)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = String.Format("select c.nombre from provincias p inner join cantones c on p.idProvincia=c.idProvincia where p.nombre='{0}'", provincia);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                cB.Items.Clear();
                while (lector.Read())
                {
                    cB.Items.Add(lector[0].ToString());
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //--------------------------------------------------------------------------------------


        //--------------------------------Metodo Cargar Distritos--------------------------------
        public void cargarDistritos(string provincia, string canton, ComboBox cB)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = String.Format("select d.nombre from provincias p inner join cantones c on p.idProvincia=c.idProvincia inner join distritos d on c.idCanton=d.idCanton where p.nombre='{0}' and c.nombre='{1}'", provincia, canton);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                cB.Items.Clear();
                while (lector.Read())
                {
                    cB.Items.Add(lector[0].ToString());
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //---------------------------------------------------------------------------------------


        //--------------------------------Metodo Verificar Email Persona--------------------------------
        private Boolean verificarEmail(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //---------------------------------------------------------------------------------------------------


        public void consultarInformacionSucursales(string nombre)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {

                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format("select s.nombre,p.nombre as nombreProvincia, c.nombre as nombreCanton, d.nombre as nombredistrito, direccionExacta from provincias p inner join cantones c on p.idProvincia=c.idProvincia inner join distritos d on c.idCanton=d.idCanton inner join Sucursales s on s.idDistrito=d.idDistrito where s.nombre='{0}'", nombre);//nombre = idSucursal
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();

                this.provinciaComboBoxSucursal.Text = lector["nombreProvincia"].ToString();
                this.provinciaComboBoxSucursal.SelectedItem = lector["nombreProvincia"].ToString();
                this.cantonComboBoxSucursal.Text = lector["nombreCanton"].ToString();
                this.cantonComboBoxSucursal.SelectedItem = lector["nombreCanton"].ToString();
                this.distritoComboBoxSucursal.Text = lector["nombreDistrito"].ToString();
                this.distritoComboBoxSucursal.SelectedItem = lector["nombreDistrito"].ToString();
                this.direccionExactarichTextBoxSucursal.Text = lector["direccionExacta"].ToString();
                conexion.Close();
                this.cargarEmailSucursal(nombre, this.emailListBoxSucursal);
                this.cargarTelefonosSucursal(nombre, this.telefonoListBoxSucursal);

            }
            catch (SqlException e)
            {
                this.mensajeLabelSucursal.Text = "Por favor revise los datos ingresados e intentelo de nuevo!";
                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void previewInformacionSucursales(string nombre)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {

                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format("select s.nombre,p.nombre as nombreProvincia, c.nombre as nombreCanton, d.nombre as nombredistrito, direccionExacta from provincias p inner join cantones c on p.idProvincia=c.idProvincia inner join distritos d on c.idCanton=d.idCanton inner join Sucursales s on s.idDistrito=d.idDistrito where s.nombre='{0}'", nombre);//nombre = idSucursal
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();

                this.provinciaComboBoxSucursal.Text = lector["nombreProvincia"].ToString();
                this.provinciaComboBoxSucursal.SelectedItem = lector["nombreProvincia"].ToString();
                this.cantonComboBoxSucursal.Text = lector["nombreCanton"].ToString();
                this.cantonComboBoxSucursal.SelectedItem = lector["nombreCanton"].ToString();
                this.distritoComboBoxSucursal.Text = lector["nombreDistrito"].ToString();
                this.distritoComboBoxSucursal.SelectedItem = lector["nombreDistrito"].ToString();
                this.direccionExactarichTextBoxSucursal.Text = lector["direccionExacta"].ToString();
                conexion.Close();
                this.cargarEmailSucursal(nombre, this.emailListBoxSucursal);
                if (this.emailListBoxSucursal.Items.Count != 0)
                {
                    this.emailListBoxSucursal.SelectedIndex = 0;
                }
                this.cargarTelefonosSucursal(nombre, this.telefonoListBoxSucursal);
                if (this.telefonoListBoxSucursal.Items.Count != 0)
                {
                    this.telefonoListBoxSucursal.SelectedIndex = 0;
                }
            }
            catch (Exception e)
            {
                this.provinciaComboBoxSucursal.Text = "";
                this.cantonComboBoxSucursal.Text = "";
                this.distritoComboBoxSucursal.Text = "";
                this.direccionExactarichTextBoxSucursal.Text = "";
                this.telefonoListBoxSucursal.Items.Clear();
                this.emailListBoxSucursal.Items.Clear();

            }
        }


        public string generarConsultaInsertarEmailSucursales(string nombreSucursal, string[] emails)
        {
            string consulta = "";
            for (int i = 0; i < emails.Length; i++)
            {
                consulta += string.Format(" exec insEmailSucursales '{0}', '{1}' ", nombreSucursal, emails[i]);
            }
            return consulta;
        }


        public string generarConsultaInsertarTelefonosSucursales(string nombreSucursal, string[] telefonos)
        {
            string consulta = "";
            for (int i = 0; i < telefonos.Length; i++)
            {
                consulta += string.Format(" exec insTelefonosSucursales '{0}', '{1}' ", nombreSucursal, telefonos[i]);
            }
            return consulta;
        }


        public Boolean insertarSucursal(string nombreSucursal, string provincia, string canton, string distrito, string Direccion)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format(" select d.idDistrito from provincias p inner join cantones c on p.idProvincia=c.idProvincia inner join distritos d on c.idCanton=d.idCanton where p.nombre='{0}' and c.nombre='{1}' and d.nombre='{2}'", provincia, canton, distrito);

                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();
                consulta = string.Format(" exec insSucursal '{0}', '{1}' ,'{2}' ", nombreSucursal, lector[0], Direccion);
                lector.Close();
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();

                conexion.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }


        public Boolean modificarEmailTelefonoSucursal(string consulta)
        {
            SqlConnection conexion;
            SqlCommand comando;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException e)
            {

                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public Boolean modificarSucursal(string nombreSede, string provincia, string canton, string distrito, string Direccion)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("select d.idDistrito from provincias p inner join cantones c on p.idProvincia=c.idProvincia inner join distritos d on c.idCanton=d.idCanton where p.nombre='{0}' and c.nombre='{1}' and d.nombre='{2}'", provincia, canton, distrito);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();
                consulta += string.Format(" exec modDistritoSucursal '{0}', {1} ", nombreSede, lector[0]);
                consulta += string.Format(" exec modDireccionExactaSucursal '{0}', '{1}' ", nombreSede, Direccion);
                lector.Close();
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException e)
            {

                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public Boolean eliminarSucursal(string nombre)
        {
            SqlConnection conexion;
            SqlCommand comando;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("exec borrarSucursal '{0}' ", nombre);
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public Boolean agregarFuncion(string cedula, string nombre, string descripcion)
        {

            SqlConnection conexion;
            SqlCommand comando;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("exec insFuncion  '{0}', '{1}', '{2}' ", cedula, nombre, descripcion);
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public Boolean eliminarFuncion(string cedula, string nombre, string descripcion)
        {

            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format("select idFuncion from Funciones f where f.cedulaEmpleado= '{0}' and f.nombre='{1}' and f.descripcion='{2}' ", cedula, nombre, descripcion);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();



                consulta = string.Format(" exec borrarFuncion  {0} ", lector[0]);
                lector.Close();
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public Boolean agregarHorario(string cedula, string dia, string horaEntrada, string horaSalida)
        {
            SqlConnection conexion;
            SqlCommand comando;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format(" exec insHorario '{0}', '{1}', '{2}', '{3}' ", cedula, dia, horaEntrada, horaSalida);
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
        }


        public Boolean eliminarHorario(string cedula, string dia, string horaEntrada, string horaSalida)
        {

            SqlConnection conexion;
            SqlCommand comando;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format(" exec borrarHorario '{0}',  '{1}', '{2}', '{3}' ", cedula, dia, horaEntrada, horaSalida);
                comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
        }


        public void cargarSucursales()
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = "Select nombre from sucursales";
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                this.sucursalComboBoxSucursal.Items.Clear();
                while (lector.Read())
                {
                    this.sucursalComboBoxSucursal.Items.Add(lector[0]);
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void cargarTelefonosSucursal(string sucursal, ListBox lB)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = String.Format("select tS.telefono from telefonosSucursales tS  where tS.nombreSucursales = '{0}'", sucursal);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lB.Items.Clear();
                while (lector.Read())
                {
                    lB.Items.Add(lector[0].ToString());
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void cargarEmailSucursal(string sucursal, ListBox lB)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = String.Format("select tS.email from emailSucursales tS  where tS.nombreSucursales = '{0}'", sucursal);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lB.Items.Clear();
                while (lector.Read())
                {
                    lB.Items.Add(lector[0].ToString());
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void verRadioButtonSucursal_CheckedChanged(object sender, EventArgs e)
        {
            this.sucursalComboBoxSucursal.Visible = true;
            this.sucursalTextBoxSucursal.Visible = false;
            this.provinciaComboBoxSucursal.Enabled = true;
            this.cantonComboBoxSucursal.Enabled = true;
            this.distritoComboBoxSucursal.Enabled = true;
            this.direccionExactarichTextBoxSucursal.Enabled = true;
            this.operacionButtonSucursal.Text = "Borrar";
            this.operacionButtonSucursal.Visible = false;
            this.mensajeLabelSucursal.Text = "";

            this.agregarEmailButtonSucursal.Visible = false;
            this.removerEmailButtonSucursal.Visible = false;
            this.agregarTelefonoButtonSucursal.Visible = false;
            this.removerTelefonoButtonSucural.Visible = false;
            this.emailTextBoxSucursal.Visible = false;
            this.telefonoMaskedTextBoxSucursal.Visible = false;
            this.emailListBoxSucursal.Visible = true;
            this.telefonoListBoxSucursal.Visible = true;
            this.emailLabelSucursal.Visible = true;
            this.telefonoLabelSucursal.Visible = true;
            this.emailListBoxSucursal.Items.Clear();
            this.telefonoListBoxSucursal.Items.Clear();

            this.cargarSucursales();
        }


        private void insertarRadioButtonSucursal_CheckedChanged(object sender, EventArgs e)
        {
            //this.provinciaComboBoxSucursal.SelectedIndex = -1;
            //this.cantonComboBoxSucursal.SelectedIndex = -1;
            //this.distritoComboBoxSucursal.SelectedIndex = -1;
            this.operacionButtonSucursal.Visible = true;
            //this.provinciaComboBoxSucursal.Text = "";
            //this.cantonComboBoxSucursal.Text = "";
            //this.distritoComboBoxSucursal.Text = "";
            //this.direccionExactarichTextBoxSucursal.Text = "";
            this.sucursalComboBoxSucursal.Visible = false;
            this.sucursalTextBoxSucursal.Visible = true;
            this.provinciaComboBoxSucursal.Enabled = true;
            this.cantonComboBoxSucursal.Enabled = true;
            this.distritoComboBoxSucursal.Enabled = true;
            this.direccionExactarichTextBoxSucursal.Enabled = true;
            this.operacionButtonSucursal.Text = "Insertar";
            this.mensajeLabelSucursal.Text = "";

            this.agregarEmailButtonSucursal.Visible = true;
            this.removerEmailButtonSucursal.Visible = true;
            this.agregarTelefonoButtonSucursal.Visible = true;
            this.removerTelefonoButtonSucural.Visible = true;

            this.emailTextBoxSucursal.Visible = false;
            this.telefonoMaskedTextBoxSucursal.Visible = false;
            this.emailListBoxSucursal.Visible = false;
            this.telefonoListBoxSucursal.Visible = false;
            this.agregarEmailButtonSucursal.Visible = false;
            this.removerEmailButtonSucursal.Visible = false;
            this.agregarTelefonoButtonSucursal.Visible = false;
            this.removerTelefonoButtonSucural.Visible = false;
            this.emailLabelSucursal.Visible = false;
            this.telefonoLabelSucursal.Visible = false;
            this.emailListBoxSucursal.Items.Clear();
            this.telefonoListBoxSucursal.Items.Clear();
        }


        private void modificarRadioButtonSucursal_CheckedChanged(object sender, EventArgs e)
        {
            this.operacionButtonSucursal.Visible = true;
            this.provinciaComboBoxSucursal.Text = "";
            this.cantonComboBoxSucursal.Text = "";
            this.distritoComboBoxSucursal.Text = "";
            this.direccionExactarichTextBoxSucursal.Text = "";
            this.sucursalComboBoxSucursal.Visible = false;
            this.sucursalTextBoxSucursal.Visible = true;
            this.provinciaComboBoxSucursal.Enabled = true;
            this.cantonComboBoxSucursal.Enabled = true;
            this.distritoComboBoxSucursal.Enabled = true;
            this.direccionExactarichTextBoxSucursal.Enabled = true;
            this.operacionButtonSucursal.Text = "Modificar";
            this.mensajeLabelSucursal.Text = "";

            this.agregarEmailButtonSucursal.Visible = true;
            this.removerEmailButtonSucursal.Visible = true;
            this.agregarTelefonoButtonSucursal.Visible = true;
            this.removerTelefonoButtonSucural.Visible = true;

            this.emailTextBoxSucursal.Visible = true;
            this.telefonoMaskedTextBoxSucursal.Visible = true;
            this.emailListBoxSucursal.Visible = true;
            this.telefonoListBoxSucursal.Visible = true;
            this.emailLabelSucursal.Visible = true;
            this.telefonoLabelSucursal.Visible = true;
            this.emailListBoxSucursal.Items.Clear();
            this.telefonoListBoxSucursal.Items.Clear();

        }


        private void borrarRadioButtonSucursal_CheckedChanged(object sender, EventArgs e)
        {
            this.operacionButtonSucursal.Visible = true;
            this.sucursalComboBoxSucursal.Visible = true;
            this.sucursalTextBoxSucursal.Visible = false;
            this.agregarEmailButtonSucursal.Visible = false;
            this.removerEmailButtonSucursal.Visible = false;
            this.agregarTelefonoButtonSucursal.Visible = false;
            this.removerTelefonoButtonSucural.Visible = false;
            this.provinciaComboBoxSucursal.Enabled = false;
            this.cantonComboBoxSucursal.Enabled = false;
            this.distritoComboBoxSucursal.Enabled = false;

            this.emailTextBoxSucursal.Visible = false;
            this.telefonoMaskedTextBoxSucursal.Visible = false;

            this.direccionExactarichTextBoxSucursal.Enabled = false;
            this.operacionButtonSucursal.Text = "Borrar";
            this.mensajeLabelSucursal.Text = "";
            this.emailListBoxSucursal.Visible = true;
            this.telefonoListBoxSucursal.Visible = true;
            this.emailLabelSucursal.Visible = true;
            this.telefonoLabelSucursal.Visible = true;
            this.emailListBoxSucursal.Items.Clear();
            this.telefonoListBoxSucursal.Items.Clear();


            this.cargarSucursales();
        }


        private void sucursalComboBoxSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.consultarInformacionSucursales(this.sucursalComboBoxSucursal.SelectedItem.ToString());
            this.mensajeLabelSucursal.Text = "";
            this.mensajeLabelSucursal.Text = "";
        }


        private void sucursalTextBoxSucursal_KeyUp(object sender, KeyEventArgs e)
        {
            this.previewInformacionSucursales(this.sucursalTextBoxSucursal.Text);
            this.mensajeLabelSucursal.Text = this.sucursalTextBoxSucursal.Text;
            this.mensajeLabelSucursal.Text = "";
        }


        private void operacionButtonSucursal_Click(object sender, EventArgs e)
        {

            try
            {
                if (provinciaComboBoxSucursal.SelectedItem.ToString() == provinciaComboBoxSucursal.Text && cantonComboBoxSucursal.SelectedItem.ToString() == cantonComboBoxSucursal.Text && distritoComboBoxSucursal.SelectedItem.ToString() == distritoComboBoxSucursal.Text)
                {
                    if (this.insertarRadioButtonSucursal.Checked)
                    {
                        if (this.insertarSucursal(this.sucursalTextBoxSucursal.Text, provinciaComboBoxSucursal.SelectedItem.ToString(), this.cantonComboBoxSucursal.SelectedItem.ToString(), this.distritoComboBoxSucursal.SelectedItem.ToString(), direccionExactarichTextBoxSucursal.Text))
                        {
                            this.mensajeLabelSucursal.ForeColor = Color.Black;
                            this.mensajeLabelSucursal.Text = "Inserción exitosa!";
                        }
                        else
                        {
                            this.mensajeLabelSucursal.Text = "El nombre de la sucursal ya \nse encuentra en uso!";
                            this.mensajeLabelSucursal.ForeColor = Color.Red;
                        }
                    }
                    else if (this.modificarRadioButtonSucursal.Checked)
                    {
                        if (this.modificarSucursal(this.sucursalTextBoxSucursal.Text, provinciaComboBoxSucursal.SelectedItem.ToString(), cantonComboBoxSucursal.SelectedItem.ToString(), distritoComboBoxSucursal.SelectedItem.ToString(), direccionExactarichTextBoxSucursal.Text))
                        {
                            this.mensajeLabelSucursal.ForeColor = Color.Black;
                            this.mensajeLabelSucursal.Text = "Modificación exitosa!";
                            this.previewInformacionSucursales(this.sucursalTextBoxSucursal.Text);
                        }
                        else
                        {
                            this.mensajeLabelSucursal.Text = "Error al intentar modificar!";
                            this.mensajeLabelSucursal.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Está a punto de eliminar una sucursal, ¿desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.eliminarSucursal(sucursalComboBoxSucursal.SelectedItem.ToString());
                            sucursalComboBoxSucursal.Text = "";
                            this.previewInformacionSucursales("");
                            this.cargarSucursales();
                        }
                    }
                }
                else
                {
                    this.mensajeLabelSucursal.Text = "Por revise los datos ingresados!";
                    this.mensajeLabelSucursal.ForeColor = Color.Red;
                }

            }
            catch (Exception)
            {
                this.mensajeLabelSucursal.Text = "Por revise los datos ingresados!";
                this.mensajeLabelSucursal.ForeColor = Color.Red;
            }

        }


        private void telefonoMaskedTextBoxSucursal_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            this.mensajeLabelSucursal.Text = "";
        }


        private void provinciaComboBoxSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cantonComboBoxSucursal.Text = "";
            this.cargarCantones(this.provinciaComboBoxSucursal.SelectedItem.ToString(), this.cantonComboBoxSucursal);
            if (this.cantonComboBoxSucursal.Items.Count > 0)
            {
                this.cantonComboBoxSucursal.SelectedIndex = 0;
            }

        }


        private void cantonComboBoxSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.distritoComboBoxSucursal.Text = "";
            this.cargarDistritos(this.provinciaComboBoxSucursal.SelectedItem.ToString(), this.cantonComboBoxSucursal.SelectedItem.ToString(), this.distritoComboBoxSucursal);
            if (this.distritoComboBoxSucursal.Items.Count > 0)
            {
                this.distritoComboBoxSucursal.SelectedIndex = 0;
            }

        }


        private void agregarEmailButtonSucursal_Click(object sender, EventArgs e)
        {
            if (verificarEmail(emailTextBoxSucursal.Text))
            {
                if (this.emailListBoxSucursal.Items.Contains(emailTextBoxSucursal.Text))
                {
                    this.mensajeLabelSucursal.Text = "El correo ya está registrado!";
                    this.mensajeLabelSucursal.ForeColor = Color.Red;
                }
                else
                {
                    this.modificarEmailTelefonoSucursal(string.Format("  exec insEmailSucursales '{0}', '{1}'  ", sucursalTextBoxSucursal.Text, emailTextBoxSucursal.Text));
                    emailListBoxSucursal.Items.Add(emailTextBoxSucursal.Text);
                    this.emailListBoxSucursal.SelectedIndex = this.emailListBoxSucursal.Items.Count - 1;
                    emailTextBoxSucursal.Text = "";
                    this.mensajeLabelSucursal.Text = "";
                }

            }
            else
            {
                this.mensajeLabelSucursal.Text = "Formato inválido!";
                this.mensajeLabelSucursal.ForeColor = Color.Red;
            }
        }


        private void agregarTelefonoButtonSucursal_Click(object sender, EventArgs e)
        {
            if (telefonoMaskedTextBoxSucursal.Text.Contains(" ") || telefonoMaskedTextBoxSucursal.Text.Length != 9)
            {
                this.mensajeLabelSucursal.Text = "Formato inválido!";
                this.mensajeLabelSucursal.ForeColor = Color.Red;
                return;
            }
            else
            {

                if (this.telefonoListBoxSucursal.Items.Contains(telefonoMaskedTextBoxSucursal.Text))
                {
                    this.mensajeLabelSucursal.Text = "El teléfono ya está registrado!";
                    this.mensajeLabelSucursal.ForeColor = Color.Red;
                }
                else
                {
                    this.modificarEmailTelefonoSucursal(string.Format("  exec insTelefonosSucursales '{0}', '{1}'  ", sucursalTextBoxSucursal.Text, telefonoMaskedTextBoxSucursal.Text));
                    telefonoListBoxSucursal.Items.Add(telefonoMaskedTextBoxSucursal.Text);
                    this.telefonoListBoxSucursal.SelectedIndex = this.telefonoListBoxSucursal.Items.Count - 1;
                    telefonoMaskedTextBoxSucursal.Text = "";
                    this.mensajeLabelSucursal.Text = "";
                }
            }
        }


        private void removerEmailButtonSucursal_Click(object sender, EventArgs e)
        {
            if (this.emailListBoxSucursal.Items.Count > 0)
            {
                this.modificarEmailTelefonoSucursal(string.Format("  exec borrarEmailSucursal '{0}', '{1}'  ", sucursalTextBoxSucursal.Text, emailListBoxSucursal.SelectedItem.ToString()));
                this.emailListBoxSucursal.Items.RemoveAt(emailListBoxSucursal.SelectedIndex);
                this.emailListBoxSucursal.SelectedIndex = this.emailListBoxSucursal.Items.Count - 1;

            }



        }


        private void removerTelefonoButtonSucural_Click(object sender, EventArgs e)
        {
            if (this.telefonoListBoxSucursal.Items.Count > 0)
            {
                this.modificarEmailTelefonoSucursal(string.Format("  exec borrarTelefonoSucursal '{0}', '{1}'  ", sucursalTextBoxSucursal.Text, telefonoListBoxSucursal.SelectedItem.ToString()));
                this.telefonoListBoxSucursal.Items.RemoveAt(telefonoListBoxSucursal.SelectedIndex);
                this.telefonoListBoxSucursal.SelectedIndex = this.telefonoListBoxSucursal.Items.Count - 1;
            }
        }


        private void cargarEmpleadosFuncionesSuscursales()
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = "Select cedula  from empleados";
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                this.sucursalComboBoxSucursal.Items.Clear();
                while (lector.Read())
                {
                    this.empleadoComboBoxFunciones.Items.Add(lector[0]);
                }
                conexion.Close();

                limpiarInformacionFunciones();
                //empleadoComboBoxFunciones.SelectedIndex = 0;

            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void cargarEmpleadosHorarios()
        {

            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;

            try
            {
                limpiarInformacionHorarios();
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = "Select cedula  from empleados";
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                this.empleadoComboBoxHorarios.Items.Clear();
                while (lector.Read())
                {
                    this.empleadoComboBoxHorarios.Items.Add(lector[0]);
                }
                lector.Close();
                conexion.Close();

                
                //empleadoComboBoxFunciones.SelectedIndex = 0;

            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void limpiarInformacionFunciones()
        {
            this.informacionPersonalLabelFunciones.Text = "";
            this.nombreFuncionLabelFunciones.Text = "";
            this.descripcionLabelFunciones.Text = "";
            this.funcionesListBoxFunciones.Items.Clear();
            this.descripcionFuncionRichTextBoxFunciones.Text = "";
            this.mensajeLabelFunciones.Text = "";
        }


        private void limpiarInformacionHorarios()
        {
            this.informacionLabelHorarios.Text = "";
            this.diaListBoxHorarios.Items.Clear();
            this.horaEntradaListBoxHorarios.Items.Clear();
            this.horaSalidaListBoxHorarios.Items.Clear();
            this.mensajeLabelHorarios.Text = "";
        }


        private void empleadoComboBoxFunciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarInformacionFunciones();
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {

                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format("Select (p.nombre + p.apellido1 + p.apellido2) as nombre, e.nombreSucursales as sucursal from personas p inner join empleados  e on p.cedula='{0}'", this.empleadoComboBoxFunciones.SelectedItem.ToString());

                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();

                this.informacionPersonalLabelFunciones.Text = "Nombre:\n" + lector["nombre"].ToString();
                this.informacionPersonalLabelFunciones.Text += "\n\nTrabaja en la sucursal:\n" + lector["sucursal"].ToString();
                lector.Close();



                consulta = string.Format("Select f.nombre from funciones f inner join empleados  e on f.cedulaEmpleado='{0}'", this.empleadoComboBoxFunciones.SelectedItem.ToString());

                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    this.funcionesListBoxFunciones.Items.Add(lector[0]);
                }
                lector.Close();

                conexion.Close();
                if (funcionesListBoxFunciones.Items.Count != 0)
                {
                    funcionesListBoxFunciones.SelectedIndex = 0;
                }


            }
            catch (SqlException)
            {
                MessageBox.Show("Error al cargar información de las funciones del empleado!");
            }


        }


        private void agregarbuttonFunciones_Click(object sender, EventArgs e)
        {
            if (this.nombreFuncionTextBoxFunciones.Text.Length < 5 || this.descripcionNuevaFuncionRichTextBoxFunciones.Text.Length < 5)
            {

                this.mensajeLabelFunciones.Text = "Nombre de sucursal o \ndescripción inválidos!";
                this.mensajeLabelFunciones.ForeColor = Color.Red;

                return;
            }
            else
            {
                if (this.funcionesListBoxFunciones.Items.Contains(this.nombreFuncionTextBoxFunciones.Text))
                {
                    this.mensajeLabelFunciones.Text = "El empleado ya tiene esta funcion!";
                    this.mensajeLabelFunciones.ForeColor = Color.Red;
                }
                else
                {
                    if (this.empleadoComboBoxFunciones.Items.Count > 0)
                    {
                        if (agregarFuncion(this.empleadoComboBoxFunciones.SelectedItem.ToString(), this.nombreFuncionTextBoxFunciones.Text.ToString(), descripcionNuevaFuncionRichTextBoxFunciones.Text))
                        {
                            this.funcionesListBoxFunciones.Items.Add(this.nombreFuncionTextBoxFunciones.Text.ToString());
                            this.funcionesListBoxFunciones.SelectedIndex = this.funcionesListBoxFunciones.Items.Count - 1;
                            this.mensajeLabelFunciones.Text = "Funcion agregada!";
                            this.mensajeLabelFunciones.ForeColor = Color.Black;
                        }
                        else
                        {
                            this.mensajeLabelFunciones.Text = "Error al intentar agregar \nla función!";
                            this.mensajeLabelFunciones.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        this.mensajeLabelFunciones.Text = "Debe haber al menos un \nempleado registrado!";
                        this.mensajeLabelFunciones.ForeColor = Color.Red;
                    }
                }
            }
        }


        private void funcionesListBoxFunciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (funcionesListBoxFunciones.SelectedIndex == -1)
            {
                if (funcionesListBoxFunciones.Items.Count > 0)
                {
                    funcionesListBoxFunciones.SelectedIndex = funcionesListBoxFunciones.Items.Count - 1;
                }
                else
                {
                    descripcionFuncionRichTextBoxFunciones.Text = "";
                    return;
                }
            }
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {

                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format("Select descripcion as descripcion from funciones f inner join empleados  e on e.cedula='{0}' and f.nombre='{1}' ", this.empleadoComboBoxFunciones.SelectedItem.ToString(), this.funcionesListBoxFunciones.SelectedItem.ToString());

                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();

                this.descripcionFuncionRichTextBoxFunciones.Text = lector["descripcion"].ToString();
                lector.Close();
                conexion.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Error al cargar la descripción de la función seleccionada!");
            }
        }


        private void removerButtonFunciones_Click(object sender, EventArgs e)
        {
            if (funcionesListBoxFunciones.Items.Count > 0)
            {
                if (eliminarFuncion(this.empleadoComboBoxFunciones.SelectedItem.ToString(), this.funcionesListBoxFunciones.SelectedItem.ToString(), descripcionFuncionRichTextBoxFunciones.Text))
                {

                    this.funcionesListBoxFunciones.Items.RemoveAt(this.funcionesListBoxFunciones.SelectedIndex);
                    this.funcionesListBoxFunciones.SelectedIndex = this.funcionesListBoxFunciones.Items.Count - 1;
                    this.mensajeLabelFunciones.Text = "Funcion eliminada!";
                    this.mensajeLabelFunciones.ForeColor = Color.Black;
                }
                else
                {
                    this.mensajeLabelFunciones.Text = "Error al intentar eliminar \nla función!";
                    this.mensajeLabelFunciones.ForeColor = Color.Red;
                }
            }
        }


        private void empleadoComboBoxHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {

                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                consulta = string.Format("Select (p.nombre + p.apellido1 + p.apellido2) as nombre, e.nombreSucursales as sucursal from personas p inner join empleados  e on p.cedula='{0}'", this.empleadoComboBoxHorarios.SelectedItem.ToString());

                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                lector.Read();

                this.informacionLabelHorarios.Text = "Nombre:\n" + lector["nombre"].ToString();
                this.informacionLabelHorarios.Text += "\n\nTrabaja en la sucursal:\n" + lector["sucursal"].ToString();
                lector.Close();



                consulta = string.Format("Select h.dia as dia, h.Hentrada as hE, h.HSalida as hS from horarios h inner join empleados  e on h.cedula='{0}'", this.empleadoComboBoxHorarios.SelectedItem.ToString());
                limpiarInformacionHorarios();
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                Boolean paso = true;
                string dia = "";
                while (lector.Read())
                {
                    if (paso)
                    {
                        switch (lector["dia"].ToString())
                        {
                            case "L":
                                dia = "Lunes";
                                break;
                            case "K":
                                dia = "Martes";
                                break;
                            case "M":
                                dia = "Miércoles";
                                break;
                            case "J":
                                dia = "Jueves";
                                break;
                            case "V":
                                dia = "Viernes";
                                break;
                            case "S":
                                dia = "Sábado";
                                break;
                            default:
                                dia = "Domingo";
                                break;
                        }
                        this.diaListBoxHorarios.Items.Add(dia);
                        this.horaEntradaListBoxHorarios.Items.Add(lector["hE"]);
                        this.horaSalidaListBoxHorarios.Items.Add(lector["hS"]);
                        paso = false;
                    }
                    else
                    {
                        paso = true;
                    }

                }
                lector.Close();

                conexion.Close();
                if (this.diaListBoxHorarios.Items.Count > 0)
                {
                    this.diaListBoxHorarios.SelectedIndex = 0;
                    this.horaEntradaListBoxHorarios.SelectedIndex = 0;
                    this.horaSalidaListBoxHorarios.SelectedIndex = 0;
                }



            }
            catch (SqlException)
            {
                MessageBox.Show("Error al cargar información de los horarios del empleado!");
            }
        }


        private void agregarHorarioButtonHorarios_Click(object sender, EventArgs e)
        {
            if (this.empleadoComboBoxHorarios.SelectedItem == null || this.diaComboBoxHorarios.SelectedItem == null)
            {

                this.mensajeLabelHorarios.Text = "Todos los campos son requeridos!";
                this.mensajeLabelHorarios.ForeColor = Color.Red;

                return;
            }
            else
            {
                string dia;
                if (this.diaComboBoxHorarios.SelectedItem.ToString() == "Martes")
                {
                    dia = "K";
                }
                else
                {
                    dia = this.diaComboBoxHorarios.SelectedItem.ToString().ElementAt(0).ToString();
                }

                if (agregarHorario(this.empleadoComboBoxHorarios.SelectedItem.ToString(), dia, this.horaEntradaDateTimePickerHorarios.Value.ToString("HH:mm:ss"), this.horaSalidaDateTimePickerHorarios.Value.ToString("HH:mm:ss")))
                {

                    this.diaListBoxHorarios.Items.Add(this.diaComboBoxHorarios.SelectedItem.ToString());
                    this.horaEntradaListBoxHorarios.Items.Add(horaEntradaDateTimePickerHorarios.Value.ToString("HH:mm:ss"));
                    this.horaSalidaListBoxHorarios.Items.Add(horaSalidaDateTimePickerHorarios.Value.ToString("HH:mm:ss"));
                    this.diaListBoxHorarios.SelectedIndex = this.diaListBoxHorarios.Items.Count - 1;
                    this.horaEntradaListBoxHorarios.SelectedIndex = this.horaEntradaListBoxHorarios.Items.Count - 1;
                    this.horaSalidaListBoxHorarios.SelectedIndex = this.horaSalidaListBoxHorarios.Items.Count - 1;
                    this.mensajeLabelHorarios.Text = "Horario agregado!";
                    this.mensajeLabelHorarios.ForeColor = Color.Black;
                }
                else
                {
                    this.mensajeLabelHorarios.Text = "Por favor revise los datos ingresados!";
                    this.mensajeLabelHorarios.ForeColor = Color.Red;
                }
            }
        }


        private void diaListBoxHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.horaEntradaListBoxHorarios.SelectedIndex = this.diaListBoxHorarios.SelectedIndex;
            this.horaSalidaListBoxHorarios.SelectedIndex = this.diaListBoxHorarios.SelectedIndex;
        }

        private void horaEntradaListBoxHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.diaListBoxHorarios.SelectedIndex = this.horaEntradaListBoxHorarios.SelectedIndex;
            this.horaSalidaListBoxHorarios.SelectedIndex = this.diaListBoxHorarios.SelectedIndex;
        }

        private void horaSalidaListBoxHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.diaListBoxHorarios.SelectedIndex = this.horaSalidaListBoxHorarios.SelectedIndex;
            this.horaEntradaListBoxHorarios.SelectedIndex = this.horaSalidaListBoxHorarios.SelectedIndex;
        }


        private void removerHorarioButtonHorarios_Click(object sender, EventArgs e)
        {
            if (this.diaListBoxHorarios.Items.Count > 0)
            {
                string dia;
                if (this.diaListBoxHorarios.SelectedItem.ToString() == "Martes")
                {
                    dia = "K";
                }
                else
                {
                    dia = this.diaListBoxHorarios.SelectedItem.ToString().ElementAt(0).ToString();
                }



                eliminarHorario(this.empleadoComboBoxHorarios.SelectedItem.ToString(), dia, this.horaEntradaListBoxHorarios.SelectedItem.ToString(), this.horaSalidaListBoxHorarios.SelectedItem.ToString());
                int i = this.diaListBoxHorarios.SelectedIndex;

                this.diaListBoxHorarios.Items.RemoveAt(i);

                this.horaEntradaListBoxHorarios.Items.RemoveAt(i);

                this.horaSalidaListBoxHorarios.Items.RemoveAt(i);


                if (this.diaListBoxHorarios.Items.Count > 0)
                {
                    i = this.diaListBoxHorarios.Items.Count - 1;
                    this.diaListBoxHorarios.SelectedIndex = i;

                }


            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////
        //                                   BRANDON                                              // 
        ////////////////////////////////////////////////////////////////////////////////////////////


        public void insertarVenta(string cedula)
        {
            SqlConnection conexion;
            SqlCommand comando;
            //SqlDataReader lector;
            string consulta;
            try
            {

                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                DateTimePicker dateTimePicker1 = new DateTimePicker();
                string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                Console.WriteLine(theDate);
                //lector=comando.ExecuteReader();
                //lector.Read();
                consulta = string.Format("insertarVenta");
                // lector.Close();
                int monto = 0;
                comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@cedulaEmpleado", cedula);
                comando.Parameters.Add("@fecha", theDate);
                comando.Parameters.Add("@monto", monto);
                //string value = "12";//"select Top 1 Codigo from Venta";

                //Console.WriteLine("hgh");    
                // cbVentas.SelectedItem = value;
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Venta creada");
            }
            catch (SqlException e)
            {
                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public void insertarVentaArticulo(int codigoVenta, int codigoArticulo, int cantidad)
        {

            SqlConnection conexion;
            SqlCommand comando;
            //SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("insertarVentaArticulo");
                comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@codigoVenta", codigoVenta);
                comando.Parameters.Add("@codigoArticulo", codigoArticulo);
                comando.Parameters.Add("@cantidad", cantidad);
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Árticulo agregado a la Venta");
            }
            catch (SqlException e)
            {
                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void cargarVentas()
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = "select Codigo from Ventas";
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                cbVentas.Items.Clear();
                cbEliminarVentaArticulo.Items.Clear();
                cbEliminarVenta.Items.Clear();

                while (lector.Read())
                {
                    cbEliminarVenta.Items.Add(lector[0]);
                    cbEliminarVentaArticulo.Items.Add(lector[0]);
                    cbVentas.Items.Add(lector[0]);
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void cargarArticulos()
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = "select Codigo from Articulos";
                //consulta = string.Format("select Articulos.Codigo from Articulos inner join VentasArticulos on CodigoArticulo!=Articulos.Codigo inner join Ventas on Ventas.Codigo!= VentasArticulos.CodigoVenta where Ventas.Codigo='{0}'", codigo);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                cbArticulos.Items.Clear();
                //cbEliminarArticulo.Items.Clear();

                while (lector.Read())
                {
                    cbArticulos.Items.Add(lector[0].ToString());
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cargarArticulosLb()
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            int codigoVar;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();

                if (string.IsNullOrEmpty(cbVentas.Text) == false)
                {
                    if (cbVentas.SelectedItem != null)
                    {
                        string codigoStr = cbVentas.SelectedItem.ToString();
                        codigoVar = Convert.ToInt32(cbVentas.Text);
                        Console.WriteLine(codigoVar);
                        consulta = string.Format("select Descripcion from Articulos inner join VentasArticulos on CodigoArticulo=Articulos.Codigo inner join Ventas on Ventas.Codigo= VentasArticulos.CodigoVenta where Ventas.Codigo='{0}'", codigoVar);
                        comando = new SqlCommand(consulta, conexion);
                        lector = comando.ExecuteReader();
                        // lector.Read();
                        lbArticulos.Items.Clear();
                        while (lector.Read())
                        {
                            lbArticulos.Items.Add(lector[0]);
                        }
                    }

                }
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void cargarArticulosVenta(int codigoVenta)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("select Articulos.Codigo from Articulos inner join VentasArticulos on CodigoArticulo=Articulos.Codigo inner join Ventas on Ventas.Codigo= VentasArticulos.CodigoVenta where Ventas.Codigo='{0}'", codigoVenta);
                //select Descripcion from Articulos inner join VentasArticulos on CodigoArticulo=Articulos.Codigo inner join Ventas on Ventas.Codigo='{0}'", codigoVar)
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                //cbArticulos.Items.Clear();
                cbEliminarArticulo.Items.Clear();

                while (lector.Read())
                {
                    cbEliminarArticulo.Items.Add(lector[0]);
                    //cbArticulos.Items.Add(lector[0]);
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void eliminarVenta(int codigo)
        {
            SqlConnection conexion;
            SqlCommand comando;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("eliminarVenta");
                //consulta = string.Format("eliminarVenta codigo ");
                comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@codigo", codigo);
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Venta Cancelada");
            }
            catch (SqlException e)
            {
                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void eliminarVentaArticulo(int codigoVenta, int codigoArticulo)
        {
            SqlConnection conexion;
            SqlCommand comando;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("eliminarVentasArticulo");
                comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@codigoventa", codigoVenta);
                comando.Parameters.Add("@codigoArticulo", codigoArticulo);
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Artículo Eliminado De la Venta");
            }
            catch (SqlException e)
            {
                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void eliminardistrito(int idDistrito)
        {
            SqlConnection conexion;
            SqlCommand comando;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("eliminarDistrito");
                comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idDistrito", idDistrito);
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Distrito Eliminado");
            }
            catch (SqlException e)
            {
                MessageBox.Show("No se puedo realizar la operación. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void insertarDistrito(int idCanton, string nombre)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("insertarDistrito");
                comando = new SqlCommand(consulta, conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idCanton", idCanton);
                comando.Parameters.Add("@nombre", nombre);
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Distrito Agregado");
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void verInfoVentas(string inicio, string final)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;
            string consulta;
            try
            {
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                consulta = string.Format("select v.Codigo as Codigo, v.cedulaEmpleado as cedulaEmpleado, v.fecha as fecha,a.Precio as Precio, a.Descripcion as Descripcion from Ventas v inner join  VentasArticulos va on v.Codigo= va.CodigoVenta inner join Articulos a on va.CodigoArticulo=a.Codigo where v.fecha>='{0}' and v.fecha<='{1}'", inicio, final);
                comando = new SqlCommand(consulta, conexion);
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    lbCodigoConsulta.Items.Add(lector["Codigo"]);
                    lbCedulaConsulta.Items.Add(lector["cedulaEmpleado"]);
                    lbFechaConsulta.Items.Add(lector["fecha"]);
                    lbMontoConsulta.Items.Add(lector["Precio"]);
                    lbArticulosConsulta.Items.Add(lector["Descripcion"]);
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("No ha sido posible determinar la consulta. " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void crearVenta_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cedula.Text))
            {
                MessageBox.Show("Escriba su cédula");
            }
            else
            {
                DateTimePicker dateTimePicker1 = new DateTimePicker();
                string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                insertarVenta(cedula.Text);
                cargarVentas();
                var cont = cbVentas.Items.Count;
                cbVentas.SelectedIndex = cont - 1;
            }

        }


        private void cbVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cargarVentas();
            cargarArticulosLb();
            int codigo = Convert.ToInt32(cbVentas.Text);
            //cargarArticulos(codigo);

        }


        private void eliminarventa_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cbEliminarVenta.Text))
            {
                MessageBox.Show("Seleccione Una Venta");
            }
            //string codigoStr = cbVentas.SelectedItem.ToString();
            else
            {
                int codigo = Convert.ToInt32(cbEliminarVenta.Text);
                eliminarVenta(codigo);
                cargarVentas();

            }

            //}
        }


        private void eliminarArticulosventa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbEliminarArticulo.Text) || string.IsNullOrEmpty(cbEliminarVentaArticulo.Text))
            {
                MessageBox.Show("No Deben Haber Campos Vacíos");
            }
            else
            {
                int codigoVenta = Convert.ToInt32(cbEliminarVentaArticulo.Text);
                int codigoArticulo = Convert.ToInt32(cbEliminarArticulo.Text);
                eliminarVentaArticulo(codigoVenta, codigoArticulo);
                cargarVentas();
                //cargarArticulos();
                cargarArticulosLb();
            }
        }


        private void cbEliminarVentaArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codigo = Convert.ToInt32(cbEliminarVentaArticulo.Text);
            cargarArticulosVenta(codigo);
        }


        private void cbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCantones(cbProvincia.Text.ToString(),cbCanton);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbCanton.Text) || string.IsNullOrEmpty(cbProvincia.Text) || string.IsNullOrEmpty(distrito.Text))
            {
                MessageBox.Show("No Deben Haber Campos Vacíos");
            }
            else
            {
                string ncanton = cbCanton.Text.ToString();
                SqlConnection conexion;
                SqlDataReader lector;
                SqlCommand comando;
                int idCanton;
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                string sidCanton = string.Format("select cantones.idCanton as idCanton from cantones  where cantones.nombre= '{0}'", ncanton);
                comando = new SqlCommand(sidCanton, conexion);
                lector = comando.ExecuteReader();
                lector.Read();
                idCanton = Convert.ToInt32(lector["idCanton"].ToString());
                insertarDistrito(idCanton, distrito.Text);
                cargarDistritos(cbProvincia.Text,ncanton,cbDistritos);
                conexion.Close();
            }

        }


        private void button3_Click(object sender, EventArgs e)
        {
            lbArticulosConsulta.Items.Clear();
            lbCedulaConsulta.Items.Clear();
            lbCodigoConsulta.Items.Clear();
            lbFechaConsulta.Items.Clear();
            lbMontoConsulta.Items.Clear();
            verInfoVentas(mbInicio.Text, mbFin.Text);
        }


        private void eliminarDistrito_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbCanton.Text) || string.IsNullOrEmpty(cbProvincia.Text) || string.IsNullOrEmpty(cbDistritos.Text))
            {
                MessageBox.Show("No Deben Haber Campos Vacíos");
            }
            else
            {
                string ndistrito = cbDistritos.Text.ToString();
                string ncanton = cbCanton.Text.ToString();
                SqlConnection conexion;
                SqlDataReader lector;
                SqlCommand comando;
                int idDistrito;
                conexion = new SqlConnection("Data Source=AJDURANCR\\;Initial Catalog=Sunflowers;Integrated Security=True");
                conexion.Open();
                string sDistrito = string.Format("select distritos.idDistrito as idDistrito from distritos where distritos.nombre='{0}'", ndistrito);
                comando = new SqlCommand(sDistrito, conexion);
                lector = comando.ExecuteReader();
                lector.Read();
                idDistrito = Convert.ToInt32(lector["idDistrito"].ToString());
                eliminardistrito(idDistrito);
                cargarDistritos(cbProvincia.Text,ncanton,cbDistritos);
            }

        }


        private void ventaArticulo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbVentas.Text) || string.IsNullOrEmpty(cbArticulos.Text) || string.IsNullOrEmpty(tbCantidad.Text))
            {
                MessageBox.Show("Rellene todos los espacios");
            }
            else
            {
                int codigoVenta = Convert.ToInt32(cbVentas.Text);
                int codigoArticulo = Convert.ToInt32(cbArticulos.Text);
                int cantidad = Convert.ToInt32(tbCantidad.Text);
                insertarVentaArticulo(codigoVenta, codigoArticulo, cantidad);
                //cargarArticulos();
                cargarVentas();
                tbCantidad.ResetText();
            }
        }
    }
}
