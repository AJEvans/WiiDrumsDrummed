using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using WiimoteLib;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    

    public partial class Form1 : Form
    {

        // create a new instance of the Wiimote
        public Wiimote wm = new Wiimote();
        

		// We don't really need the console, but there's some debugging code you can turn on 
		// below.
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)] 
        public static extern bool AllocConsole();

        
		// Again, don't use this code, but if you want to try and connect to 
		// software that takes in key presses, you can use this. Doesn't really work 
		// as most software needs to be selected to pick up key presses, and so does this.
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        const int VK_a = 0x41;
        const int VK_b = 0x42;
        const int VK_c = 0x43;
        const int VK_d = 0x44;
        const int VK_e = 0x45;
        const int VK_f = 0x46;
		
		
		// Change your sound file names here.
        SoundPlayer red = new SoundPlayer(Properties.Resources.CYCdh_K5_Snr04);
        SoundPlayer blue = new SoundPlayer(Properties.Resources.CYCdh_K5_Kick93);
        SoundPlayer green = new SoundPlayer(Properties.Resources.CYCdh_K5_Tom02c);
        SoundPlayer orange = new SoundPlayer(Properties.Resources.CYCdh_K5_OpHat01);
        SoundPlayer yellow = new SoundPlayer(Properties.Resources.CYCdh_K5_ClHat03);

        public Form1()
        {
			
			// Get a console interface.
            AllocConsole();

			// All the rest of this is from the wiimotelib examples.
			
            InitializeComponent();
            // setup the event to handle state changes
            wm.WiimoteChanged += wm_WiimoteChanged;

            // setup the event to handle insertion/removal of extensions
            wm.WiimoteExtensionChanged += wm_WiimoteExtensionChanged;

            // connect to the Wiimote
            wm.Connect();

            // set the report type to return the IR sensor and accelerometer data (buttons always come back)
            // set the report type to return the IR sensor and accelerometer data (buttons always come back)
            wm.SetReportType(InputReport.ButtonsExtension, true);

            //wm.SetReportType(Wiimote.InputReport.IRAccel, true);
            if (wm.WiimoteState.ExtensionType == ExtensionType.Drums)
                Console.WriteLine("Drums");
   



        }

    



        void wm_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs args)
        {
            if (args.Inserted)
                wm.SetReportType(InputReport.IRExtensionAccel, true);    // return extension data
            else
                wm.SetReportType(InputReport.IRAccel, true);            // back to original mode
        }



        void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs args)
        {
            // current state information
            WiimoteState ws = args.WiimoteState;


			// If you want to try either the comments to the console or the key presses, you 
			// can uncomment the relevant parts of the code, below.
			
            if (ws.DrumsState.Blue)
            {
                // Console.WriteLine("Blue");
                // pressKey(VK_a);
                
                blue.Play();

            }
            if (ws.DrumsState.Red)
            {
                // Console.WriteLine("Red");
                //pressKey(VK_b);

                red.Play();
            }
            if (ws.DrumsState.Green)
            {
                //Console.WriteLine("Green");
                

                green.Play();
                //pressKey(VK_c);
            }
            if (ws.DrumsState.Orange)
            {
                //Console.WriteLine("Orange");

                orange.Play();
                // pressKey(VK_d);
            }
            if (ws.DrumsState.Yellow)
            {
                //Console.WriteLine("Yellow");

                yellow.Play();
                // pressKey(VK_e);
            }
        }


        void pressKey(int key)
        {

            const uint KEYEVENTF_KEYUP = 0x0002;
            const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }

    }

}