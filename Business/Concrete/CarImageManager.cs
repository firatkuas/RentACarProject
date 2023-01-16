using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Castle.DynamicProxy.Generators;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile formFile, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckCarImagesLimitExceded(carImage.CarId));

            if (result != null)
            {
                return result;
            }

            if (formFile is null)
            {
                return new ErrorResult(Messages.CarImagesNotFound);
            }

            var randomName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
            var path = Constants.Paths.UploadImages + randomName;
            carImage.Date = DateTime.Now;
            carImage.ImagePath = randomName;

            Helper.FileUpload(formFile, path);

            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            var deletedFile = Constants.Paths.UploadImages;
            deletedFile += _carImageDal.Get(c => c.Id == carImage.Id).ImagePath;

            Helper.FileDelete(deletedFile);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImagesListed);
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int id)
        {
            var result = BusinessRules.Run(CheckIfImagesExists(id));
            if (result != null)
            {
                var carImage = new CarImage
                {
                    Id = default,
                    CarId = id,
                    Date = DateTime.Now,
                    ImagePath = "defaultpath"
                };
                return new SuccessDataResult<List<CarImage>>(new List<CarImage>
                {
                    carImage
                });
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == id),Messages.CarImagesListed);
        }

        public IDataResult<CarImage> GetByCarId(int id)
        {

            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }

        public IResult Update(IFormFile formFile, CarImage carImage)
        {

            var randomName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
            var path = Constants.Paths.UploadImages + randomName;
            carImage.Date = DateTime.Now;
            carImage.ImagePath = randomName;
            var deletedImagePath = Constants.Paths.UploadImages;
            deletedImagePath += _carImageDal.Get(c => c.Id == carImage.Id).ImagePath;

            Helper.FileUpload(formFile, path);
            Helper.FileDelete(deletedImagePath);

            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        private IResult CheckCarImagesLimitExceded(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceded);
            }
            return new SuccessResult();
        }

        private IResult CheckIfImagesExists(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id).Any();
            if (!result)
            {
                return new ErrorResult(Messages.CarImagesNotFound);
            }
            return new SuccessResult();
        }
    }
}
