using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WFBS.Business.Core.Clases
{
    public class Auditoria : ICrud
    {
        public decimal id_auditoria { get; set; }
        public string ip_conexion { get; set; }
        public string rut { get; set; }
        public DateTime fecha { get; set; }

        public Auditoria()
        {
            this.Init();

        }

        private void Init()
        {
            this.id_auditoria = 0;
            this.ip_conexion = string.Empty;
            this.rut = string.Empty;
            this.fecha = DateTime.Now;
        }

        public Auditoria(string xml)
        {
            try
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Auditoria));
                StringReader read = new StringReader(xml);

                Auditoria au = (Auditoria)serializador.Deserialize(read);

                this.id_auditoria = au.id_auditoria;
                this.ip_conexion = au.ip_conexion;
                this.rut = au.rut;
                this.fecha = au.fecha;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Deserializar la Área: " + ex.ToString());
            }
        }

        public bool Create()
        {
            try
            {
                DAL.WFBSEntities auditoria = new DAL.WFBSEntities();
                DAL.AUDITORIA au = new DAL.AUDITORIA();

                au.ID_AUDITORIA = this.id_auditoria;
                au.IP_CONEXION = this.ip_conexion;
                au.RUT = this.rut;
                au.FECHA_INGRESO = this.fecha;

                auditoria.AUDITORIA.Add(au);
                auditoria.SaveChanges();
                auditoria = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar la Auditoría:" + ex.ToString());
                return false;
            }
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public string Serializar()
        {
            try
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Auditoria));
                StringWriter writer = new StringWriter();
                serializador.Serialize(writer, this);
                writer.Close();
                return writer.ToString();
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Serializar la Auditoría: " + ex.ToString());
                return null;
            }
        }
    }
}
