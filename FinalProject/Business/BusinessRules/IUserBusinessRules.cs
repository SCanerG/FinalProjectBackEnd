using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
    public interface IUserBusinessRules
    {
     IResult UserExists(string email);
        IResult CheckIfPasswordLenghtLessThan(string password);
    }
}
