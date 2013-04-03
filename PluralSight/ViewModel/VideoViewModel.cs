using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluralSight.ViewModel
{
    public class VideoViewModel : SelectableItem
    {
        public String Name
        {
            get { return video.Name; }
            set { video.Name = value; }
        }

        private Video video;

        public VideoViewModel(Video video)
        {
            Statistics.Singlton.NumberOfVideos++;
            // TODO: Complete member initialization
            this.video = video;
        }

        public override List<SelectableItem> GetChildren()
        {
            return null;
        }
    }
}
