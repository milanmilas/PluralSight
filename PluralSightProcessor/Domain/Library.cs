﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public class Library : SelectableItem
    {
        public List<Course> Courses { get; set; }
    }
}
