using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WFBS.Business.Core.Clases
{
    public class Evaluacion : ICrud
    {
        public decimal id_evaluacion { get; set; }
        public decimal id_area { get; set; }
        public decimal id_periodo_evaluacion { get; set; }
        public decimal id_tipo_evaluacion { get; set; }
        public decimal id_competencia { get; set; }
        public string rut_evaluado { get; set; }
        public string rut_evaluador { get; set; }
        public decimal nota_esperada { get; set; }
        public DateTime fecha_contesta { get; set; }
        public decimal nota_encuesta { get; set; }

        public Evaluacion()
        {
            this.Init();
        }

        private void Init()
        {
            this.id_evaluacion = 0;
            this.id_area = 0;
            this.id_competencia = 0;
            this.id_periodo_evaluacion = 0;
            this.id_tipo_evaluacion = 0;
            this.nota_encuesta = 0;
            this.nota_esperada = 0;
            this.rut_evaluado = string.Empty;
            this.rut_evaluador = string.Empty;
            this.fecha_contesta = DateTime.Now;

        }

        public Evaluacion(string xml)
        {
            try
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Evaluacion));
                StringReader read = new StringReader(xml);

                Evaluacion ev = (Evaluacion)serializador.Deserialize(read);

                this.id_evaluacion = ev.id_evaluacion;
                this.id_area = ev.id_area;
                this.id_competencia = ev.id_competencia;
                this.id_periodo_evaluacion = ev.id_periodo_evaluacion;
                this.id_tipo_evaluacion = ev.id_tipo_evaluacion;
                this.nota_encuesta = ev.nota_encuesta;
                this.nota_esperada = ev.nota_esperada;
                this.rut_evaluado = ev.rut_evaluado;
                this.rut_evaluador = ev.rut_evaluador;
                this.fecha_contesta = ev.fecha_contesta;

            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Deserializar la evaluación: " + ex.ToString());
            }
        }

        public bool Create()
        {
            try
            {
                DAL.WFBSEntities evaluacion = new DAL.WFBSEntities();
                DAL.EVALUACION ev = new DAL.EVALUACION();

                ev.ID_EVALUACION = this.id_evaluacion;
                ev.ID_AREA = this.id_area;
                ev.ID_COMPETENCIA = this.id_competencia;
                ev.ID_PERIODO_EVALUACION = this.id_periodo_evaluacion;
                ev.ID_TIPO_EVALUACION= this.id_tipo_evaluacion;
                ev.NOTA_ENCUESTA = this.nota_encuesta;
                ev.NOTA_ESPERADA_COMPETENCIA = this.nota_esperada;
                ev.RUT_EVALUADO = this.rut_evaluado;
                ev.RUT_EVALUADOR = this.rut_evaluador;
                ev.FECHA_CONTESTA_ENCUESTA = this.fecha_contesta;

                evaluacion.EVALUACION.Add(ev);
                evaluacion.SaveChanges();
                evaluacion = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Agregar el Usuario: " + ex.ToString());
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities evaluacion = new DAL.WFBSEntities();
                DAL.EVALUACION ev = evaluacion.EVALUACION.First(b => b.ID_EVALUACION == this.id_evaluacion);

                evaluacion.SaveChanges();
                evaluacion = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Desactivar la Área: " + ex.ToString());
                return false;
            }
        }

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities evaluacion = new DAL.WFBSEntities();
                DAL.EVALUACION ev = evaluacion.EVALUACION.First(b => b.ID_EVALUACION == this.id_evaluacion);

                this.id_evaluacion = ev.ID_EVALUACION;
                this.id_area = Convert.ToInt32(ev.ID_AREA);
                this.id_competencia = Convert.ToInt32(ev.ID_COMPETENCIA);
                this.id_periodo_evaluacion = Convert.ToInt32(ev.ID_PERIODO_EVALUACION);
                this.id_tipo_evaluacion = Convert.ToInt32(ev.ID_TIPO_EVALUACION);
                this.nota_encuesta = ev.NOTA_ENCUESTA;
                this.nota_esperada = Convert.ToInt32(ev.NOTA_ESPERADA_COMPETENCIA);
                this.rut_evaluado = ev.RUT_EVALUADO;
                this.rut_evaluador = ev.RUT_EVALUADOR;
                this.fecha_contesta = ev.FECHA_CONTESTA_ENCUESTA;
                evaluacion = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Leer la Evaluación:" + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities evaluacion = new DAL.WFBSEntities();
                DAL.EVALUACION ev = evaluacion.EVALUACION.First(b => b.ID_EVALUACION == this.id_evaluacion);

                ev.ID_AREA = this.id_area;
                ev.ID_COMPETENCIA = this.id_competencia;
                ev.ID_PERIODO_EVALUACION = this.id_periodo_evaluacion;
                ev.ID_TIPO_EVALUACION = this.id_tipo_evaluacion;
                ev.NOTA_ENCUESTA = this.nota_encuesta;
                ev.NOTA_ESPERADA_COMPETENCIA = this.nota_esperada;
                ev.RUT_EVALUADO = this.rut_evaluado;
                ev.RUT_EVALUADOR = this.rut_evaluador;
                ev.FECHA_CONTESTA_ENCUESTA = this.fecha_contesta;

                evaluacion.SaveChanges();
                evaluacion = null;
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Actualizar la Evaluación: " + ex.ToString());
                return false;
            }
        }

        public string Serializar()
        {
            try
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Evaluacion));
                StringWriter writer = new StringWriter();
                serializador.Serialize(writer, this);
                writer.Close();
                return writer.ToString();
            }
            catch (Exception ex)
            {
                Log.Logger.log("No se pudo Serializar la Evaluación: " + ex.ToString());
                return null;
            }
        }
    }
}
