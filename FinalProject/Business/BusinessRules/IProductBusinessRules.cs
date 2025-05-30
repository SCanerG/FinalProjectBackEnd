using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
    public interface IProductBusinessRules
    {
        IResult CheckIfProductCountOfCategoryCorrect(int categoryId);
        IResult CheckIfProductNameExists(string productName);
        IResult CheckIfCategoryLimitExceeded();
    }
}
