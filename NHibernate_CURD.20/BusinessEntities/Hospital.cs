using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NHibernate_CURD._20.BusinessEntities
{
    public class Hospital
    {
        [HiddenInput]
        public virtual int hospital_id { get; set; }

        [Display(Name = "Hospital Name")]
        public virtual string hospitalName { get; set; } = string.Empty;

        [Display(Name = "State")]
        public virtual string hosState { get; set; } = string.Empty;

        [Display(Name = "Zip_Code")]
        public virtual int zipcode { get; set; }

        [Display(Name = "City Name")]
        public virtual int city_id { get; set; }

        public virtual City? city { get; set; }
    }
}
