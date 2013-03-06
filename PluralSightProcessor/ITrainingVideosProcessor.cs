using System;
using System.Collections.Generic;

namespace PluralSightProcessor
{
    public interface ITrainingVideosProcessor
    {
        IList<PluralSightProcessor.Domain.Library> GetLibraryList();
    }
}
