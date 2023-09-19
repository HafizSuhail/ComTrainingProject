using Microsoft.AspNetCore.Mvc;
using NHibernate_CURD._20.BusinessEntities;
using NHibernate_CURD._20.Models;
using NHibernate_CURD._20.Services;

namespace NHibernate_CURD._20.Controllers
{
    public class HospitalController : Controller
    {
        private readonly HospitalService _hospitalService;

        public HospitalController()
        {
            _hospitalService = new HospitalService();
        }
        public IActionResult HospitalList()
        {
            var hoslist = _hospitalService.GetFilterdData();
            return View(hoslist);
        }

        [HttpGet]
        public IActionResult Registration_EditForm(int? recordId) 
       {

            if (recordId.HasValue)
            {
                var hospital = _hospitalService.GetRecordById(recordId.Value);
                return View(hospital);
            }
           else
            {
                HospitalEditorModel countyList = _hospitalService.PrepareHospitalEditor();
                return View(countyList);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(HospitalEditorModel uiInputs) 
        {
            if(uiInputs.Hospitalid == 0) 
            {
                _hospitalService.CreateHospital(uiInputs);
                return RedirectToAction("HospitalList");
            }
            
            else
            {
                _hospitalService.GetRecordById(uiInputs.Hospitalid);
                _hospitalService.UpdateHosRecord(uiInputs);
                
            }
            return RedirectToAction("HospitalList");
        }

        public JsonResult DeleteHosRecord(int recordId) 
        {
            try
            {
                _hospitalService.DeleteOperation(recordId);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
           


        }
            
        
        
    }
}
