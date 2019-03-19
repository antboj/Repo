using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Classes.Expressions
{
    public class RuleInfo
    {
        // Naziv polja, x.Property
        public string Property { get; set; }
        // Operator
        public string Operator { get; set; }
        // Vrijednost za filtraciju, constant
        public string Value { get; set; }
    }
}
