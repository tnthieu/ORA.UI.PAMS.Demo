using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORA_UI_PAMS_Demo.Models {
    public class SpecialNote
    {
        public int Id { get; set; }
        public string InstitutionAttachmentKey { get; set; }
        public bool IsActive { get; set; }
        public bool IsImportant { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string UserName { get; set; }
        public string UID { get; set; }
        public string CreatedBy { get; set; }
        public string AttachmentName { get; set; }
        public string FullAttachmentName { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        public string EditComment { get; set; }
        public bool InitialCreate { get; set; } = false;
        public string OriginalComment { get; set; } = string.Empty;
        public string AttachmentCount { get; set; } = "0";
        public string Fund { get; set; } = string.Empty;
        public string FundId { get; set; } = string.Empty;
    }
}
