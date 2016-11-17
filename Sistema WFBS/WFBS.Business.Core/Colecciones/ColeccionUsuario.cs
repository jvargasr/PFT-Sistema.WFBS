using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace WFBS.Business.Core
{
    public class ColeccionUsuario : List<Usuario>
    {
        public ColeccionUsuario()
        {
        }

        public ColeccionUsuario(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<Usuario>));
            StringReader read = new StringReader(xml);

            List<Usuario> lista = (List<Usuario>)serializador.Deserialize(read);
            this.AddRange(lista);
        }


        public List<Usuario> ReadAllUsuarios()
        {
            var usuariosBDD = from u in CommonBC.ModeloWFBS.USUARIO
                              join a in CommonBC.ModeloWFBS.AREA on u.ID_AREA equals a.ID_AREA
                              join p in CommonBC.ModeloWFBS.PERFIL on u.ID_PERFIL equals p.ID_PERFIL

                              select new Usuario
                              {
                                  Rut = u.RUT,
                                  Nombre = u.NOMBRE,
                                  Sexo = (u.SEXO == "M" ? "Masculino" : u.SEXO == "F" ? "Femenino" : "No determinado"),
                                  Jefe = u.JEFE_RESPECTIVO,
                                  Perfil = p.TIPO_USUARIO,
                                  Area = (p.TIPO_USUARIO == "administrador" ? "" : (p.TIPO_USUARIO == "jefe" || p.TIPO_USUARIO == "funcionario") ? a.NOMBRE : "No determinado"),
                                  Password = u.PASSWORD,
                                  Obs = (u.OBSOLETO == 0 ? "No" : u.OBSOLETO == 1 ? "Si" : "No determinado"),
                              };
            return usuariosBDD.ToList();
        }


        private List<Core.Usuario> GenerarListado(List<DAL.USUARIO> usuariosBDD)
        {
            List<Core.Usuario> usuariosController = new List<Usuario>();

            foreach (DAL.USUARIO item in usuariosBDD)
            {
                Core.Usuario us = new Usuario();

                us.Rut = item.RUT;
                us.Nombre = item.NOMBRE;
                us.Sexo = item.SEXO;
                us.Id_Area = Convert.ToInt32(item.ID_AREA);
                us.Id_Perfil = Convert.ToInt32(item.ID_PERFIL);
                us.Jefe = item.JEFE_RESPECTIVO;
                us.Obsoleto = Convert.ToInt32(item.OBSOLETO);

                usuariosController.Add(us);
            }

            return usuariosController;

        }


        public string Serializar(List<Usuario> usuario)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<Usuario>));
            StringWriter writer = new StringWriter();
            serializador.Serialize(writer, usuario);
            writer.Close();
            return writer.ToString();
        }
    }
}
