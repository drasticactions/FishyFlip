using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Exceptions;

public class InvalidDidError : Exception
{
    public InvalidDidError(string message) : base(message)
    {
    }
}
