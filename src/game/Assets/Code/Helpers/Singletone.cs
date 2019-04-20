using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Singletone<TInstance>
        where TInstance : new()
    {
        static Singletone()
        {
            Instance = new TInstance();
        }

        public static TInstance Instance
        {
            get;
        }
    }
}
