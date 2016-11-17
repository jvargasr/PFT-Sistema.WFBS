using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace WFBS.Business.Core
{
    public class ColeccionPeriodoEvaluacion : List<PeriodoEvaluacion>
    {
        public ColeccionPeriodoEvaluacion()
        {
        }

        public ColeccionPeriodoEvaluacion(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<PeriodoEvaluacion>));
            StringReader read = new StringReader(xml);

            List<PeriodoEvaluacion> lista = (List<PeriodoEvaluacion>)serializador.Deserialize(read);
            this.AddRange(lista);
        }


        public List<PeriodoEvaluacion> ReadAllPeriodos()
        {
            List<DAL.PERIODO_EVALUACION> periodosBDD = CommonBC.ModeloWFBS.PERIODO_EVALUACION.ToList();
            return GenerarListadoPeriodos(periodosBDD);
        }

        private List<Core.PeriodoEvaluacion> GenerarListadoPeriodos(List<DAL.PERIODO_EVALUACION> periodosBDD)
        {
            List<Core.PeriodoEvaluacion> periodosController = new List<PeriodoEvaluacion>();

            foreach (DAL.PERIODO_EVALUACION item in periodosBDD)
            {
                Core.PeriodoEvaluacion ar = new PeriodoEvaluacion();

                ar.idPeriodo = Convert.ToInt32(item.ID_PERIODO_EVALUACION);
                ar.fechaInicio = item.FECHA_INICIO;
                ar.vigencia = Convert.ToInt32(item.VIGENCIA);
                ar.porcentajeE = Convert.ToInt32(item.PORCENTAJE_EVALUACION);
                ar.porcentajeAE = Convert.ToInt32(item.PORCENTAJE_AUTOEVALUACION);
                ar.Read();
                periodosController.Add(ar);
            }

            return periodosController;
        }


        public string Serializar(List<PeriodoEvaluacion> periodoE)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<PeriodoEvaluacion>));
            StringWriter writer = new StringWriter();
            serializador.Serialize(writer, periodoE);
            writer.Close();
            return writer.ToString();
        }
    }
}
