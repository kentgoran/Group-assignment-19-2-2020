using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyOne
{


    public interface IVehicle
    {

        string RegNum { get; }

        DateTime ArrivalTime { get; }

        int Size { get; }

        IVehicle Clone();

    }
}
