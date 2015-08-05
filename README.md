![pushetta.com](http://www.pushetta.com/static/site/img/logo_100.png) 

# Microsoft .Net client for Pushetta

PushettaClient is the official client for sending and receiving notification with Pushetta service (http://www.pushetta.com).
This client currently works on Windows 10 (Univesal App) and Framework 4.5 CLR applications.

## Installation

Clone the repository and link the project for the used framework.
NuGet package will be avaiable soon on official nuget directory.

## Usage

Sending notifications it's really simple, all You need it's an Api Key, taken from Pushetta's Dashboard and a Channel. 
The latter can be created here http://www.pushetta.com/my/channels/create/


```csharp
	IPushettaSender pushetta = new PushettaSender(new PushettaConfig()
	{
                APIKey = "APY_KEY"
    });
	pushetta.SendMessage("CHANNEL_NAME", new PushMessage("Hello Pushetta World!"));
```

Receiving notifications it's almost simple as sending it.


```csharp
	receiver = new PushettaReceiver(new PushettaConfig()
	{
			APIKey = "APY_KEY"
    });

	receiver.OnMessage += OnMessageReceived;
	receiver.SubscribeChannel(txtChannel.Text);

	private void OnMessageReceived(object sender, MessageEventArgs e)
	{
		// In e.Message there is paypload of notification
    
	}
```