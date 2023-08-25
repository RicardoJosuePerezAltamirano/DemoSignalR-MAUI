using Microsoft.AspNetCore.SignalR.Client;

namespace MauiSignalR;

public partial class MainPage : ContentPage
{
    private readonly HubConnection _connection;

    public MainPage()
    {
        InitializeComponent();

        _connection = new HubConnectionBuilder()
            .WithUrl("http://192.168.0.16:5061/chat")
            .Build();

        _connection.On<string>("MessageReceived", (message) =>
        {
            Dispatcher.Dispatch(() =>
            {
                Result.Text += $"{message} - ";
            });
            
        });
        _connection.On<string>("ReceiveMessage", (message) =>
        {
            Dispatcher.Dispatch(() =>
            {
                Result.Text += $"{message} - ";
            });

        });

        Task.Run(() =>
        {
            Dispatcher.Dispatch(async () =>
            await _connection.StartAsync());
        });
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
       

        myChatMessage.Text = String.Empty;
    }

    private void myChatMessage_Completed(object sender, EventArgs e)
    {

    }

    private async void myChatMessage_TextChanged(object sender, TextChangedEventArgs e)
    {
        var ToSend = e.NewTextValue;
        await _connection.InvokeCoreAsync("SendPrivateMessage", args: new[] { Grupo.Text,ToSend });

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _connection.InvokeCoreAsync("JoinRoom", args: new[] { Grupo.Text });
    }
}

