using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MoneyApp.View;
using MoneyApp.Data;
using MoneyApp.Sensors;
using System.Collections.Generic;

namespace MoneyApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<DataManager>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            AccelerometerSensor.ToggleAccelerometer();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            //AccelerometerSensor.ToggleAccelerometer();
        }
    }
}
