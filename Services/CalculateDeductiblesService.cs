using Charity_Calculator_Challange.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charity_Calculator_Challange.Services
{
    public class CalculateDeductiblesService : ICalculateDeductiblesService
    {
        double currentTaxTate = 0.20;

        public CalculateDeductiblesService()
        {

        }

        public decimal Calculate(double donation, double suppliment = 1)
        {
            decimal deductable = Convert.ToDecimal(donation * suppliment * (currentTaxTate / (100 - currentTaxTate)));
            return deductable;
        }
    }
}
