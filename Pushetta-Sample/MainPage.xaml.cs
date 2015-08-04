using com.gumino.Pushetta;
using com.gumino.Pushetta.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pushetta_Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IPushettaReceiver receiver = null;

        public MainPage()
        {
            this.InitializeComponent();
         
        }

       
  
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            IPushettaSender pushetta = new PushettaSender(new PushettaConfig()
            {
                APIKey = txtAPIKey.Text
            });

            try
            {
                pushetta.SendMessage(txtChannel.Text,
                    new PushMessage(txtMessage.Text));
            }
            catch (PushettaException pex)
            {
                errorBlock.Visibility = Visibility.Visible;
                txtError.Text = pex.Message;
            }
        }

        private async void Receiver_OnMessage(object sender, MessageEventArgs e)
        {
             await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                 Windows.UI.Core.CoreDispatcherPriority.Normal,
                 () =>
                 {
                     lblMessageReceived.Visibility = Visibility.Visible;
                     lblMessageReceived.Text = Encoding.UTF8.GetString(e.Message);
                 });


        }

        private void btnReceive_Click(object sender, RoutedEventArgs e)
        {
            if (receiver == null)
            {

                try
                {
                    receiver = new PushettaReceiver(new PushettaConfig()
                    {
                        APIKey = txtAPIKey.Text
                    });

                    receiver.OnMessage += Receiver_OnMessage;
                    receiver.SubscribeChannel(txtChannel.Text);

                    lblMessageReceived.Visibility = Visibility.Visible;
                    lblMessageReceived.Text = "WAITING FOR MESSAGES....";
                    
                }
                catch (PushettaException pex)
                {
                    errorBlock.Visibility = Visibility.Visible;
                    txtError.Text = pex.Message;
                }
            }
        }
    }
}
