using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Services;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalValueController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public ExternalValueController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public string Get() => _paymentService.GetMessage();
    }
}