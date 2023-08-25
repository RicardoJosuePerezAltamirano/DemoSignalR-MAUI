using Microsoft.AspNetCore.SignalR.Client;

namespace WinFormsSignalR
{
    public partial class Form1 : Form
    {
        private readonly HubConnection _connection;
        public Form1()
        {
            InitializeComponent();
            _connection = new HubConnectionBuilder()
            .WithUrl("http://192.168.0.16:5061/chat")
            .Build();
            Task.Run(async () =>
            {
                await _connection.StartAsync();
                //Dispatcher.Dispatch(async () =>
                //);
            });

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await _connection.InvokeCoreAsync("JoinRoom", args: new[] { "sucursal 1" });
            _connection.On<string>("ReceiveMessage", (message) =>
            {
                //Dispatcher.Dispatch(() =>
                //{
                textBox1.Text += $"{message} - ";
                //});

            });
        }
    }
}