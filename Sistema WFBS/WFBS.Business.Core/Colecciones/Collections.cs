using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.DAL;

namespace WFBS.Business.Core
{
    public class Collections
    {/* Usuario */
        public List<Usuario> ReadAllUsuarios()
        {
            var usuariosBDD = from u in CommonBC.ModeloWFBS.USUARIO
                              join a in CommonBC.ModeloWFBS.AREA on u.ID_AREA equals a.ID_AREA
                              join p in CommonBC.ModeloWFBS.PERFIL on u.ID_PERFIL equals p.ID_PERFIL

                              select new Usuario
                              {
                                  Rut = u.RUT,
                                  Nombre = u.NOMBRE,
                                  Sexo = (u.SEXO == "M" ? "Masculino" : u.SEXO == "F" ? "Femenino" : "No determinado"),
                                  Jefe = u.JEFE_RESPECTIVO,
                                  Perfil = p.TIPO_USUARIO,
                                  Area = (p.TIPO_USUARIO == "administrador" ? "" : (p.TIPO_USUARIO == "jefe" || p.TIPO_USUARIO == "funcionario") ? a.NOMBRE : "No determinado"),
                                  Password = u.PASSWORD,
                                  Obs = (u.OBSOLETO == 0 ? "No" : u.OBSOLETO == 1 ? "Si" : "No determinado"),
                              };
            return usuariosBDD.ToList();
        }


        private List<Core.Usuario> GenerarListado(List<DAL.USUARIO> usuariosBDD)
        {
            List<Core.Usuario> usuariosController = new List<Usuario>();

            foreach (DAL.USUARIO item in usuariosBDD)
            {
                Core.Usuario us = new Usuario();

                us.Rut = item.RUT;
                us.Nombre = item.NOMBRE;
                us.Sexo = item.SEXO;
                us.Id_Area = Convert.ToInt32(item.ID_AREA);
                us.Id_Perfil = Convert.ToInt32(item.ID_PERFIL);
                us.Jefe = item.JEFE_RESPECTIVO;
                us.Obsoleto = Convert.ToInt32(item.OBSOLETO);

                usuariosController.Add(us);
            }

            return usuariosController;

        }

        /* Area */
        public List<Area> ReadAllAreas()
        {
            var AreasBDD = from c in CommonBC.ModeloWFBS.AREA /*AreasBDD*/

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

                areasController.Add(ar);
            }

            return areasController;
        }
        /* Perfiles de cargo */
        public List<PerfilesdeCargo> ReadAllPerfilesdeCargo()
        {
            //List<Modelo.PERFIL_DE_CARGO> perfilesdecargoBDD = CommonBC.ModeloWfbs.PERFIL_DE_CARGO.ToList();
            //return GenerarListado(perfilesdecargoBDD);
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

        /* Perfil */
        public List<Perfil> ReadAllPerfiles()
        {
            List<DAL.PERFIL> perfilesBDD = CommonBC.ModeloWFBS.PERFIL.ToList();
            return GenerarListadoPerfil(perfilesBDD);
        }

        private List<Perfil> GenerarListadoPerfil(List<PERFIL> perfilesBDD)
        {
            List<Core.Perfil> perfilesController = new List<Perfil>();

            foreach (DAL.PERFIL item in perfilesBDD)
            {
                Core.Perfil p = new Perfil();

                p.id_pefil = Convert.ToInt32(item.ID_PERFIL);
                p.perfil = item.TIPO_USUARIO;

                perfilesController.Add(p);
            }

            return perfilesController;
        }


        /* reporte grupal en construcción */
        /* Obtener competencias por area */
        public List<int> CompetenciasPorArea(int id_area)
        {
            List<int> ids_competencias = new List<int>();
            var CompetenciasDB = CommonBC.ModeloWFBS.COMPETENCIA; //.Where(relacion n:m para sacar competencia por área)
            foreach (var item in CompetenciasDB)
            {
                ids_competencias.Add((int)item.ID_COMPETENCIA);
            }
            return ids_competencias;
        }

        /* Obtener periodo de evaluación activo */
        public int ObtenerPeriodoEvaluacion()
        {
            DateTime hoy = DateTime.Now;
            var id = CommonBC.ModeloWFBS.PERIODO_EVALUACION.Where(p => hoy >= p.FECHA_INICIO && hoy <= (p.FECHA_INICIO.AddDays((double)p.VIGENCIA))).Select(p => p.ID_PERIODO_EVALUACION).First();
            return (int)id;
        }

        /* Reporte evaluación por grupo */
        public List<float> ObtenerNotasCompetencia(int id_area, int id_competencia)
        {//      Obtener las brechas de todos los funcionarios
            int id_periodo = 1;//ObtenerPeriodoEvaluacion();
            var notasDB = CommonBC.ModeloWFBS.EVALUACION.Where(e => e.ID_PERIODO_EVALUACION == id_periodo && e.ID_AREA == id_area
            && e.ID_COMPETENCIA == id_competencia).Select(e => e.NOTA_ESPERADA_COMPETENCIA - e.NOTA_ENCUESTA);
            List<float> notas = new List<float>();
            foreach (var item in notasDB)
            {
                notas.Add((float)item);
            }
            return notas;
        }
        /* Fin reporte grupal en construcción */

        /* Usuario Jefe */
        public List<Usuario> ObtenerJefes()
        {
            var Jefes = CommonBC.ModeloWFBS.USUARIO.Where(usu => usu.ID_PERFIL == 2);
            return (GenerarListado(Jefes.ToList()));
        }

        /* Competencia */
        public List<Competencia> ReadAllCompetencias()
        {
            /*List<Modelo.COMPETENCIA> CompetenciasBDD = CommonBC.ModeloWfbs.COMPETENCIA.ToList();
            return GenerarListadoCompetencia(CompetenciasBDD);*/

            var CompetenciasBDD = from c in CommonBC.ModeloWFBS.COMPETENCIA /*usuariosBDD*/

                                  select new Competencia
                                  {
                                      Id_com = c.ID_COMPETENCIA,
                                      Nombre = c.NOMBRE,
                                      Descripcion = c.DESCRIPCION,
                                      Sigla = c.SIGLA,
                                      Obs = (c.OBSOLETA == 0 ? "No" : c.OBSOLETA == 1 ? "Si" : "No determinado"),
                                      Nivel_O = c.NIVEL_OPTIMO_ESPERADO,
                                      Pregunta_Asociada = c.PREGUNTA_ASOCIADA
                                  };
            return CompetenciasBDD.ToList();
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

                competenciasController.Add(com);
            }

            return competenciasController;
        }


        // Periodo Evaluacion
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

                periodosController.Add(ar);
            }

            return periodosController;
        }


        /* Habilidad */
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
    }
}
