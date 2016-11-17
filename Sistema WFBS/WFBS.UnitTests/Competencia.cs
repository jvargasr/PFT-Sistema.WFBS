using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFBS.Business.Core;

namespace PruebasUnitarias
{
    [TestClass]
    public class Competencia
    {
        [TestMethod]
        public void crearCompetencia()
        {

            WFBS.Business.Core.Competencia a = new WFBS.Business.Core.Competencia();

            a.Id_com = 2;
            a.Nombre = "Prueba 3";
            a.Descripcion = "Descripcion de prueba";
            a.Sigla = "p3";
            a.Obsoleta = 0;
            a.Nivel_Optimo = 3 ;

            bool esperando = true;
            bool actua = a.Create();

            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void modificarCompetencia()
        {

            WFBS.Business.Core.Competencia a = new WFBS.Business.Core.Competencia();

            a.Id_competencia = 1;

            a.Nombre = "Prueba 5";
            a.Descripcion = "Descripcion de prueba";
            a.Sigla = "pu5";
            a.Obsoleta = 0;
            a.Nivel_Optimo = 4;


            bool esperando = true;
            bool actua = a.Update();

            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void EliminarCompetencia()
        {

            WFBS.Business.Core.Competencia a = new WFBS.Business.Core.Competencia();

            a.Id_com = 21;

            bool esperando = true;
            bool actua = a.Delete();

            Assert.AreEqual(esperando, actua);



        }
    }
}
