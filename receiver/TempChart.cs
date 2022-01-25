﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sinux
{
    public partial class FormTempChart : Form
    {
        private readonly Thread fillThread = null;

        public FormTempChart()
        {
            InitializeComponent();
            InitChart();

            openFileDialog.InitialDirectory = Logfile.Instance.GetLogDir();
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fillThread = new Thread(() => FillChart(openFileDialog.FileName));
                fillThread.Start();
            }
        }

        private void InitChart()
        {
            Series series = new Series();
            chartTemp.Series.Add(series);
            chartTemp.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";

            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 2;
            series.Color = Color.Green;
            series.XValueType = ChartValueType.Time;
            series.YValueType = ChartValueType.Double;
            series.IsVisibleInLegend = false;
            //series.LegendText = "Temperature";
        }

        private void FillChart(string filepath)
        {
            Console.WriteLine("Fill chart thread started!");
            while (true)
            {
                List<DateTime> xs = new List<DateTime>();
                List<float> ys = new List<float>();
                string[] data = null;

                if (filepath.ToLower() == Logfile.Instance.GetLogFile().ToLower())
                {
                    // If same file as log file has been chosen,
                    // we need to operate thread-safe.
                    data = Logfile.Instance.ReadLines();
                }
                else
                {
                    data = File.ReadAllLines(filepath);
                }


                foreach (string line in data)
                {
                    string[] result = Regex.Split(line, "; ");
                    if (result.Length == 2)
                    {
                        bool timeResult = DateTime.TryParse(result[0], out DateTime time);
                        bool tempResult = float.TryParse(result[1], out float temp);
                        if (timeResult == true && tempResult == true)
                        {
                            xs.Add(time);
                            ys.Add(temp);
                        }
                    }
                }

                chartTemp.Invoke(new Action(() => {
                    chartTemp.Series[0].Points.Clear();
                    chartTemp.Series[0].Points.DataBindXY(xs, ys);
                }));
                Thread.Sleep(1000);
            }
        }

        private void FormTempChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (fillThread != null)
            {
                fillThread.Abort();
                fillThread.Join();
            }
        }
    }
}
