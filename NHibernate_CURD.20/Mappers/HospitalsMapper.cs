using FluentNHibernate.Mapping;
using NHibernate_CURD._20.BusinessEntities;

namespace NHibernate_CURD._20.Mappers
{
    public class HospitalsMapper :ClassMap<Hospital>
    {
        public HospitalsMapper()
        {
            Id(x => x.hospital_id).GeneratedBy.Identity();
            Map(x => x.hospitalName);
            Map(x => x.hosState);
            Map(x => x.zipcode);
            Map(x => x.city_id);
            References(p => p.city, "city_id").ReadOnly();
            Table("Hospital");
        }

    }
}
