using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSight.ViewModel
{
    public class Statistics : NotifyPropertyChangedBase
    {
        private static Statistics _singlton = new Statistics();

        public static Statistics Singlton{
            get{
                 return _singlton;
            }
        }

        private int numberOfLibraries;
        private int numberOfCourses;
        private int numberOfChapters;
        private int numberOfVideos;


        public int NumberOfLibraries { get { return numberOfLibraries; } set { numberOfLibraries = value; OnPropertyChanged(() => NumberOfLibraries); } }
        public int NumberOfCourses { get { return numberOfCourses; } set { numberOfCourses = value; OnPropertyChanged(() => NumberOfCourses); } }
        public int NumberOfChapters { get { return numberOfChapters; } set { numberOfChapters = value; OnPropertyChanged(() => NumberOfChapters); } }
        public int NumberOfVideos { get { return numberOfVideos; } set { numberOfVideos = value; OnPropertyChanged(() => NumberOfVideos); } }
    }
}
