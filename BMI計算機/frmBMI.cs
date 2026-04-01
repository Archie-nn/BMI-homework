using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BMI計算機
{
    public partial class frmBMI : Form
    {
        string[] strResultList = { "體重過輕", "健康體位", "體位過重", "輕度肥胖", "中度肥胖", "重度肥胖" };
        Color[] colorList = { Color.Blue, Color.Green, Color.Orange, Color.DarkOrange, Color.Red, Color.Purple };
        public frmBMI()
        {
            InitializeComponent();
        }
        // 在 frmBMI 類別內新增此方法
        private void ApplyModernStyle(Control ctrl, int radius = 10)
        {
            // 讓控制項邊角變圓滑
            ctrl.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, ctrl.Width, ctrl.Height, radius, radius));
        }

        // 需要引用 System.Runtime.InteropServices 以調用 Windows API 畫圓角
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void SetResultUI(int index, double bmi)
        {
            Color backColor = colorList[index];
            lblResult.Text = $"{bmi:F2} ({strResultList[index]})";

            // 平滑變色效果 (選用)
            lblResult.BackColor = backColor;

            // 自動判斷文字顏色：計算亮度 (Brightness)
            // 亮度公式: (R*299 + G*587 + B*114) / 1000
            double brightness = (backColor.R * 299 + backColor.G * 587 + backColor.B * 114) / 1000;
            lblResult.ForeColor = brightness < 128 ? Color.White : Color.Black;

            // 調用上面寫的圓角 function
            ApplyModernStyle(lblResult, 20);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            bool isHeightValid = double.TryParse(txtHeight.Text, out double height);
            bool isWeightValid = double.TryParse(txtWeight.Text, out double weight);
            // 驗證身高輸入
            if (isHeightValid)
            {
                if (height <= 0)
                {
                    MessageBox.Show("身高必須大於零。", "身高值錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("請輸入有效的身高數值。", "身高值錯誤",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 驗證體重輸入
            if (isWeightValid)
            {
                if (weight <= 0)
                {
                    MessageBox.Show("體重必須大於零。", "體重值錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("請輸入有效的體重數值。", "體重值錯誤",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double h = double.Parse(txtHeight.Text);
            double w = double.Parse(txtWeight.Text);
            h /= 100; //公分轉為公尺單位
            double bmi = w / Math.Pow(h, 2);
            //string strResult = "";
            Color colorResult = Color.Black;
            int resultIndex = 0;
            if (bmi < 18.5)
            {
                resultIndex = 0;
            }
            else if (bmi < 24)
            {
                resultIndex = 1;
            }
            else if (bmi < 27)
            {
                resultIndex = 2;
            }
            else if (bmi < 30)
            {
                resultIndex = 3;
            }
            else if (bmi < 35)
            {
                resultIndex = 4;
            }
            else
            {
                resultIndex = 5;
            }
            //strResult = strResultList[resultIndex];
            //colorResult = colorList[resultIndex];
            SetResultUI(resultIndex, bmi);
            //lblResult.Text = $"{bmi:F2} ({strResult})";
            //lblResult.BackColor = colorResult;
            //txtResult.Text = bmi.ToString();


        }
    }
}
