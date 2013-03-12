using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PluralSight.ViewModel
{
    public class ChapterViewModel : SelectableItem
    {
        private Chapter chapter;

        public ChapterViewModel(Chapter chapter)
        {
            // TODO: Complete member initialization
            this.chapter = chapter;
            chapter.Videos.CollectionChanged += Courses_CollectionChanged;
        }

         public String Name {
             get { return chapter.Name; }
             set { chapter.Name = value; }
        }

        private ObservableCollection<VideoViewModel> videos = new ObservableCollection<VideoViewModel>();

        public ObservableCollection<VideoViewModel> Videos
        {
            get { return videos; }
            set { videos = value; }
        }

        public ChapterViewModel()
        {
            chapter.Videos.CollectionChanged += Courses_CollectionChanged;
        }

        void Courses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems)
            {
                videos.Add(new VideoViewModel(item as Video));
            }
        }

        public override List<SelectableItem> GetChildren()
        {
            return videos.ToList<SelectableItem>();
        }
    }
}
