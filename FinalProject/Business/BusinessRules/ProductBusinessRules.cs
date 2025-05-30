using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
    public class ProductBusinessRules:IProductBusinessRules
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductBusinessRules(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        public IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.Getall(p => p.CategoryId == categoryId).Count;
            if (result >= 50)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);

            }
            return new SuccessResult();
        }
        public IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.Getall(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);

            }
            return new SuccessResult();
        }
        public IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 50)
            {
                return new ErrorResult();

            }
            return new SuccessResult();
        }
    }
}
