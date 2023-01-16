using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICarService carService = new CarManager(new EfCarDal(), new BrandManager(new EfBrandDal()));
            IBrandService brandService = new BrandManager(new EfBrandDal());
            IColorService colorService = new ColorManager(new EfColorDal());
            //IUserService userService = new UserManager(new EfUserDal());
            //ICustomerService customerService = new CustomerManager(new EfCustomerDal());
            //IRentalService rentalService = new RentalManager(new EfRentalDal());

            //IDataResult<List<Car>> result = carService.GetAll();

            //foreach (var item in result.Data)
            //{
            //    Console.WriteLine(item.Description);
            //}

            var carDetails = carService.GetCarDetails();
            if (carDetails.Success)
            {
                foreach (var carDetail in carDetails.Data)
                {
                    Console.WriteLine("{0} / {1} / {2} / {3}", carDetail.CarName,
                        carDetail.BrandName, carDetail.ColorName, carDetail.DailyPrice);
                }
            }
            

            //RentaCar(rentalService);

            //AddUsersAndCompanies(userService, customerService);

            //AddCarBrands(brandService);
            //AddColors(colorService);
            //AddCars(carService);

            //Console.ReadLine();
        }

        private static void RentaCar(IRentalService rentalService)
        {
            IResult result = rentalService.Add(new Rental
            {
                CarId = 1,
                CustomerId = 1,
                RentDate = DateTime.Now,
                ReturnDate = null
            });
            if (result.Success) Console.WriteLine("Araba Kiralandı");
            else Console.WriteLine(result.Message);
        }

        private static void AddUsersAndCompanies(IUserService userService, ICustomerService customerService)
        {
            IResult result = userService.Add(new User
            {
                FirstName = "Fırat",
                LastName = "Kuas",
                Email = "firatkuas@gmail.com",
                //PasswordHash = "Aa123456"
            });
            if (result.Success) Console.WriteLine("Fırat Eklendi");
            result = customerService.Add(new Customer
            {
                UserId = 1
            });
            if (result.Success) Console.WriteLine("Fırat Müşterisi Eklendi");

            result = userService.Add(new User
            {
                FirstName = "Alper",
                LastName = "Erdoğan",
                Email = "1alpererdogan@gmail.com",
                //PasswordHash = "Bb123456"
            });
            if (result.Success) Console.WriteLine("Alper Eklendi");
            result = customerService.Add(new Customer
            {
                UserId = 2,
                CompanyName = "AlperErdoganA.S"
            });
            if (result.Success) Console.WriteLine("Alper Şirketi Eklendi");
        }

        private static void AddCars(ICarService carService)
        {
            IResult result = carService.Add(new Car
            {
                BrandId = 1,
                ColorId = 1,
                DailyPrice = 1000,
                Description = "A3",
                ModelYear = 2021
            });

            if (result.Success) Console.WriteLine("A3 Eklendi");
            result = carService.Add(new Car
            {
                BrandId = 2,
                ColorId = 1,
                DailyPrice = 900,
                Description = "320i Sedan",
                ModelYear = 2020
            });

            if (result.Success) Console.WriteLine("320i Sedan Eklendi");
            result = carService.Add(new Car
            {
                BrandId = 3,
                ColorId = 2,
                DailyPrice = 400,
                Description = "Clio",
                ModelYear = 2022
            });

            if (result.Success) Console.WriteLine("Clio Eklendi");
        }

        private static void AddColors(IColorService colorService)
        {
            IResult result = colorService.Add(new Color
            {
                Name = "White"
            });
            if (result.Success) Console.WriteLine("White Eklendi");
            result = colorService.Add(new Color
            {
                Name = "Black"
            });
            if (result.Success) Console.WriteLine("Black Eklendi");
            result = colorService.Add(new Color
            {
                Name = "Gray"
            });
            if (result.Success) Console.WriteLine("Gray Eklendi");
        }

        private static IResult AddCarBrands(IBrandService brandService)
        {
            IResult result = brandService.Add(new Brand
            {
                Name = "Audi"
            });
            if (result.Success) Console.WriteLine("Audi Eklendi");

            result = brandService.Add(new Brand
            {
                Name = "Bmw"
            });
            if (result.Success) Console.WriteLine("Bmw Eklendi");
            result = brandService.Add(new Brand
            {
                Name = "Renault"
            });
            if (result.Success) Console.WriteLine("Renault Eklendi");
            return result;
        }
    }
}
