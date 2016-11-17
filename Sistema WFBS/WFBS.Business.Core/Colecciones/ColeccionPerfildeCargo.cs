using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace WFBS.Business.Core
{
    public class ColeccionPerfildeCargo : List<PerfilesdeCargo>
    {
        public ColeccionPerfildeCargo()
        {
        }

        public ColeccionPerfildeCargo(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<PerfilesdeCargo>));
            StringReader read = new StringReader(xml);

            List<PerfilesdeCargo> lista = (List<PerfilesdeCargo>)serializador.Deserialize(read);
            this.AddRange(lista);
        }

        /* Perfiles de cargo */
        public List<PerfilesdeCargo> ReadAllPerfilesdeCargo()
        {
            var PCargoBDD = from pc in CommonBC.ModeloWFBS.PERFIL_DE_CARGO

                            select new PerfilesdeCargo
                            {
                                Id_PC = pc.ID_PERFIL_DE_CARGO,
                                descripcion = pc.DESCRIPCION,
                                Obs = (pc.OBSOLETO == 0 ? "No" : pc.OBSOLETO == 1 ? "Si" : "No determinado"),
                                id_areas = pc.ID_AREAS,
                            };
            return PCargoBDD.ToList();
        }

        private List<Core.PerfilesdeCargo> GenerarListado(List<DAL.PERFIL_DE_CARGO> perfilesdecargoBDD)
        {
            List<Core.PerfilesdeCargo> perfilesdecargoController = new List<PerfilesdeCargo>();

            foreach (DAL.PERFIL_DE_CARGO item in perfilesdecargoBDD)
            {
                Core.PerfilesdeCargo pc = new PerfilesdeCargo();

                pc.Id_PC = Convert.ToInt32(item.ID_PERFIL_DE_CARGO);
                pc.descripcion = item.DESCRIPCION;
                pc.Obsoleto = Convert.ToInt32(item.OBSOLETO);

                perfilesdecargoController.Add(pc);
            }

            return perfilesdecargoController;
        }

        public string Serializar(List<PerfilesdeCargo> perfildeCargo)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<PerfilesdeCargo>));
            StringWriter writer = new StringWriter();
            serializador.Serialize(writer, perfildeCargo);
            writer.Close();
            return writer.ToString();
        }
    }
}
