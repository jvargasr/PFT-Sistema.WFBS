using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WFBS.Business.Core;

namespace WFBS.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceWFBS : IServiceWFBS
    {
        #region Competencia
        // Competencia
        public bool CrearCompetencia(string xml)
        {
            Competencia com = new Competencia(xml);
            return com.Create();
        }

        public bool ActualizarCompetencia(string xml)
        {
            Competencia com = new Competencia(xml);
            return com.Update();
        }

        public bool EliminarCompetencia(string xml)
        {
            Competencia com = new Competencia(xml);
            return com.Delete();
        }

        public string LeerCompetencia(string xml)
        {
            try
            {
                Competencia com = new Competencia(xml);
                if (com.Read())
                {
                    return com.Serializar();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string LeerCompetencias()
        {
            ColeccionCompetencia colCom = new ColeccionCompetencia();
            return colCom.Serializar(colCom.ReadAllCompetencias());
        }
        #endregion

        //---------------------------------------------------------//

        #region Habilidad
        // Habilidad

        public bool CrearHabilidad(string xml)
        {
            Habilidad hab = new Habilidad(xml);
            return hab.Create();
        }

        public bool ActualizarHabilidad(string xml)
        {
            Habilidad hab = new Habilidad(xml);
            return hab.Update();
        }

        public bool EliminarHabilidad(string xml)
        {
            Habilidad hab = new Habilidad(xml);
            return hab.Delete();
        }

        public string LeerHabilidad(string xml)
        {
            try
            {
                Habilidad hab = new Habilidad(xml);
                if (hab.Read())
                {
                    return hab.Serializar();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string LeerHabilidades()
        {
            ColeccionHabilidad colCom = new ColeccionHabilidad();
            return colCom.Serializar(colCom.ReadAllHabilidades());
        }

        public string LeerHabPorCom(int id)
        {
            ColeccionHabilidad colCom = new ColeccionHabilidad();
            return colCom.Serializar(colCom.ObtenerHabPorCom(id));
        }
        #endregion

        //---------------------------------------------------------//

        #region Periodo de evaluacion
        // Periodo de Evaluacion
        public bool CrearPeriodoEvaluacion(string xml)
        {
            PeriodoEvaluacion pe = new PeriodoEvaluacion(xml);
            return pe.Create();
        }

        public bool ActualizarPeriodoEvaluacion(string xml)
        {
            PeriodoEvaluacion pe = new PeriodoEvaluacion(xml);
            return pe.Update();
        }

        public bool EliminarPeriodoEvaluacion(string xml)
        {
            PeriodoEvaluacion pe = new PeriodoEvaluacion(xml);
            return pe.Delete();
        }

        public string LeerPeriodoEvaluacion(string xml)
        {
            try
            {
                PeriodoEvaluacion pe = new PeriodoEvaluacion(xml);
                if (pe.Read())
                {
                    return pe.Serializar();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string LeerPeriodosEvaluaciones()
        {
            ColeccionPeriodoEvaluacion colPE = new ColeccionPeriodoEvaluacion();
            return colPE.Serializar(colPE.ReadAllPeriodos());
        }
        #endregion

        //---------------------------------------------------------//

        #region Usuario
        // Usuario
        public bool ValidarUsuario(string xml)
        {
            try
            {
                Usuario us = new Usuario(xml);
                return us.ValidarUsuario();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public bool CrearUsuario(string xml)
        {
            Usuario us = new Usuario(xml);
            return us.Create();
        }

        public bool ActualizarUsuario(string xml)
        {
            Usuario us = new Usuario(xml);
            return us.Update();
        }

        public bool EliminarUsuario(string xml)
        {
            Usuario us = new Usuario(xml);
            return us.Delete();
        }

        public string LeerUsuario(string xml)
        {
            try
            {
                Usuario us = new Usuario(xml);
                if (us.Read())
                {
                    return us.Serializar();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string LeerUsuarios()
        {
            ColeccionUsuario colUs = new ColeccionUsuario();
            return colUs.Serializar(colUs.ReadAllUsuarios());
        }

        public bool Desactivado(string xml)
        {
            try
            {
                Usuario us = new Usuario(xml);
                return us.Desactivado();

            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        #endregion

        //---------------------------------------------------------//

        #region Area
        // Area
        public bool CrearArea(string xml)
        {
            Area ar = new Area(xml);
            return ar.Create();
        }

        public bool ActualizarArea(string xml)
        {
            Area ar = new Area(xml);
            return ar.Update();
        }

        public bool EliminarArea(string xml)
        {
            Area ar = new Area(xml);
            return ar.Delete();
        }

        public string LeerArea(string xml)
        {
            try
            {
                Area ar = new Area(xml);
                if (ar.Read())
                {
                    return ar.Serializar();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string LeerAreas()
        {
            ColeccionArea colArea = new ColeccionArea();
            return colArea.Serializar(colArea.ReadAllAreas());
        }
        #endregion

        //---------------------------------------------------------//

        #region Perfil de cargo
        // Perfil de Cargo
        /*public bool CrearPerfildeCargo(string xml, List<Area> areaSeleccionada)
        {
            PerfilesdeCargo pc = new PerfilesdeCargo(xml);
            return pc.Create(areaSeleccionada);
        }*/
        public bool CrearPerfildeCargo(string xml)
        {
            PerfilesdeCargo pc = new PerfilesdeCargo(xml);
            List<Area> area = new List<Area>();
            return pc.Create(area);
        }

        public bool ActualizarPerfildeCargo(string xml)
        {
            PerfilesdeCargo pc = new PerfilesdeCargo(xml);
            List<Area> area = new List<Area>();
            return pc.Update(area);
        }

        public bool EliminarPerfildeCargo(string xml)
        {
            PerfilesdeCargo pc = new PerfilesdeCargo(xml);
            return pc.Delete();
        }

        public string LeerPerfildeCargo(string xml)
        {
            try
            {
                PerfilesdeCargo pc = new PerfilesdeCargo(xml);
                if (pc.Read())
                {
                    return pc.Serializar();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string LeerPerfilesdeCargo()
        {
            ColeccionArea colPC = new ColeccionArea();
            return colPC.Serializar(colPC.ReadAllAreas());
        }
        #endregion
    }
}
