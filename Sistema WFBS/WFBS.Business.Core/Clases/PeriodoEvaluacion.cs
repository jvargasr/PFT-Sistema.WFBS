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
    public class PeriodoEvaluacion : ICrud
    {
        public int idPeriodo { get; set; }
        public DateTime fechaInicio { get; set; }
        public int vigencia { get; set; }
        public int porcentajeE { get; set; }
        public int porcentajeAE { get; set; }
        public decimal Id_Periodo { get; set; }

        public PeriodoEvaluacion()
        {
            this.Init();
        }

        private void Init()
        {
            this.idPeriodo = 0;
            this.fechaInicio = DateTime.Now;
            this.vigencia = 0;
            this.porcentajeE = 0;
            this.porcentajeAE = 0;
        }

        public PeriodoEvaluacion(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(PeriodoEvaluacion));
            StringReader read = new StringReader(xml);

            PeriodoEvaluacion pe = (PeriodoEvaluacion)serializador.Deserialize(read);

            this.idPeriodo = pe.idPeriodo;
            this.fechaInicio = pe.fechaInicio;
            this.vigencia = pe.vigencia;
            this.porcentajeE = pe.porcentajeE;
            this.porcentajeAE = pe.porcentajeAE;
        }

        public bool Create()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = new PERIODO_EVALUACION();

                pe.ID_PERIODO_EVALUACION = this.idPeriodo;
                pe.FECHA_INICIO = this.fechaInicio;
                pe.VIGENCIA = this.vigencia;
                pe.PORCENTAJE_EVALUACION = this.porcentajeE;
                pe.PORCENTAJE_AUTOEVALUACION = this.porcentajeAE;

                periodo.PERIODO_EVALUACION.Add(pe);
                periodo.SaveChanges();
                periodo = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo agregar el periodo de evaluación: " + ex.ToString());
                return false;
            }
        }

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = periodo.PERIODO_EVALUACION.First(b => b.ID_PERIODO_EVALUACION == this.idPeriodo);

                this.idPeriodo = Convert.ToInt32(pe.ID_PERIODO_EVALUACION);
                this.fechaInicio = pe.FECHA_INICIO;
                this.vigencia = Convert.ToInt32(pe.VIGENCIA);
                this.porcentajeE = Convert.ToInt32(pe.PORCENTAJE_EVALUACION);
                this.porcentajeAE = Convert.ToInt32(pe.PORCENTAJE_AUTOEVALUACION);

                periodo = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo leer el periodo de evaluación: " + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = periodo.PERIODO_EVALUACION.First(b => b.ID_PERIODO_EVALUACION == this.idPeriodo);

                pe.ID_PERIODO_EVALUACION = this.idPeriodo;
                pe.FECHA_INICIO = this.fechaInicio;
                pe.VIGENCIA = this.vigencia;
                pe.PORCENTAJE_EVALUACION = this.porcentajeE;
                pe.PORCENTAJE_AUTOEVALUACION = this.porcentajeAE;

                periodo.SaveChanges();
                periodo = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo actualizar el periodo de evaluación: " + ex.ToString());
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities periodo = new DAL.WFBSEntities();
                DAL.PERIODO_EVALUACION pe = periodo.PERIODO_EVALUACION.First(b => b.ID_PERIODO_EVALUACION == this.idPeriodo);

                pe.VIGENCIA = 0;

                periodo.SaveChanges();
                periodo = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo desactivar el periodo de evaluación: " + ex.ToString());
                return false;
            }
        }

        public string Serializar()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(PeriodoEvaluacion));
            StringWriter writer = new StringWriter();
            serializador.Serialize(writer, this);
            writer.Close();
            return writer.ToString();
        }
    }
}
