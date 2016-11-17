using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace WFBS.Business.Core
{
    public class ColeccionCompetencia : List<Competencia>
    {
        public ColeccionCompetencia()
        {
        }

        public ColeccionCompetencia(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<Competencia>));
            StringReader read = new StringReader(xml);

            List<Competencia> lista = (List<Competencia>)serializador.Deserialize(read);
            this.AddRange(lista);
        }
        public List<Competencia> ReadAllCompetencias()
        {
            List<DAL.COMPETENCIA> CompetenciasBDD = CommonBC.ModeloWFBS.COMPETENCIA.ToList();
            return GenerarListadoCompetencia(CompetenciasBDD);
        }

        private List<Core.Competencia> GenerarListadoCompetencia(List<DAL.COMPETENCIA> CompetenciaBDD)
        {
            List<Core.Competencia> competenciasController = new List<Competencia>();

            foreach (DAL.COMPETENCIA item in CompetenciaBDD)
            {
                Core.Competencia com = new Competencia();

                com.Id_competencia = Convert.ToInt32(item.ID_COMPETENCIA);
                com.Nombre = item.NOMBRE;
                com.Descripcion = item.DESCRIPCION;
                com.Sigla = item.SIGLA;
                com.Obsoleta = Convert.ToInt32(item.OBSOLETA);
                com.Nivel_Optimo = Convert.ToInt32(item.NIVEL_OPTIMO_ESPERADO);
                com.Pregunta_Asociada = item.PREGUNTA_ASOCIADA;
                com.Read();
                competenciasController.Add(com);
            }

            return competenciasController;
        }

        public string Serializar(List<Competencia> competencia)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<Competencia>));
            StringWriter writer = new StringWriter();
            serializador.Serialize(writer, competencia);
            writer.Close();
            return writer.ToString();
        }
    }
}
