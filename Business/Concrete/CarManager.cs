using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
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
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;

        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _carDal = carDal;
            _brandService = brandService;
        }

        [SecuredOperation("admin,car.add")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfCarNameExist(car.Description), 
                CheckIfCarCountOfBrandCorrect(car.BrandId),
                CheckIfBrandLimitExceded());

            if (result != null)
            {
                return result;
            }
            _carDal.Add(car);
            return new SuccessResult();
            //ValidationTool.Validate(new CarValidator(), car);


            //if (car.Description.Length >= 2 && car.DailyPrice > 0)
            //{
            //    _carDal.Add(car);
            //    return new SuccessResult();
            //}
            //return new ErrorResult(Messages.CheckCarNameOrDailyPrice);
        }

        public IResult Delete(Car car)
        {

            _carDal.Delete(car);
            return new SuccessResult();
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        public IResult Update(Car car)
        {

            IResult result = BusinessRules.Run(CheckIfCarNameExist(car.Description), CheckIfCarCountOfBrandCorrect(car.BrandId));

            if (result != null)
            {
                return result;
            }
            _carDal.Update(car);
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.CarCountOfBrandError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCarNameExist(string carName)
        {
            var result = _carDal.GetAll(c => c.Description == carName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CarAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfBrandLimitExceded()
        {
            var result = _brandService.GetAll().Data.Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.BrandLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
