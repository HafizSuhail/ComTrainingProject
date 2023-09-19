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
        // Declare the readonly property of db connection 
        private readonly ISessionFactory _sessionFactory;

        //Create a constructure to initialize the variable 
        public HospitalService()
        {
            // Its a method of DB Connection of NHibernate, refer the (NHibernateHelper Class)
            _sessionFactory = NHibernateHelper.CreateSessionFactory();

        }

        // Method to get data from db which has forign key in th table 
        public List<HospitalDto> GetFilterdData()
        {
                // Create a List to keep the filtered data into it
                List<HospitalDto> hoslistdata = new List<HospitalDto>();

                
                //first get connect with db class
                using (var session = _sessionFactory.OpenSession())
                {
                    //Declare the variable for Alias  
                    Hospital hosAlias = null;
                    City cityAlias = null;
                    HospitalDto hospitalDto = null;

                    // Write the Query to fetch the data from DB table convert into list & assign to list variable
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

        // Method to preparemodel & dropdown list data
        public HospitalEditorModel PrepareHospitalEditor()
        {
            // Create the DB Connection
            var session = _sessionFactory.OpenSession(); 

            //  Get list of Cities objects from DB
            var cityList = session.Query<City>().ToList();

            // Create the variable of hospitaleditormodel
            var dropDownList = new HospitalEditorModel();

            // Create the select listitem for Cities
            dropDownList.Cities = new List<SelectListItem>();

            dropDownList.Cities.Add(new SelectListItem { Value = null, Text = "--Select Country--" });

            // Make foreach to loop Cities 
            foreach (var cityitem in cityList)
            {
                // Create the Cityname variable
                var cityname = $"{cityitem.cityName}";

                //  Save the each object in list
                var countryItem = new SelectListItem
                {
                    Value = cityitem.city_id.ToString(),
                    Text = cityname
                };

                // Add data into dropdownlist
                dropDownList.Cities.Add(countryItem);
            }

            return dropDownList;
        }

        // Method to getrecord by ID & ITS Dropdownlist cities data
        public HospitalEditorModel GetRecordById(int recordId)
        {
            // Create the Connection
            using (var session = _sessionFactory.OpenSession())
            {
                // Create a varaible & Fetch the recordID from Database
                var hosRecord = session.Query<Hospital>().Where(r => r.hospital_id == recordId).FirstOrDefault();

                // create an object of model class
                var recorddata_litsdata = new HospitalEditorModel();

                //  Get list of Cities objects from DB
                var cityList = session.Query<City>().ToList();

                // Create the select listitem for Cities
                recorddata_litsdata.Cities = new List<SelectListItem>(); 

                recorddata_litsdata.Cities.Add(new SelectListItem { Value = null, Text = "--Select Country--" });

                // Make foreach to loop Cities 
                foreach (var cityitem in cityList)
                {
                    // Create the Cityname variable
                    var cityname = $"{cityitem.cityName}";

                    //  Save the each object in list
                    var countryItem = new SelectListItem
                    {
                        Value = cityitem.city_id.ToString(),
                        Text = cityname
                    };

                    recorddata_litsdata.Cities.Add(countryItem);
                }

                // And bind the data from Model fetch_variable to DB_variabl 

                recorddata_litsdata.Hospitalid = hosRecord.hospital_id;
                recorddata_litsdata.HospitalName = hosRecord.hospitalName;
                recorddata_litsdata.HosState = hosRecord.hosState;
                recorddata_litsdata.Zipcode = hosRecord.zipcode;
                recorddata_litsdata.Cityid = hosRecord.city_id;

                return recorddata_litsdata;
            }
            
        }

        //Method to Add data into database table 
        public Hospital CreateHospital (HospitalEditorModel userInputs)
        {
            
            // Create the Connection
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {

                // Create an object of Hospital Entity Class 

                var newrecord = new Hospital
                {
                    //3,Assign form input to Entity Class object & save it 
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

        //Method to update data into database table 
        public Hospital UpdateHosRecord(HospitalEditorModel upinputs)
        {
            // Create the Connection
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                // Create a varaible & Fetch the recordID from Database
                var fetchRecord = session.Query<Hospital>().Where(r => r.hospital_id == upinputs.Hospitalid).FirstOrDefault();

                // And bind the data from DB fetch_variabl to Model fetch_variable from UI & Update it

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

            // Create a varaible & Fetch the recordID from Database
            var fetchRecord = session.Query<Hospital>().Where(r => r.hospital_id == id).FirstOrDefault();
            //Delete the fetch_Record
            session.Delete(fetchRecord);
            transaction.Commit();

        }


    }
}
