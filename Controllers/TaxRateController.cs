using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charity_Calculator_Challange.Controllers
{
    public class TaxRateController : ControllerBase
    {
        public TaxRateController()
        {

        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok();      
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update()
        {
            return Ok();
        }
    }
}
