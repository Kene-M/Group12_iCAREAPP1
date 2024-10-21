//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Group12_iCAREAPP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DocumentMetadata
    {
        public string ID { get; set; }
        public string docName { get; set; }
        public Nullable<System.DateTime> dateOfCreation { get; set; }
        public string drugUsedID { get; set; }
        public string patientID { get; set; }
        public string workerID { get; set; }
    
        public virtual DrugsDictionary DrugsDictionary { get; set; }
        public virtual PatientRecord PatientRecord { get; set; }
        public virtual iCAREWorker iCAREWorker { get; set; }
    }
}
