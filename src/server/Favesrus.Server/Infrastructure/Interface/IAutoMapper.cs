﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Server.Infrastructure.Interface
{
    public interface IAutoMapper
    {
        T Map<T>(object objectToMap);
    }
}
