using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Repo.Classes.Expressions
{
    public class SortInfo
    {
        // Polje po kojem sortiramo, p.Property
        public string Property { get; set; }
        // asc || desc
        public string SortDirection { get; set; }

    }
}
