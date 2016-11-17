using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace WFBS.Business.Core
{
    public class ColeccionHabilidad : List<Habilidad>
    {
        public ColeccionHabilidad()
        {
        }

        public ColeccionHabilidad(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<Habilidad>));
            StringReader read = new StringReader(xml);

            List<Habilidad> lista = (List<Habilidad>)serializador.Deserialize(read);
            this.AddRange(lista);
        }


        public List<Habilidad> ReadAllHabilidades()
        {
            List<DAL.HABILIDAD> habilidadesBDD = CommonBC.ModeloWFBS.HABILIDAD.ToList();
            return GenerarListado(habilidadesBDD);
        }

        private List<Core.Habilidad> GenerarListado(List<DAL.HABILIDAD> habilidadesBDD)
        {
            List<Core.Habilidad> habilidadesController = new List<Habilidad>();

            foreach (DAL.HABILIDAD item in habilidadesBDD)
            {
                Core.Habilidad hab = new Habilidad();

                hab.Id_Habilidad = Convert.ToInt32(item.ID_HABILIDAD);
                hab.Id_Competencia = Convert.ToInt32(item.ID_COMPETENCIA);
                hab.Nombre = item.NOMBRE;
                hab.Orden_Asignado = Convert.ToInt32(item.ORDEN_ASIGNADO);
                hab.Alternativa_Pregunta = item.ALTERNATIVA_PREGUNTA;

                habilidadesController.Add(hab);
            }

            return habilidadesController;

        }

        public List<Habilidad> ObtenerHabPorCom(int id)
        {
            decimal id_com = Convert.ToDecimal(id);
            //var habilidad = CommonBC.ModeloWfbs.HABILIDAD.Where(h => h.ID_COMPETENCIA == id);
            var HabiBDD = from h in CommonBC.ModeloWFBS.HABILIDAD
                          join c in CommonBC.ModeloWFBS.COMPETENCIA on h.ID_COMPETENCIA equals c.ID_COMPETENCIA
                          where h.ID_COMPETENCIA == id_com
                          select new Habilidad
                          {
                              Id_Hab = h.ID_HABILIDAD,
                              Competencia = c.NOMBRE,
                              Nombre = h.NOMBRE,
                              Orden = h.ORDEN_ASIGNADO,
                              Alternativa_Pregunta = h.ALTERNATIVA_PREGUNTA
                          };
            return HabiBDD.ToList();
            //return (GenerarListado(habilidad.ToList()));
        }


        public string Serializar(List<Habilidad> habilidad)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<Habilidad>));
            StringWriter writer = new StringWriter();
            serializador.Serialize(writer, habilidad);
            writer.Close();
            return writer.ToString();
        }
    }
}
