﻿using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PluralSight.ViewModel
{
    public class CourseViewModel : SelectableItem
    {
        private Course course;

        public CourseViewModel(Course course)
        {
            Statistics.Singlton.NumberOfCourses++;
            this.course = course;
            course.Chapters.CollectionChanged += Courses_CollectionChanged;
            lock (course)
            {
                foreach (var item in course.Chapters)
                {
                    chapters.Add(new ChapterViewModel(item as Chapter));
                }
            }
        }

        public String Name {
            get { return course.Name; }
            set { course.Name = value; }
        }

        private ObservableCollection<ChapterViewModel> chapters = new ObservableCollection<ChapterViewModel>();

        public ObservableCollection<ChapterViewModel> Chapters
        {
            get { return chapters; }
            set { chapters = value; }
        }

        public CourseViewModel()
        {
            course.Chapters.CollectionChanged += Courses_CollectionChanged;
        }

        void Courses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            lock (course)
            {
                foreach (var item in e.NewItems)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(
                         System.Windows.Threading.DispatcherPriority.Normal,
                         (Action)delegate()
                         {
                             chapters.Add(new ChapterViewModel(item as Chapter));
                         });
                }
            }
        }

        public override List<SelectableItem> GetChildren()
        {
            return chapters.ToList<SelectableItem>();
        }
    }
}
