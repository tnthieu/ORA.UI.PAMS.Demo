using System.Net;

namespace ORA.UI.PAMS.Demo.Models
{
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
        //[Remote("Comment", "SpecialNoteValidate")]
        public string Comment { get; set; }
        public string EditComment { get; set; }
        public bool InitialCreate { get; set; } = false;
        public string OriginalComment { get; set; } = string.Empty;
        public string AttachmentCount { get; set; } = "0";
        public string Fund { get; set; } = string.Empty;
        public string FundId { get; set; } = string.Empty;
        public int Number { get; set; }

        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string Extra3 { get; set; }
        public string Extra4 { get; set; }
        public string Extra5 { get; set; }
        public string Extra6 { get; set; }
        public string Extra7 { get; set; }
        public string Extra8 { get; set; }
        public string Extra9 { get; set; }
        public string Extra10 { get; set; }
        public string Extra11 { get; set; }
        public string Extra12 { get; set; }
        public string Extra13 { get; set; }
        public string Extra14 { get; set; }
        public string Extra15 { get; set; }

    }
}
