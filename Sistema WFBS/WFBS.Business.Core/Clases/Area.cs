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
    public class Area : ICrud
    {
        public int id_area { get; set; }
        public string area { get; set; }
        public string abreviacion { get; set; }
        public int obsoleta { get; set; }
        public decimal Id_area { get; set; }
        public string obs { get; set; }

        public Area()
        {
            this.Init();
        }

        private void Init()
        {
            this.abreviacion = string.Empty;
            this.area = string.Empty;
            this.id_area = 0;
            this.obsoleta = 0;
        }

        public Area(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Area));
            StringReader read = new StringReader(xml);

            Area ar = (Area)serializador.Deserialize(read);

            this.id_area = ar.id_area;
            this.area = ar.area;
            this.abreviacion = ar.abreviacion;
            this.obsoleta = ar.obsoleta;
        }

        public bool Create()
        {
            try
            {
                DAL.WFBSEntities area = new DAL.WFBSEntities();
                DAL.AREA ar = new AREA();

                ar.ID_AREA = this.id_area;
                ar.NOMBRE = this.area;
                ar.ABREVIACION = this.abreviacion;
                ar.OBSOLETA = this.obsoleta;

                area.AREA.Add(ar);
                area.SaveChanges();
                area = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo agregar la área: " + ex.ToString());
                return false;
            }
        }

        public bool Read()
        {
            try
            {
                DAL.WFBSEntities area = new DAL.WFBSEntities();
                DAL.AREA ar = area.AREA.First(b => b.ID_AREA == this.id_area);

                this.id_area = Convert.ToInt32(ar.ID_AREA);
                this.area = ar.NOMBRE;
                this.abreviacion = ar.ABREVIACION;
                this.obsoleta = Convert.ToInt32(ar.OBSOLETA);
                area = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo leer la área: " + ex.ToString());
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                DAL.WFBSEntities area = new DAL.WFBSEntities();
                DAL.AREA ar = area.AREA.First(b => b.ID_AREA == this.id_area);

                ar.NOMBRE = this.area;
                ar.ID_AREA = this.id_area;
                ar.ABREVIACION = this.abreviacion;
                ar.OBSOLETA = this.obsoleta;

                area.SaveChanges();
                area = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo actualizar la área: " + ex.ToString());
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                DAL.WFBSEntities area = new DAL.WFBSEntities();
                DAL.AREA ar = area.AREA.First(c => c.ID_AREA == this.id_area);

                ar.OBSOLETA = 1;

                area.SaveChanges();
                area = null;
                return true;
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo desactivar la área: " + ex.ToString());
                return false;
            }
        }

        public string Serializar()
        {
            try
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Area));
                StringWriter writer = new StringWriter();
                serializador.Serialize(writer, this);
                writer.Close();
                return writer.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }
    }
}
