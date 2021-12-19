using Charity_Calculator_Challange.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charity_Calculator_Challange.Controllers
{
    public class CalculatorController : ControllerBase
    {
        public ICalculateDeductiblesService _calculator { get; }

        public CalculatorController(ICalculateDeductiblesService calculator)
        {
            _calculator = calculator;
        }

        public IActionResult Deductable(double donation, double sup = 1)
        {
            try
            {
                return Ok(_calculator.Calculate(donation, sup));
            }
            catch (Exception)
            {
                //TODO:Log exception
                return StatusCode(500);
            }
        }
    }
}
