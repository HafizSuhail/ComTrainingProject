using FluentNHibernate.Mapping;
using NHibernate_CURD._20.BusinessEntities;

namespace NHibernate_CURD._20.Mappers
{
    public class CityMapper : ClassMap<City>
    {

        public CityMapper()
        {
            Id(p => p.city_id).GeneratedBy.Identity();
            Map(p => p.cityName);
            Table("City");
        }
    }
}
