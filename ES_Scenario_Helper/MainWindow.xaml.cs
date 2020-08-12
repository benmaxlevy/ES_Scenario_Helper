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
        
        //helpers

        private string[] GenerateAcPosition(String lat, String lon, String alt, String gndSpd, String hdg, Random rand)
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

            string position = "@N:" + callsigns[callsign] + callsignNum + ":" + squawk + ":1:";

            position = position + lat + ":" + lon + ":" + alt + ":" + gndSpd + ":0:" + hdg + ":0";

            string[] ac =
            {
                callsigns[callsign] + callsignNum,
                position
            };

            return ac;
        }

        private string GenerateAcFlightPlan(string callsign, Random rand)
        {

            string[] aircrafts = //up to index of 7
            {
                "A318",
                "A321",
                "B712",
                "B737",
                "B739",
                "E195",
                "B752",
                "A333",
                "B744"
            };

            int aircraftNum = rand.Next(0, 7);

            string result = "$FP" + callsign + ":*A:I:" + aircrafts[aircraftNum] + ":300:" + Dept.Text + ":0000:0000:0:" + Arrival.Text + ":0:0:0:0::/v/:" + Route.Text;

            return result;
        }

        //"click" methods
        private void CopyResult(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.SetText(Result.Text);
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            Result.Text = "";

            Random rand = new Random();

            for (int i = 0; i < Int16.Parse(NumAc.Text); i++)
            {
                string[] position = this.GenerateAcPosition(Lat.Text, Lon.Text, Alt.Text, GndSpd.Text, Hdg.Text, rand);
                Result.Text += position[1] + "\n" + this.GenerateAcFlightPlan(position[0], rand) + "\n \n";
            }
        }
    }
}
