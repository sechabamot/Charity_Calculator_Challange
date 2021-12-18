using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charity_Calculator_Challange.Controllers
{
    [ApiController]
    public class TaxRateController : ControllerBase
    {
        public TaxRateController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();      
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }
    }
}
