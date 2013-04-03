using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PluralSightProcessor; 
using PluralSightProcessor.Domain;
using PluralSight.ViewModel;

namespace PluralSight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LibraryTreeViewModel namedItemModel = new LibraryTreeViewModel();
        IList<LibraryViewModel> libraryViewModelList;
        public Statistics Statistics {
            get{ return Statistics.Singlton;}
        }

        public IList<Library> LibraryList { get; set; }

        public MainWindow()
        {
            libraryViewModelList = namedItemModel.libraryViewModelList;
            LibraryList = namedItemModel.libraryList;

            this.DataContext = libraryViewModelList;

            InitializeComponent();
        }

        private void SaveDictionary_Click(object sender, RoutedEventArgs e)
        {
            namedItemModel.SaveDictionary(@"LibraryList.xml");
        }
    }
}