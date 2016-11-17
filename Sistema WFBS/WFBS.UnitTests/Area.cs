using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFBS.Business.Core;

namespace PruebasUnitarias
{
    [TestClass]
    public class Area
    {
        [TestMethod]
        public void crearArea()
        {

            WFBS.Business.Core.Area a = new WFBS.Business.Core.Area();

            a.id_area = 6;
            a.area = "prueba6";
            a.abreviacion = "p6";
            a.obsoleta = 0;

            bool esperando = true;
            bool actua = a.Create();

            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void modificarArea()
        {

            WFBS.Business.Core.Area a = new WFBS.Business.Core.Area();

            a.id_area = 3;
            
               a.area = "ppppppp";
                a.abreviacion = "p67";
                a.obsoleta = 0;
            

            bool esperando = true;
            bool actua = a.Update();

            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void EliminarArea()
        {

            WFBS.Business.Core.Area a = new WFBS.Business.Core.Area();

            a.Id_area = 1;

            bool esperando = true;
            bool actua = a.Delete();

            Assert.AreEqual(esperando, actua);



        }


    }
}
