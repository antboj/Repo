using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Classes.Expressions
{
    public class FilterInfo
    {
        // Povezivanje uslova : And | Or
        public string Condition { get; set; }
        // Uslovi
        public List<RuleInfo> Rules { get; set; } = new List<RuleInfo>();
    }
}
