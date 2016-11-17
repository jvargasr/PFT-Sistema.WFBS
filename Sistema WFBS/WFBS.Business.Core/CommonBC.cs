using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.DAL;
using System.Data;

namespace WFBS.Business.Core
{
    public class CommonBC
    {
        private static DAL.WFBSEntities _modeloWfbs;

        public static DAL.WFBSEntities ModeloWFBS
        {
            get
            {
                if (_modeloWfbs == null)
                {
                    _modeloWfbs = new WFBS.DAL.WFBSEntities();
                }
                return _modeloWfbs;
            }
        }

        public CommonBC()
        {
        }
    }
}
