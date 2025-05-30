using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.BusinessRules;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        IProductDal _productDal;
        ICategoryService _categoryService;
        IProductBusinessRules _productBusinessRules;
        public ProductManager(IProductDal productDal, ICategoryService categoryService, IProductBusinessRules productBusinessRules)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            _productBusinessRules = productBusinessRules;
        }
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {

            IResult result = Core.Utilities.Business.BusinessRules.Run(
                _productBusinessRules.CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                _productBusinessRules.CheckIfProductNameExists(product.ProductName),
                _productBusinessRules.CheckIfCategoryLimitExceeded()
                 );

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);


        }
        [CacheAspect]//Key,value
        public IDataResult<List<Product>> GetAll()
        {
            //yetkisi var mı?
            //if (DateTime.Now.Hour==16)
            //{
            //    return new ErrorDataResult<List<Product>>(_productDal.Getall(), Messages.MaintenanceTime);

            //}
            return new SuccessDataResult<List<Product>>(_productDal.Getall(), Messages.ProductsListed);


        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            //return new ErrorDataResult<List<Product>>(_productDal.Getall(p => p.CategoryId == id) , "Başarısız");

            return new SuccessDataResult<List<Product>>(_productDal.Getall(p => p.CategoryId == id), Messages.ProductsListed);
        }

        [CacheAspect(10)]//Key,value
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.Getall(p => p.UnitPrice >= min && p.UnitPrice <= max), Messages.ProductsListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailDtos()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductsListed);
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        //public IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        //{
        //    var result = _productDal.Getall(p => p.CategoryId == categoryId).Count;
        //    if (result >= 50)
        //    {
        //        return new ErrorResult(Messages.ProductCountOfCategoryError);

        //    }
        //    return new SuccessResult();
        //}
        //public IResult CheckIfProductNameExists(string productName)
        //{
        //    var result = _productDal.Getall(p => p.ProductName == productName).Any();
        //    if (result)
        //    {
        //        return new ErrorResult(Messages.ProductNameAlreadyExists);

        //    }
        //    return new SuccessResult();
        //}
        //public IResult CheckIfCategoryLimitExceeded()
        //{
        //    var result = _categoryService.GetAll();
        //    if (result.Data.Count > 50)
        //    {
        //        return new ErrorResult();

        //    }
        //    return new SuccessResult();
        //}

    }
}