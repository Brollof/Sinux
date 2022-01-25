using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sinux
{
    public partial class formTempChart : Form
    {
        public formTempChart()
        {
            InitializeComponent();
            InitChart();

            openFileDialog.InitialDirectory = Logfile.Instance.GetLogDir();
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FillChart(openFileDialog.FileName);
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
            foreach (string line in File.ReadAllLines(filepath))
            {
                string[] result = Regex.Split(line, "; ");
                if (result.Length == 2)
                {
                    bool timeResult = DateTime.TryParse(result[0], out DateTime time);
                    bool tempResult = float.TryParse(result[1], out float temp);
                    if (timeResult == true && tempResult == true)
                    {
                        chartTemp.Series[0].Points.AddXY(time, temp);
                    }
                }
            }
        }
    }
}
