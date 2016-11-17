using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.DAL;
using System.Xml.Serialization;
using System.IO;

namespace WFBS.Business.Core
{
    public class Habilidad : ICrud
    {
        public int Id_Habilidad { get; set; }
        public int Id_Competencia { get; set; }
        public string Nombre { get; set; }
        public int Orden_Asignado { get; set; }
        public string Alternativa_Pregunta { get; set; }
        public decimal Id_Hab { get; set; }
        public string Competencia { get; set; }
        public decimal Orden { get; set; }

        public Habilidad()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_Competencia = 0;
            this.Id_Competencia = 0;
            this.Nombre = string.Empty;
            this.Orden_Asignado = 0;
            this.Alternativa_Pregunta = string.Empty;
        }

        public Habilidad(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Habilidad));
            StringReader read = new StringReader(xml);

            Habilidad hab = (Habilidad)serializador.Deserialize(read);

            this.Id_Habilidad = hab.Id_Habilidad;
            this.Id_Competencia = hab.Id_Competencia;
            this.Nombre = hab.Nombre;
            this.Orden_Asignado = hab.Orden_Asignado;
            this.Alternativa_Pregunta = hab.Alternativa_Pregunta;
        }

        //Crea una nueva instancia de WFBSentities y almacena en esta la nueva instancia de modelo habilidad
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = new HABILIDAD();
                hab.ID_HABILIDAD = this.Id_Habilidad;
                hab.ID_COMPETENCIA = this.Id_Competencia;
                hab.NOMBRE = this.Nombre;
                hab.ORDEN_ASIGNADO = this.Orden_Asignado;
                hab.ALTERNATIVA_PREGUNTA = this.Alternativa_Pregunta;

                habi.HABILIDAD.Add(hab);
                habi.SaveChanges();
                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo agregar la habilidad: " + ex.ToString());
                return false;
            }

        }

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = habi.HABILIDAD.First(h => h.ID_HABILIDAD == this.Id_Habilidad);

                this.Id_Habilidad = Convert.ToInt32(hab.ID_HABILIDAD);
                this.Id_Competencia = Convert.ToInt32(hab.ID_COMPETENCIA);
                this.Nombre = hab.NOMBRE;
                this.Orden_Asignado = Convert.ToInt32(hab.ORDEN_ASIGNADO);
                this.Alternativa_Pregunta = hab.ALTERNATIVA_PREGUNTA;

                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo agregar la habilidad: " + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = habi.HABILIDAD.First(h => h.ID_HABILIDAD == this.Id_Habilidad);

                hab.ID_HABILIDAD = this.Id_Habilidad;
                hab.ID_COMPETENCIA = this.Id_Competencia;
                hab.NOMBRE = this.Nombre;
                hab.ORDEN_ASIGNADO = this.Orden_Asignado;
                hab.ALTERNATIVA_PREGUNTA = this.Alternativa_Pregunta;

                habi.SaveChanges();
                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo agregar la habilidad: " + ex.ToString());
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities habi = new DAL.WFBSEntities();
                DAL.HABILIDAD hab = habi.HABILIDAD.First(h => h.ID_HABILIDAD == this.Id_Habilidad);

                habi.HABILIDAD.Remove(hab);
                habi.SaveChanges();
                habi = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo agregar la habilidad: " + ex.ToString());
                return false;
            }
        }

        public string Serializar()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Habilidad));
            StringWriter writer = new StringWriter();
            serializador.Serialize(writer, this);
            writer.Close();
            return writer.ToString();
        }
    }
}
