using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Append_Delegate
{
    class test
    {
        private TextAppendDelegate mydel;
        public test(TextAppendDelegate d){
            mydel = d;
        }
        public void sendText(object txt)
        {
            mydel((string)txt);
        }
    }
}
