namespace NHibernate_CURD._20.Services
{
    public class PatientService
    {
        //private readonly IServiceCollection _services;

        private readonly ISession _session;

        public PatientService(ISession session)
        {
            _session = session;
        }




    }
}
