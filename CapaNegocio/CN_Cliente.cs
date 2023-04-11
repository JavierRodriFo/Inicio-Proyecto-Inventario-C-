using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Cliente
    {
       private CD_Cliente objcd_cliente = new CD_Cliente();

        public List<Cliente> Listar() 
        {
            return objcd_cliente.Listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.Nombres == "")
            {
                Mensaje += "Es necesario el nombre del Cliente\n";

            }
            if (obj.Apellidos == "")
            {
                Mensaje += "Es necesario el apellido completo del Cliente\n";

            }
            if (obj.Direccion == "")
            {
                Mensaje += "Es necesario la direccion del Cliente\n";

            }

            if (Mensaje != string.Empty)
            {
                return 0;

            }
            else
            {
                return objcd_cliente.Registrar(obj, out Mensaje);

            }





        }
        public bool Editar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Nombres == "")
            {
                Mensaje += "Es necesario el nombre del Cliente\n";

            }
            if (obj.Apellidos == "")
            {
                Mensaje += "Es necesario el apellido completo del Cliente\n";

            }
            if (obj.Direccion == "")
            {
                Mensaje += "Es necesario la direccion del Cliente\n";

            }

            if (Mensaje != string.Empty)
            {
                return false;

            }
            else
            {
                return objcd_cliente.Editar(obj, out Mensaje);

            }


        }
        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            return objcd_cliente.Eliminar(obj, out Mensaje);


        }
    }
}
