using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace NHibernate_CURD._20.Models
{
    public class HospitalEditorModel
    {
        [HiddenInput]
        public int Hospitalid { get; set; }

        [Display(Name = "Hospital Name")]
        public string HospitalName { get; set; } = string.Empty;

        [Display(Name = "State")]
        public string HosState { get; set; } = string.Empty;

        [Display(Name = "Zip Code")]
        public int Zipcode { get; set; }

        [Display(Name = "Country Name")]
        public int Cityid { get; set; }

        [IgnoreDataMember]
        public List<SelectListItem> Cities { get; set; }


    }
}
