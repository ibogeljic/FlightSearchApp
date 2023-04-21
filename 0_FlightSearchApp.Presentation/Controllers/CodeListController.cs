using FlightSearchApp.Domain;
using FlightSearchApp.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using FlightSearchApp.Infrastructure;

namespace FlightSearchApp.Presentation
{
    public class CodeListController : Controller
    {
        public ICodeListService CodeListService;

        public CodeListController(ICodeListService codeListService)
        {
            CodeListService = codeListService;
        }

        public IActionResult CodeListIndex()
        {
            return View();
        }

        public JsonResult CodeListGetEntitiesForCombo()
        {
            return Json(CodeListService.ReadEntitiesForCombo(false));
        }
        public JsonResult CodeListGetEntitiesForFilterCombo()
        {
            return Json(CodeListService.ReadEntitiesForCombo(true));
        }
        public JsonResult CodeListReadForDT(string entity)
        {
            return Json(new
            {
                aaData = CodeListService.ReadForDT(entity)
            });
        }
        public string CodeListAdd(CodeList codeList)
        {
            return CodeListService.Create(codeList);
        }
        public void CodeListUpdate(CodeList codeList)
        {
            CodeListService.Update(codeList);
        }
        public void CodeListDelete(int id)
        {
            CodeListService.Delete(id);
        }
    }
}