﻿using HumanResources.Core.Entities;
using HumanResources.DAL.Context;
using HumanResources.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Concrete
{
    public class PackageRepository : GenericRepository<Package>, IPackageDal
    {
        private readonly ApplicationDbContext db;

        public PackageRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
    }
}
