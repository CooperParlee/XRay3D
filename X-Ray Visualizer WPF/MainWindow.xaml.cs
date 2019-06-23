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

namespace X_Ray_Visualizer_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DICOMProcessing dicomProcessor;
        DisplayManagement display;

        public MainWindow()
        {
            InitializeComponent();
            this.display = new DisplayManagement(imageOriginalBox, imageProcessedBox);
            this.dicomProcessor = new DICOMProcessing(this.display);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dicomProcessor.FindDicomFile();
        }
    }
}
