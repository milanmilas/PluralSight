﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor
{
    public interface ILibraryDocumentFactory
    {
        object GetDocument { get; }
    }
}
