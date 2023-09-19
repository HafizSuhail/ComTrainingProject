using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NHibernate_CURD._20.BusinessEntities
{
    public class Patients
    {

        public virtual int patient_id { get; set; }

        [Display(Name = "PatientName")]
        public virtual string patientName { get; set; } = null!;

        [Display(Name = "Gender")]
        public virtual string gender { get; set; } = null!;

        [Display(Name = "Age")]
        public virtual int age { get; set; }

        [Display(Name = "MobileNumber")]
        public virtual string? mobileno { get; set; }
    }
}
