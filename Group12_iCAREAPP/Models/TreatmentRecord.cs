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
    
    public partial class TreatmentRecord
    {
        public string ID { get; set; }
        public string workerID { get; set; }
        public string patientID { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> treatmentDate { get; set; }
    
        public virtual iCAREWorker iCAREWorker { get; set; }
        public virtual PatientRecord PatientRecord { get; set; }
    }
}
