using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFBS.Business.Core;

namespace PruebasUnitarias
{
    [TestClass]
    public class Periodo
    {
        [TestMethod]
        public void crearPeriodo()
        {

            WFBS.Business.Core.PeriodoEvaluacion p = new WFBS.Business.Core.PeriodoEvaluacion();

            p.Id_Periodo = 3;
            p.fechaInicio = DateTime.Parse("10/12/2016");
            p.vigencia = 28;
            p.porcentajeE = 40;
            p.porcentajeAE = 60;
          

            bool esperando = true;
            bool actua = p.Create();

            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void modificarPeriodo()
        {

            WFBS.Business.Core.PeriodoEvaluacion p = new WFBS.Business.Core.PeriodoEvaluacion();

            p.idPeriodo = 24;
            p.fechaInicio = DateTime.Parse("10/10/2016");
            p.vigencia = 30;
            p.porcentajeE = 60;
            p.porcentajeAE = 40;


            bool esperando = true;
            bool actua = p.Update();

            Assert.AreEqual(esperando, actua);

        }

        [TestMethod]
        public void EliminarPeriodo()
        {

            WFBS.Business.Core.PeriodoEvaluacion p = new WFBS.Business.Core.PeriodoEvaluacion();

            p.idPeriodo = 24;

            bool esperando = true;
            bool actua = p.Delete();

            Assert.AreEqual(esperando, actua);


        }
    }
}
