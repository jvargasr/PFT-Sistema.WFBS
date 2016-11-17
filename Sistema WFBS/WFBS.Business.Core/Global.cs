using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Core
{
    public static class Global
    {
        private static string _rutUsuario;
        private static string _nombreUsuario;
        private static string _areaInvestigacion;
        private static string _jefeUsuario;

        public static string RutUsuario
        {
            get { return _rutUsuario; }
            set { _rutUsuario = value; }
        }

        public static string NombreUsuario
        {
            get { return _nombreUsuario; }
            set { _nombreUsuario = value; }
        }

        public static string AreaInvestigacion
        {
            get { return _areaInvestigacion; }
            set { _areaInvestigacion = value; }
        }

        public static string JefeUsuario
        {
            get { return _jefeUsuario; }
            set { _jefeUsuario = value; }
        }

        public static void LimpiarVariables()
        {
            _rutUsuario = String.Empty;
            _nombreUsuario = String.Empty;
            _areaInvestigacion = String.Empty;
            _jefeUsuario = String.Empty;
        }
    }
}
