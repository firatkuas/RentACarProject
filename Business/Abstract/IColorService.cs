using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface IColorService
    {
        IResult Add(Color car);
        IResult Update(Color car);
        IResult Delete(Color car);
        IDataResult<List<Color>> GetAll();
        IDataResult<Color> GetById(int id);
    }
}
