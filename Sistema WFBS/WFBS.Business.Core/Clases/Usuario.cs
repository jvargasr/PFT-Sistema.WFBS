using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.DAL;
using WFBS.Business.Log;
using System.Xml.Serialization;
using System.IO;

namespace WFBS.Business.Core
{
    public class Usuario : ICrud
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public int Id_Area { get; set; }
        public int Id_Perfil { get; set; }
        public string Jefe { get; set; }
        public string Password { get; set; }
        public int Obsoleto { get; set; }
        public string Area { get; set; }
        public string Perfil { get; set; }
        public string Obs { get; set; }

        public Usuario()
        {
            this.Init();
        }

        private void Init()
        {
            this.Rut = string.Empty;
            this.Nombre = string.Empty;
            this.Sexo = string.Empty;
            this.Id_Area = 0;
            this.Id_Perfil = 0;
            this.Jefe = string.Empty;
            this.Password = string.Empty;
            this.Obsoleto = 0;
        }

        public Usuario(string xml)
        {
            try
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Usuario));
                StringReader read = new StringReader(xml);

                Usuario us = (Usuario)serializador.Deserialize(read);

                this.Rut = us.Rut;
                this.Nombre = us.Nombre;
                this.Sexo = us.Sexo;
                this.Id_Area = us.Id_Area;
                this.Id_Perfil = us.Id_Perfil;
                this.Jefe = us.Jefe;
                this.Password = us.Password;
                this.Obsoleto = us.Obsoleto;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Deserializar el Usuario: " + ex.ToString());
            }
        }

        public bool ValidarUsuario()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this.Rut && b.PASSWORD == this.Password && b.ID_PERFIL == 1);

                this.Rut = us.RUT;
                this.Nombre = us.NOMBRE;
                this.Sexo = us.SEXO;
                this.Id_Area = Convert.ToInt32(us.ID_AREA);
                this.Id_Perfil = Convert.ToInt32(us.ID_PERFIL);
                this.Jefe = us.JEFE_RESPECTIVO;
                this.Password = us.JEFE_RESPECTIVO;
                this.Obsoleto = Convert.ToInt32(us.OBSOLETO);

                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Validar el Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool Create()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = new USUARIO();

                us.RUT = this.Rut;
                us.NOMBRE = this.Nombre;
                us.ID_AREA = this.Id_Area;
                us.ID_PERFIL = this.Id_Perfil;
                us.SEXO = this.Sexo;
                us.JEFE_RESPECTIVO = this.Jefe;
                us.PASSWORD = this.Password;
                us.OBSOLETO = this.Obsoleto;

                user.USUARIO.Add(us);
                user.SaveChanges();
                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar el Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this.Rut);

                this.Rut = us.RUT;
                this.Nombre = us.NOMBRE;
                this.Sexo = us.SEXO;
                this.Id_Area = Convert.ToInt32(us.ID_AREA);
                this.Id_Perfil = Convert.ToInt32(us.ID_PERFIL);
                this.Jefe = us.JEFE_RESPECTIVO;
                this.Password = us.PASSWORD;
                this.Obsoleto = Convert.ToInt32(us.OBSOLETO);

                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer el Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this.Rut);

                us.NOMBRE = this.Nombre;
                us.ID_AREA = this.Id_Area;
                us.ID_PERFIL = this.Id_Perfil;
                us.SEXO = this.Sexo;
                us.JEFE_RESPECTIVO = this.Jefe;
                us.PASSWORD = this.Password;
                us.OBSOLETO = this.Obsoleto;

                user.SaveChanges();
                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar el Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this.Rut);

                us.OBSOLETO = 1;

                user.SaveChanges();
                user = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Desactivar el Usuario: " + ex.ToString());
                return false;
            }
        }
        public bool Desactivado()
        {
            try
            {
                DAL.WFBSEntities user = new DAL.WFBSEntities();
                DAL.USUARIO us = user.USUARIO.First(b => b.RUT == this.Rut);

                if (us.OBSOLETO == 0)
                {
                    user = null;
                    return true;
                }
                user = null;
                return false;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Validar la vigencia del Usuario: " + ex.ToString());
                return false;
            }
        }

        public string Serializar()
        {
            try
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Usuario));
                StringWriter writer = new StringWriter();
                serializador.Serialize(writer, this);
                writer.Close();
                return writer.ToString();
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Serializar el Usuario: " + ex.ToString());
                return null;
            }
        }
        
    }
}
