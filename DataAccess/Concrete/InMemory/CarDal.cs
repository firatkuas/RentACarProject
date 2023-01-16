using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class CarDal : ICarDal
    {
        List<Car> cars;
        public CarDal()
        {
            cars = new List<Car>();
        }
        public void Add(Car car)
        {
            cars.Add(car);
        }

        public void Delete(Car car)
        {
            var carDeleted = cars.First(c => c.Id == car.Id);
            cars.Remove(carDeleted);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car GetById(int id)
        {
            return cars.FirstOrDefault(x => x.Id == id);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            var updatedCar = cars.First(x => x.Id == car.Id);
            updatedCar.ModelYear = car.ModelYear;
            updatedCar.Description = car.Description;
            updatedCar.ColorId = car.ColorId;
            updatedCar.DailyPrice=  car.DailyPrice;
            updatedCar.BrandId = car.BrandId;
        }
    }
}
