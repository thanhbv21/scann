﻿using Scannn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scannn.Data
{
    public interface IGetNews
    {
        Task<NewItemsAPI> PerformGetNewsAsync();
    }
}
