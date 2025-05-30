using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
    public class UserBusinessRules:IUserBusinessRules
    {
        IUserService _userService;
        public UserBusinessRules(IUserService userService) {
            _userService = userService;
        }
        public IResult CheckIfPasswordLenghtLessThan(string password)
        {

            if (password.ToString().Length < 6)
            {
                return new ErrorResult(Messages.PasswordLengthError);
            }
            return new SuccessResult();
        }
        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
