using System;
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
        private string chartSource = null;

        public FormTempChart()
        {
            InitializeComponent();
            InitChart();

            chartTimer.Start();

            openFileDialog.InitialDirectory = Logfile.Instance.GetLogDir();
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                chartSource = openFileDialog.FileName;
                FillChart(chartSource);
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
            if (string.IsNullOrEmpty(filepath) == true)
            {
                return;
            }

            Console.WriteLine("Filling chart...");
            List<DateTime> xs = new List<DateTime>();
            List<float> ys = new List<float>();
            string[] data;

            if (filepath.ToLower() == Logfile.Instance.GetLogFile().ToLower())
            {
                // If chosen file is the same as current log file,
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

            chartTemp.Series[0].Points.Clear();
            chartTemp.Series[0].Points.DataBindXY(xs, ys);
        }

        private void chartTimer_Tick(object sender, EventArgs e)
        {
            FillChart(chartSource);
        }
    }
}
