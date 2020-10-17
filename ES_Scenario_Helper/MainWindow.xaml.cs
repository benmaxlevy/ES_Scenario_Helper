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

    public struct Airline
    {
        public string Icao;
        public string[] Aircraft;

        public Airline(string icao, string[] aircraft)
        {
            Icao = icao;
            Aircraft = aircraft;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        //helpers

        private object[] GenerateAcPosition(String lat, String lon, String alt, String gndSpd, String hdg, Random rand, Airline[] airlines, int[] squawks)
        {
            //rand num for selecting callsign
            int callsign = rand.Next(0, airlines.Length);

            //rand num for callsign num
            int callsignNum = rand.Next(1, 9999);

            //rand num for squawk
            int squawk = rand.Next(Int16.Parse(StartSquawk.Text), Int16.Parse(EndSquawk.Text));

            if(squawk.ToString().Contains('8'))
            {
                squawk = Int16.Parse(squawk.ToString().Replace('8', '2'));
            } else if(squawk.ToString().Contains('9'))
            {
                squawk = Int16.Parse(squawk.ToString().Replace('9', '3'));
            }

            while (squawks.Contains(squawk))
            {
                squawk += 1;
                if (squawk.ToString().Contains('8'))
                {
                    squawk = Int16.Parse(squawk.ToString().Replace('8', '2'));
                }
                else if (squawk.ToString().Contains('9'))
                {
                    squawk = Int16.Parse(squawk.ToString().Replace('9', '3'));
                }
            }

            string position = "@N:" + airlines[callsign].Icao + callsignNum + ":" + squawk + ":1:";

            position = position + lat + ":" + lon + ":" + alt + ":" + gndSpd + ":0:" + hdg + ":0";

            object[] ac =
            new object[] {
                airlines[callsign].Icao + callsignNum,
                position,
                airlines[callsign].Aircraft,
                squawk
            };

            return ac;
        }

        private string GenerateAcRoute(int acGenerated)
        {
            string result = "$ROUTE:" + Route.Text + "\n" + "START:" + Int16.Parse(BetweenAc.Text)*acGenerated + "\nDELAY:2:5\nREQALT:" + Waypoint.Text + ":" + WaypointCross.Text + "\n";
            return result;
        }

        private string GenerateAcFlightPlan(object callsign, Random rand, string[] airlineAircrafts)
        {
            int aircraftNum = rand.Next(0, airlineAircrafts.Length);

            string result = "$FP" + callsign + ":*A:I:" + airlineAircrafts[aircraftNum] + ":300:" + Dept.Text + ":0000:0000:" + FlightPlanAlt.Text + ":" + Arrival.Text + ":0:0:0:0::/v/:" + FlightPlanRoute.Text;

            return result;
        }

        //"click" methods
        private void CopyResult(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.SetText(Result.Text);
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            //airline shit
            string[] aalAircraft =
            {
                "A319",
                "A321",
                "A320",
                "B737",
                "A332",
                "B772",
            };

            string[] ualAircraft =
            {
                "A319",
                "A320",
                "B737",
                "B738",
                "B739",
                "B752",
                "B772"  
            };

            string[] nksAircraft =
            {
                "A319",
                "A320",
                "A321"
            };

            string[] fftAircraft =
            {
                "A320",
                "A321"
            };

            string[] dalAircraft =
            {
                "A319",
                "A320",
                "A321",
                "A332",
                "B712",
                "B737",
                "B738",
                "B752"
            };

            string[] upsAircraft =
            {
                "A306",
                "B744",
                "B752",
                "B763",
                "MD11"
            };

            string[] fdxAircraft =
            {
                "A306",
                "B752",
                "B763",
                "B772",
                "MD11"
            };

            string[] ejaAircraft =
            {
                "LJ35",
                "LJ45"
            };

            string[] capAircraft =
            {
                "C172",
                "C182",
                "C152"
            };

            Airline[] airlines =
            {
                new Airline("AAL", aalAircraft),
                new Airline("UAL", ualAircraft),
                new Airline("NKS", nksAircraft), 
                new Airline("FFT", fftAircraft), 
                new Airline("DAL", dalAircraft), 
                new Airline("UPS", upsAircraft), 
                new Airline("FDX", fdxAircraft), 
                new Airline("EJA", ejaAircraft),
                //new Airline("CAP", capAircraft)
            };
            //end airline shit
            
            Result.Text = "";

            Random rand = new Random();

            List<int> squawks = new List<int>();

            for (int i = 0; i < Int16.Parse(NumAc.Text); i++)
            {
                object[] position = this.GenerateAcPosition(Lat.Text, Lon.Text, Alt.Text, GndSpd.Text, Hdg.Text, rand, airlines, squawks.ToArray());
                squawks.Add((int)position[3]);
                Result.Text += position[1] + "\n" + this.GenerateAcFlightPlan(position[0], rand, (string[])position[2]) + "\n" + this.GenerateAcRoute(i) + "\n";
            }
        }
    }
}
