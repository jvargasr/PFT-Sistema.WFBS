using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFBS.Business.Core;

using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class PerfilesdeCargo
    {
        [TestMethod]
        public void crearPerfildeCargo()
        {

            WFBS.Business.Core.PerfilesdeCargo pc = new WFBS.Business.Core.PerfilesdeCargo();
            pc.descripcion = "Descripción de prueba";
            pc.Obsoleto = 0;

            List<WFBS.Business.Core.Area> areas = new List<WFBS.Business.Core.Area>();
            WFBS.Business.Core.Area a = new WFBS.Business.Core.Area();
            a.id_area = 1;
            areas.Add(a);

            bool esperando = true;
            bool actua = pc.Create(areas);

            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void modificarArea()
        {

            WFBS.Business.Core.PerfilesdeCargo pc = new WFBS.Business.Core.PerfilesdeCargo();
            pc.Id_PC = 21;
            pc.descripcion = "Actualización de prueba";
            pc.Obsoleto = 0;

            List<WFBS.Business.Core.Area> areas = new List<WFBS.Business.Core.Area>();
            WFBS.Business.Core.Area a = new WFBS.Business.Core.Area();
            a.id_area = 2;
            areas.Add(a);


            bool esperando = true;
            bool actua = a.Update();

            Assert.AreEqual(esperando, actua);



        }

        [TestMethod]
        public void EliminarArea()
        {

            WFBS.Business.Core.PerfilesdeCargo pc = new WFBS.Business.Core.PerfilesdeCargo();

            pc.Id_PC = 22;

            bool esperando = true;
            bool actua = pc.Delete();

            Assert.AreEqual(esperando, actua);



        }


    }
}
