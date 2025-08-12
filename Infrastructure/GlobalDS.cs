using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum Lifetime
    {
        Singleton = 0,
        Scoped = 1,
        Transient = 2
    }
}
