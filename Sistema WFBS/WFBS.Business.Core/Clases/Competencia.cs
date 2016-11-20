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
    public class Competencia : ICrud
    {
        public int Id_competencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Sigla { get; set; }
        public int Obsoleta { get; set; }
        public int Nivel_Optimo { get; set; }
        public string Pregunta_Asociada { get; set; }
        public decimal Id_com { get; set; }
        public string Obs { get; set; }
        public decimal Nivel_O { get; set; }

        public Competencia()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_competencia = 0;
            this.Nombre = string.Empty;
            this.Descripcion = string.Empty;
            this.Sigla = string.Empty;
            this.Obsoleta = 0;
            this.Nivel_Optimo = 0;
            this.Pregunta_Asociada = string.Empty;
        }

        public Competencia(string xml)
        {
            try
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Competencia));
                StringReader read = new StringReader(xml);

                Competencia com = (Competencia)serializador.Deserialize(read);

                this.Id_competencia = com.Id_competencia;
                this.Nombre = com.Nombre;
                this.Descripcion = com.Descripcion;
                this.Sigla = com.Sigla;
                this.Obsoleta = com.Obsoleta;
                this.Nivel_Optimo = com.Nivel_Optimo;
                this.Pregunta_Asociada = com.Pregunta_Asociada;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Deserializar la Competencia: " + ex.ToString());
            }
        }

        //Crea una nueva instancia de WFBSentities y almacena en esta la nueva instancia de modelo competencia
        public bool Create()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = new COMPETENCIA();

                com.ID_COMPETENCIA = this.Id_competencia;
                com.NOMBRE = this.Nombre;
                com.DESCRIPCION = this.Descripcion;
                com.SIGLA = this.Sigla;
                com.OBSOLETA = this.Obsoleta;
                com.NIVEL_OPTIMO_ESPERADO = this.Nivel_Optimo;
                com.PREGUNTA_ASOCIADA = this.Pregunta_Asociada;

                compe.COMPETENCIA.Add(com);
                compe.SaveChanges();
                compe = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar la Competencia: " + ex.ToString());
                return false;
            }
        }

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = compe.COMPETENCIA.First(c => c.ID_COMPETENCIA == this.Id_competencia);

                this.Id_competencia = Convert.ToInt32(com.ID_COMPETENCIA);
                this.Nombre = com.NOMBRE;
                this.Descripcion = com.DESCRIPCION;
                this.Sigla = com.SIGLA;
                this.Obsoleta = Convert.ToInt32(com.OBSOLETA);
                this.Nivel_Optimo = Convert.ToInt32(com.NIVEL_OPTIMO_ESPERADO);
                this.Pregunta_Asociada = com.PREGUNTA_ASOCIADA;
                if (this.Obsoleta == 0)
                {
                    this.Obs = "No";
                }
                else
                {
                    this.Obs = "Si";
                }

                compe = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer la Competencia: " + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = compe.COMPETENCIA.First(c => c.ID_COMPETENCIA == this.Id_competencia);

                com.ID_COMPETENCIA = this.Id_competencia;
                com.NOMBRE = this.Nombre;
                com.DESCRIPCION = this.Descripcion;
                com.SIGLA = this.Sigla;
                com.OBSOLETA = this.Obsoleta;
                com.NIVEL_OPTIMO_ESPERADO = this.Nivel_Optimo;
                com.PREGUNTA_ASOCIADA = this.Pregunta_Asociada;

                compe.SaveChanges();
                compe = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar la Competencia: " + ex.ToString());
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities compe = new DAL.WFBSEntities();
                DAL.COMPETENCIA com = compe.COMPETENCIA.First(c => c.ID_COMPETENCIA == this.Id_competencia);

                com.OBSOLETA = 1;
                compe.SaveChanges();
                compe = null;

                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Desactivar la Competencia: " + ex.ToString());
                return false;
            }
        }

        public string Serializar()
        {
            try
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Competencia));
                StringWriter writer = new StringWriter();
                serializador.Serialize(writer, this);
                writer.Close();
                return writer.ToString();
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Serializar la Competencia: " + ex.ToString());
                return null;
            }
        }
    }
}
