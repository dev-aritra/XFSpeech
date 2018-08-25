using System;
using Xamarin.Forms;

namespace XFSpeechDemo
{
    public partial class MainPage : ContentPage
    {
        private ISpeechToText _speechRecongnitionInstance;
        public MainPage()
        {
            InitializeComponent();

            _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            stop.IsEnabled = false;

            MessagingCenter.Subscribe<ISpeechToText, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

            MessagingCenter.Subscribe<ISpeechToText>(this, "Final", (sender) =>
            {
                start.IsEnabled = true;
                stop.IsEnabled = false;
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });
        }

        private void SpeechToTextFinalResultRecieved(string args)
        {
            recon.Text = args;
        }

        private void Start_Clicked(object sender, EventArgs e)
        {
            _speechRecongnitionInstance.StartSpeechToText();
            if (Device.RuntimePlatform == Device.iOS)
            {
                start.IsEnabled = false;
            }

            stop.IsEnabled = true;

        }

        private void Stop_Clicked(object sender, EventArgs e)
        {
            start.IsEnabled = true;
            stop.IsEnabled = false;
            _speechRecongnitionInstance.StopSpeechToText();
        }
    }
}
