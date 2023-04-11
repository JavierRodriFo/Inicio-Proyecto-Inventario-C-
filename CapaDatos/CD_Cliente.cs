using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Cliente
    {
        public List<Cliente> Listar() { 
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection conn = new SqlConnection(Conexion.cadena)) {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine ("select IdCliente,Nombres,Apellidos,Direccion from Cliente");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                IdCliente=Convert.ToInt32(dr["IdCliente"]),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos= dr["Apellidos"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                            });
                        }
                    }
                }
                catch(Exception ex) {
                    lista = new List<Cliente>();
                }

            }
            return lista;

        }
        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idclienteResultado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCLIENTE", oconexion);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.Add("idclienteResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;


                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    idclienteResultado = Convert.ToInt32(cmd.Parameters["idclienteResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();




                }


            }
            catch (Exception ex)
            {
                idclienteResultado = 0;
                Mensaje = ex.Message;


            }

            return idclienteResultado;


        }

        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITCLIENTE", oconexion);
                    cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;



                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();




                }


            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;


            }

            return respuesta;


        }

        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("delete from cliente where IdCliente = @id", oconexion);
                    cmd.Parameters.AddWithValue("Id", obj.IdCliente);

                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();


                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;


            }

            return respuesta;


        }
    }
}
