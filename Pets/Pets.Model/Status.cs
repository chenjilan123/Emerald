using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Model
{
    public enum Status
    {
        OK = StatusCodes.Status200OK,
        Created = StatusCodes.Status201Created,
        Accepted = StatusCodes.Status202Accepted,
        BadRequest = StatusCodes.Status400BadRequest,
        NotFound = StatusCodes.Status404NotFound,
        
    }
}
