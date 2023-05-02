using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORA_UI_PAMS_Demo.Models {
    public class History
    {
        public int Id { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
    }
}
