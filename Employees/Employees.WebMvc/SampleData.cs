using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Employees.WebMvc
{
    public class SampleData
    {
        public static void Initialize(Employees.Abstract.ILogic logic)
        {
            logic.CreateData();
        }
    }
}
