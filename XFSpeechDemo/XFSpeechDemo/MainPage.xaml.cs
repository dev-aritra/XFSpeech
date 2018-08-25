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
            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch(Exception ex)
            {
                recon.Text = ex.Message;
            }
            
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
            try
            {
                _speechRecongnitionInstance.StartSpeechToText();
            }
            catch(Exception ex)
            {
                recon.Text = ex.Message;
            }
            
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
