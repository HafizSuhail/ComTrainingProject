using System.ComponentModel.DataAnnotations;

namespace NHibernate_CURD._20.Customes
{
    public class HospitalDto
    {
        public int hospital_id { get; set; }

        [Display(Name = "Hospital Name")]
        public string hospitalName { get; set; } = string.Empty;

        [Display(Name = "State")]
        public string hosState { get; set; } = string.Empty;

        [Display(Name = "Zip Code")]
        public int zipcode { get; set; }

        [Display(Name = "Country Name")]
        public int  city_id { get; set; }
        public string? cityName { get; set; }

    }
}
