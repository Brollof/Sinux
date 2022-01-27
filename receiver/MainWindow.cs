using System;
using System.Globalization;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Sinux
{
    public partial class FormSinux : Form
    {
        private float tempThreshold = 0.0f;
        private float currentTemperature = 0.0f;
        private const int SOUND_REPEAT_NUM = 2;
        private Socket clientSocket = null;
        private FormTempChart formTempChart = null;

        public FormSinux()
        {
            InitializeComponent();
            CenterToScreen();

            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add("Temperature chart", ShowTemperatureChart);
            menu.MenuItems.Add("Exit", ContextMenuExit);
            notifyIcon1.ContextMenu = menu;
 
            new Thread(() => RunServer()).Start();
            new Thread(() => NotifyUser()).Start();
        }

        private void RunServer()
        {
            bool socketInitialized = false;
            IPAddress ipAddr = IPAddress.Parse("192.168.50.164");
            Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(ipAddr, 60000));
            listener.Listen(5);

            byte[] data = new byte[1024];
            string msg;

            while (true)
            {
                if (socketInitialized == false)
                {
                    Console.WriteLine("Waiting for a connection...");
                    clientSocket = listener.Accept();
                    Console.WriteLine("Connected");
                    socketInitialized = true;
                }
                else
                {
                    msg = null;

                    while (true)
                    {
                        try
                        {
                            int numByte = clientSocket.Receive(data);
                            msg += Encoding.ASCII.GetString(data, 0, numByte);
                        }
                        catch (SocketException e)
                        {
                            socketInitialized = false;
                            Console.WriteLine("Error: Socket Exception.{0},{1}.", e.Message, e.ErrorCode);
                            break;
                        }

                        if (msg.IndexOf("<EOF>") > -1)
                        {
                            msg = msg.Replace("<EOF>", "");
                            break;
                        }
                    }

                    Console.WriteLine("Raw data: {0} ", msg);

                    if (ParseFloat(msg, out currentTemperature) == true)
                    {
                        if (currentTemperature > 0)
                        {
                            labCurrentTemp.Invoke(new Action(() => labCurrentTemp.Text = msg + "°C"));
                            WriteTemperature(currentTemperature.ToString());
                        }
                    }
                }
                Thread.Sleep(50);
            }
        }

        private void NotifyUser()
        {
            while (true)
            {
                if (currentTemperature > 0 && currentTemperature <= tempThreshold)
                {
                    notifyIcon1.ShowBalloonTip(10000, "Ready!", "Your water is ready", ToolTipIcon.Info);
                    using (var soundPlayer = new SoundPlayer(@"c:\Windows\Media\Alarm05.wav"))
                    {
                        for (int i = 0; i < SOUND_REPEAT_NUM; i++)
                        {
                            soundPlayer.PlaySync();
                            Thread.Sleep(500);
                        }
                    }
                    return;
                }
                Thread.Sleep(100);
            }
        }

        private void FormSinux_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnExit();
        }

        private void FormSinux_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void ContextMenuExit(object sender, EventArgs e)
        {
            OnExit();
        }

        private void TxtLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                if (ParseFloat(txtLimit.Text, out float temp) == true)
                {
                    e.Handled = true;
                    tempThreshold = temp;
                    labLimit.Text = tempThreshold.ToString() + "°C";
                }
            }
        }

        private bool ParseFloat(string s, out float result)
        {
            return float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
        }

        private void OnExit()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
            }

            if (formTempChart != null)
            {
                formTempChart.Close();
                formTempChart.Dispose();
            }

            notifyIcon1.Visible = false;
            notifyIcon1.Dispose();
            Application.Exit();
            Environment.Exit(0);
        }

        private void WriteTemperature(string temp)
        {
            string now = DateTime.Now.TimeOfDay.ToString();
            Logfile.Instance.Write(String.Format("{0}; {1}", now, temp));
        }

        void ShowTemperatureChart(object sender, EventArgs e)
        {
            if (formTempChart != null)
            {
                formTempChart.Close();
                formTempChart.Dispose();
            }

            formTempChart = new FormTempChart();
            formTempChart.Show();
        }
    }
}
