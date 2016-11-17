using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFBS.DAL;

namespace WFBS.Business.Core
{
    public class Perfil : ICrud
    {
        public int id_pefil { get; set; }
        public string perfil { get; set; }

        public Perfil()
        {
            this.Init();
        }

        private void Init()
        {
            this.id_pefil = 0;
            this.perfil = string.Empty;
        }

        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }
    }
}
