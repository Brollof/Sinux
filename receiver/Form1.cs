using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sinux
{
    public partial class FormSinux : Form
    {
        private bool socketInitialized = false;
        private Socket clientSocket = null;
        private float tempThreshold = 0.0f;
        private bool threshold_achieved = false;

        public FormSinux()
        {
            InitializeComponent();
            CenterToScreen();

            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add("Open Temperature Log");
            menu.MenuItems.Add("Exit", ContextMenuExit);
            notifyIcon1.ContextMenu = menu;

            new Thread(() => RunServer()).Start();
        }

        private void RunServer()
        {
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
                            Console.WriteLine("Error: Socket Exception.{0}{1}.", e.Message, e.ErrorCode);
                            break;
                        }

                        if (msg.IndexOf("<EOF>") > -1)
                        {
                            msg = msg.Replace("<EOF>", "");
                            break;
                        }
                    }

                    Console.WriteLine("Text received: {0} ", msg);
                    label2.Text = msg + "°C";

                    // TODO: Move sound playing to the new thread
                    float temp = float.Parse(msg, CultureInfo.InvariantCulture);
                    if (threshold_achieved == false && temp <= tempThreshold)
                    {
                        notifyIcon1.ShowBalloonTip(10000, "Ready!", "Your water is ready", ToolTipIcon.Info);
                        using (var soundPlayer = new SoundPlayer(@"c:\Windows\Media\Alarm05.wav"))
                        {
                            soundPlayer.PlayLooping();
                        }
                        threshold_achieved = true;
                    }
                }
            }
        }

        private void FormSinux_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
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
            notifyIcon1.Visible = false;
            Application.Exit();
            Environment.Exit(0);
        }

        private void TxtLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                if (float.TryParse(txtLimit.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float temp) == true)
                {
                    e.Handled = true;
                    tempThreshold = temp;
                    labLimit.Text = tempThreshold.ToString() + "°C";
                }
            }
        }
    }
}
