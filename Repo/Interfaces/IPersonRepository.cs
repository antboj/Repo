﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Models;

namespace Repo.Interfaces
{
    public interface IPersonRepository : IRepository<Person, int>
    {
    }
}
