using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTK
{
    class account
    {
        public string stt;
        public string tk;
        public string mk;
        public string server;
        public string ghiChu;

        public account() { }
        public account(string stt,string tk,string mk, string server, string ghiChu)
        {
            this.stt = stt;
            this.tk = tk;
            this.mk = mk;
            this.server = server;
            this.ghiChu = ghiChu;
        }
    }
}
