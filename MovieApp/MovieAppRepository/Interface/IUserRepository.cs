﻿using MovieApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppRepository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<EShopApplicationUser> GetAll();
        EShopApplicationUser Get(string id);
        void Insert(EShopApplicationUser entity);
        void Update(EShopApplicationUser entity);
        void Delete(EShopApplicationUser entity);

    }
}
