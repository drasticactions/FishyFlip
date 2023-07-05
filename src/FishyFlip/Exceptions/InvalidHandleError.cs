using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Exceptions;

public class InvalidHandleError : Exception
{
    public InvalidHandleError(string message) : base(message)
    {
    }
}
