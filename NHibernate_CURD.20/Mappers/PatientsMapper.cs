using FluentNHibernate.Mapping;
using NHibernate_CURD._20.BusinessEntities;

namespace NHibernate_CURD._20.Mappers
{
    public class PatientsMapper : ClassMap<Patients>
    {
        public PatientsMapper()
        {
            Id(x => x.patient_id).GeneratedBy.Identity();
            Map(x => x.patientName);
            Map(x => x.gender);
            Map(x => x.age);
            Map(x => x.mobileno);
            Table("Patients");
        }
    }
}
