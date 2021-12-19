using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charity_Calculator_Challange.Interfaces
{
    public interface ICalculateDeductiblesService
    {
        decimal Calculate(double donation, double suppliment = 1); 
    }
}
