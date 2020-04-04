using Microsoft.AspNetCore.Mvc;
using SampleApi.Services;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //private IPaymentService paymentService { get; set; }
        
        //public ValuesController(IPaymentService paymentService)
        //{
        //    this.paymentService = paymentService;
        //}
        
        //public string Get()
        //{
        //    return paymentService.GetMessage();
        //}

        [HttpGet]
        [Route("actioninjection")]
        public ActionResult<string> Get([FromServices]IPaymentService paymentService)
        {
            return paymentService.GetMessage();
        }
    }
}