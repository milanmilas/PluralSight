using System;
using System.Collections.Generic;

namespace PluralSightProcessor
{
    using PluralSightProcessor.Domain;

    public interface ITrainingVideosProcessor
    {
        IList<Library> GetLibraryList();

        IList<Library> GetLibraryList(Uri libraryUri);
    }
}
