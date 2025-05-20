using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Entities.Concrete.User;

namespace Core.Utilities.Security.JWT
{
    public partial interface ITokenHelper
    {

        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
