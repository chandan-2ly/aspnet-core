﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Model
{
    public class WebApiResponseModel
    {
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
    }
}
