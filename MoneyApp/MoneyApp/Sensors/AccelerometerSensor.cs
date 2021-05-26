using MoneyApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoneyApp.Sensors
{
    public static class AccelerometerSensor
    {
        static SensorSpeed speed = SensorSpeed.UI;
        static float sensor_edge = 2;
        public static string LastPage = "";

        static AccelerometerSensor()
        {
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        static void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            if ((data.Acceleration.X > sensor_edge) && (LastPage != nameof(SendEmailPage)))     
                Shell.Current.GoToAsync($"{nameof(SendEmailPage)}");
        }

        public static void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch
            {
                // Feature not supported on device
            }
        }
    }
}
