using Microsoft.AspNetCore.Mvc.Rendering;
using NHibernate;
using NHibernate.Transform;
using NHibernate_CURD._20.BusinessEntities;
using NHibernate_CURD._20.Customes;
using NHibernate_CURD._20.Helpers;
using NHibernate_CURD._20.Models;
using ISession = NHibernate.ISession;

namespace NHibernate_CURD._20.Services
{
    public class HospitalService
    {

        private readonly ISessionFactory _sessionFactory;

        public HospitalService()
        {
            _sessionFactory = NHibernateHelper.CreateSessionFactory();

        }

        public List<HospitalDto> GetFilterdData()
        {
                List<HospitalDto> hoslistdata = new List<HospitalDto>();

                //ISessionFactory sessionFactory = GSMS.Framework.ORM.NHibernateHelper.Instance.SessionFactory;

                using (var session = _sessionFactory.OpenSession())
                {
                    Hospital hosAlias = null;
                    City cityAlias = null;
                    HospitalDto hospitalDto = null;
                    var qOver = session.QueryOver<Hospital>(() => hosAlias)
                                   .JoinAlias(() => hosAlias.city, () => cityAlias)
                                   .SelectList(list => list
                    .Select(e => e.hospital_id).WithAlias(() => hospitalDto.hospital_id)
                    .Select(e => e.hospitalName).WithAlias(() => hospitalDto.hospitalName)
                    .Select(e => e.hosState).WithAlias(() => hospitalDto.hosState)
                    .Select(e => e.zipcode).WithAlias(() => hospitalDto.zipcode)
                    .Select(() => cityAlias.cityName).WithAlias(() => hospitalDto.cityName)
                    ).TransformUsing(Transformers.AliasToBean<HospitalDto>())
                    .List<HospitalDto>();
                    hoslistdata = qOver.ToList();
                    return hoslistdata;
                }
        }

        public HospitalEditorModel PrepareHospitalEditor()
        {
            //Create an object of model class for keep list in it

            //1st  create the Connection
            var session = _sessionFactory.OpenSession(); 

            // 2nd we are getting list of Cities objects from DB
            var cityList = session.Query<City>().ToList();

            
            var dropDownList = new HospitalEditorModel(); // Create the Varaible of hospitaleditormodel
            dropDownList.Cities = new List<SelectListItem>(); // Create the select listitem for Categories

            dropDownList.Cities.Add(new SelectListItem { Value = null, Text = "--Select Country--" });

            // create Categories loop using for each
            foreach (var cityitem in cityList)
            {
                // Create the categoryname variable
                var cityname = $"{cityitem.cityName}";

                var countryItem = new SelectListItem
                {
                    Value = cityitem.city_id.ToString(),
                    Text = cityname
                };

                dropDownList.Cities.Add(countryItem);
            }

            return dropDownList;
        }

        public HospitalEditorModel GetRecordById(int recordId)
        {
            // Create the Connection
            using (var session = _sessionFactory.OpenSession())
            {
                // Create a varaible & Fetch the recordID from Database
                var hosRecord = session.Query<Hospital>().Where(r => r.hospital_id == recordId).FirstOrDefault();
                // create an object of model class
                var recorddata_litsdata = new HospitalEditorModel();

                var cityList = session.Query<City>().ToList();


                recorddata_litsdata.Cities = new List<SelectListItem>(); // Create the select listitem for Categories

                recorddata_litsdata.Cities.Add(new SelectListItem { Value = null, Text = "--Select Country--" });

                // create Categories loop using for each
                foreach (var cityitem in cityList)
                {
                    // Create the categoryname variable
                    var cityname = $"{cityitem.cityName}";

                    var countryItem = new SelectListItem
                    {
                        Value = cityitem.city_id.ToString(),
                        Text = cityname
                    };

                    recorddata_litsdata.Cities.Add(countryItem);
                }

                // and bind the data from obj

                recorddata_litsdata.Hospitalid = hosRecord.hospital_id;
                recorddata_litsdata.HospitalName = hosRecord.hospitalName;
                recorddata_litsdata.HosState = hosRecord.hosState;
                recorddata_litsdata.Zipcode = hosRecord.zipcode;
                recorddata_litsdata.Cityid = hosRecord.city_id;

                return recorddata_litsdata;
                //return session.Get<Hospital>(recordId);
            }
            
        }

        public Hospital CreateHospital (HospitalEditorModel userInputs)
        {
            
            // Create the Connection
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {

                // Create an object of Hospital Entity Class 

                var newrecord = new Hospital
                {
                    //3,Assign form input to Entity Class object
                    hospital_id = userInputs.Hospitalid,
                    hospitalName = userInputs.HospitalName,
                    hosState = userInputs.HosState,
                    zipcode = userInputs.Zipcode,
                    city_id = userInputs.Cityid
                };
                session.Save(newrecord);
                transaction.Commit();

                return newrecord;
            }
        }

        public Hospital UpdateHosRecord(HospitalEditorModel upinputs)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var fetchRecord = session.Query<Hospital>().Where(r => r.hospital_id == upinputs.Hospitalid).FirstOrDefault();

                // and bind the data from obj

                fetchRecord.hospitalName = upinputs.HospitalName;
                fetchRecord.hosState = upinputs.HosState;
                fetchRecord.zipcode = upinputs.Zipcode;
                fetchRecord.city_id = upinputs.Cityid;
             
                session.Update(fetchRecord);
                transaction.Commit();
                return fetchRecord;
                
            }
        }

        public void DeleteOperation(int id)
        {
            // Create the Connection
            var session = _sessionFactory.OpenSession();
            var transaction = session.BeginTransaction();
            var fetchRecord = session.Query<Hospital>().Where(r => r.hospital_id == id).FirstOrDefault();
            session.Delete(fetchRecord);
            transaction.Commit();

        }


    }
}
