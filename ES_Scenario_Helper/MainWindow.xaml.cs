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

namespace ES_Scenario_Helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private String GenerateAcPosition(String Lat, String Lon, String Alt, String GndSpd, String Hdg, Random rand)
        {
            //array for a bunch of callsigns
            string[] callsigns = //13 (from the 0th index)
            {
                "AAL",
                "UAL",
                "HAL", 
                "NKS",
                "FFT",
                "DAL",
                "UPS",
                "FDX",
                "JBU",
                "EXJ",
                "ASA",
                "AAY",
                "DLH",
                "BAW"
            };
            //rand num for selecting callsign
            int callsign = rand.Next(0, 13);

            //rand num for callsign num
            int callsignNum = rand.Next(1, 9999);

            //rand num for squawk
            int squawk = rand.Next(1000, 7000);

            String Position = "@N:" + callsigns[callsign] + callsignNum + ":" + squawk + ":1:";

            Position = Position + Lat + ":" + Lon + ":" + Alt + ":" + GndSpd + ":0:" + Hdg + ":0";

            return Position;
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            Result.Text = "";

            Random rand = new Random();

            for (int i = 0; i < Int16.Parse(NumAc.Text); i++)
            {
                Result.Text += this.GenerateAcPosition(Lat.Text, Lon.Text, Alt.Text, GndSpd.Text, Hdg.Text, rand) + "\n";
            }
        }
    }
}
