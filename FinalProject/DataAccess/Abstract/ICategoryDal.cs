﻿using Entities.Concrete;
using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICategoryDal: IEntityRepository<Category>
    {

        //List<Category> Getall();

        //void Add(Category category);
        //void Update(Category category);
        //void Delete(Category category);

    }
}
