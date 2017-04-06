using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TableTest.Models;

namespace TableTest.Controllers
{
    public class ValuesController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Values/DataHandler")]
        public JsonResult<DatatablesViewModel.DTResult<Customer>> DataHandler(DatatablesViewModel.DTParameters param)
        {
            try
            {
                // lay tao service
                var dataService = new DataService();

                // lay list search colum
                var columnFilters = param.Columns.Select(col => col.Search.Value).ToList();
                var searchString = param.Search.Value;

                //lay list filter
                var datafilter = dataService.GetFilterResult(searchString, columnFilters);

                // dem so resord
                var recordsTotal = datafilter.Count();

                // lay list ket qua search
                var data = dataService.GetCustomersPaging(datafilter, param.SortOrder, param.Start, param.Length);

                var result = new DatatablesViewModel.DTResult<Customer>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal
                };

                return Json(result);
            }
            catch (Exception)
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                throw new HttpResponseException(responseMessage);
            }
        }
    }
    
}
