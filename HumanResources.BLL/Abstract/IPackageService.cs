﻿using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Abstract
{
    public interface IPackageService : IService<Package>
    {
        public IEnumerable<Package> GetByUsageAmount(int companyId);
    }
}
