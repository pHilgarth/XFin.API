﻿using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class CostCenterAssetModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
