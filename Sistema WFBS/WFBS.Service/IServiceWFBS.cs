using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WFBS.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceWFBS
    {

        #region Competencia
        [OperationContract]
        bool CrearCompetencia(string xml);

        [OperationContract]
        bool ActualizarCompetencia(string xml);

        [OperationContract]
        bool EliminarCompetencia(string xml);

        [OperationContract]
        string LeerCompetencia(string xml);

        [OperationContract]
        string LeerCompetencias();
        #endregion Competencia

        //---------------------------------------------------------//

        #region Habilidad
        [OperationContract]
        bool CrearHabilidad(string xml);

        [OperationContract]
        bool ActualizarHabilidad(string xml);

        [OperationContract]
        bool EliminarHabilidad(string xml);

        [OperationContract]
        string LeerHabilidad(string xml);

        [OperationContract]
        string LeerHabilidades();

        [OperationContract]
        string LeerHabPorCom(int id);
        #endregion Habilidad

        //---------------------------------------------------------//

        #region PeriododeEvaluacion
        [OperationContract]
        bool CrearPeriodoEvaluacion(string xml);

        [OperationContract]
        bool ActualizarPeriodoEvaluacion(string xml);

        [OperationContract]
        bool EliminarPeriodoEvaluacion(string xml);

        [OperationContract]
        string LeerPeriodoEvaluacion(string xml);

        [OperationContract]
        string LeerPeriodosEvaluaciones();
        #endregion PeriododeEvaluacion

        //---------------------------------------------------------//

        #region Usuario
        [OperationContract]
        bool ValidarUsuario(string xml);

        [OperationContract]
        bool Desactivado(string xml);

        [OperationContract]
        bool CrearUsuario(string xml);

        [OperationContract]
        bool ActualizarUsuario(string xml);

        [OperationContract]
        bool EliminarUsuario(string xml);

        [OperationContract]
        string LeerUsuario(string xml);

        [OperationContract]
        string LeerUsuarios();
        #endregion Usuario

        //---------------------------------------------------------//

        #region Area
        [OperationContract]
        bool CrearArea(string xml);

        [OperationContract]
        bool ActualizarArea(string xml);

        [OperationContract]
        bool EliminarArea(string xml);

        [OperationContract]
        string LeerArea(string xml);

        [OperationContract]
        string LeerAreas();
        #endregion Area

        //---------------------------------------------------------//

        #region PerfildeCargo
        [OperationContract]
        bool CrearPerfildeCargo(string xml);
        //bool CrearPerfildeCargo(string xml, List<Area> areaSeleccionada);

        [OperationContract]
        bool ActualizarPerfildeCargo(string xml);

        [OperationContract]
        bool EliminarPerfildeCargo(string xml);

        [OperationContract]
        string LeerPerfildeCargo(string xml);

        [OperationContract]
        string LeerPerfilesdeCargo();
        #endregion PerfildeCargo

        // TODO: agregue aquí sus operaciones de servicio
    }

    /*
    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }*/
}
