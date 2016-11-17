using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace WFBS.Business.Core
{
    public class ColeccionArea : List<Area>
    {
        public ColeccionArea()
        {
        }

        public ColeccionArea(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<Area>));
            StringReader read = new StringReader(xml);

            List<Area> lista = (List<Area>)serializador.Deserialize(read);
            this.AddRange(lista);
        }

        public List<Area> ReadAllAreas()
        {
            var AreasBDD = from c in CommonBC.ModeloWFBS.AREA

                           select new Area
                           {
                               Id_area = c.ID_AREA,
                               area = c.NOMBRE,
                               abreviacion = c.ABREVIACION,
                               obs = (c.OBSOLETA == 0 ? "No" : c.OBSOLETA == 1 ? "Si" : "No determinado"),
                           };
            return AreasBDD.ToList();
        }

        private List<Core.Area> GenerarListado(List<DAL.AREA> areasBDD)
        {
            List<Core.Area> areasController = new List<Area>();

            foreach (DAL.AREA item in areasBDD)
            {
                Core.Area ar = new Area();

                ar.id_area = Convert.ToInt32(item.ID_AREA);
                ar.obsoleta = Convert.ToInt32(item.OBSOLETA);
                ar.abreviacion = item.ABREVIACION;
                ar.area = item.NOMBRE;
                ar.Read();

                areasController.Add(ar);
            }

            return areasController;
        }



        public string Serializar(List<Area> area)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<Area>));
            StringWriter writer = new StringWriter();
            serializador.Serialize(writer, area);
            writer.Close();
            return writer.ToString();
        }
    }
}
