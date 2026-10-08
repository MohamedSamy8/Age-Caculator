using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Age_Calculator
{
    public partial class Form1 : Form
    {

        DateTime BirthDate, MinBirthDate = new DateTime(1900, 1, 1);
        TimeSpan CalculatedAgeTimeSpan;
        byte CaculatedAge_Years, CaculatedAge_Months, CaculatedAge_Days;
        public Form1()
        {
            InitializeComponent();
        }
        private bool IsBirthDateEmpty()
        {
            return (string.IsNullOrWhiteSpace(txtBirthDate_Day.Text)
                || string.IsNullOrWhiteSpace(txtBirthDate_Month.Text)
                || string.IsNullOrWhiteSpace(txtBirthDate_Year.Text)
                );
        }
        private string GetBirthDateString()
        {
            return txtBirthDate_Year.Text + "/" + txtBirthDate_Month.Text + "/" + txtBirthDate_Day.Text;
        }
        private bool IsValidDate()
        {
            return DateTime.TryParse(GetBirthDateString(), out BirthDate);
        }
        private bool IsValidBirthDate()
        {
            return (BirthDate <= DateTime.Now && BirthDate >= MinBirthDate);
        }
        private void ReturnFocusToDayTextBox()
        {
            txtBirthDate_Day.Focus();
        }
        private void ShowBirthDateEmptyMessage()
        {
            MessageBox.Show("Birth Date Should NOT be Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ReturnFocusToDayTextBox();
        }
        private void ClearTextBox(TextBox txtbox)
        {
            txtbox.Clear();
        }
        private void ClearBirthDateTextBoxes()
        {
            ClearTextBox(txtBirthDate_Day);
            ClearTextBox(txtBirthDate_Month);
            ClearTextBox(txtBirthDate_Year);
        }
        private void ClearCaculatedAge()
        {
            ClearTextBox(txtYourAge_Days);
            ClearTextBox(txtYourAge_Months);
            ClearTextBox(txtYourAge_Years);
            ClearTextBox(txtResult);
        }
        private void ClearTimeLived()
        {
            ClearTextBox(txtTotalDays);
            ClearTextBox(txtTotalHours);
            ClearTextBox(txtTotalMinutes);
            ClearTextBox(txtTotalSeconds);
            ClearTextBox(txtTotalMonths);
            ClearTextBox(txtTotalWeeks);
        }
        private void ClearMoreDetails()
        {
            ClearTextBox(txtNextBirthDay);
            ClearTextBox(txtBornOn);
        }
        private void ClearResults()
        {
            ClearCaculatedAge();
            ClearTimeLived();
            ClearMoreDetails();
        }
        private void ClearAllTextBoxes()
        {
            ClearBirthDateTextBoxes();
            ClearCaculatedAge();
            ClearTimeLived();
            ClearMoreDetails();
        }
        private void ShowInvalidDateMessage()
        {
            ClearBirthDateTextBoxes();
            MessageBox.Show("Date Is NOT Valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ReturnFocusToDayTextBox();

        }
        private void ShowInvalidBirthDateMessage()
        {
            ClearBirthDateTextBoxes();
            MessageBox.Show("Birth Date Is NOT Valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ReturnFocusToDayTextBox();
        }
        private bool ValidateBirthDate()
        {
            if (IsBirthDateEmpty())
            {
                ShowBirthDateEmptyMessage();
                return false;
            }
            else if (!IsValidDate())
            {
                ShowInvalidDateMessage();
                return false;
            }
            else if (!IsValidBirthDate())
            {
                ShowInvalidBirthDateMessage();
                return false;
            }
            return true;
        }
        private void FillCaculatedAgeResults()
        {
            txtYourAge_Days.Text = CaculatedAge_Days.ToString();
            txtYourAge_Months.Text = CaculatedAge_Months.ToString();
            txtYourAge_Years.Text = CaculatedAge_Years.ToString();

            txtResult.Text += "You are " + txtYourAge_Years.Text + " Years ";
            txtResult.Text += txtYourAge_Months.Text + " Months ";
            txtResult.Text += txtYourAge_Days.Text + " Days Old.";
        }
        private void FillTimeLivedResults()
        {
            long  TotalMonths,TotalWeeks, TotalDays, TotalHours, TotalMinutes, TotalSeconds;

            TotalMonths = (CaculatedAge_Years * 12) + CaculatedAge_Months;
            TotalWeeks = Convert.ToInt64(CalculatedAgeTimeSpan.TotalDays / 7);
            TotalHours = Convert.ToInt64(CalculatedAgeTimeSpan.TotalHours);
            TotalMinutes = Convert.ToInt64(CalculatedAgeTimeSpan.TotalMinutes);
            TotalSeconds  = Convert.ToInt64(CalculatedAgeTimeSpan.TotalSeconds);
            TotalDays = Convert.ToInt64(CalculatedAgeTimeSpan.TotalDays);

            txtTotalMonths.Text = TotalMonths.ToString();
            txtTotalWeeks .Text = TotalWeeks.ToString();
            txtTotalDays.Text = TotalDays.ToString();
            txtTotalHours.Text = TotalHours.ToString();
            txtTotalMinutes.Text = TotalMinutes.ToString();
            txtTotalSeconds.Text = TotalSeconds.ToString();
        }
        private void CaculateAndFillNextBirthDay()
        {
            DateTime NextBirthDay = new DateTime(DateTime.Now.Year,BirthDate.Month,BirthDate.Day);

            if(DateTime.Now >  NextBirthDay)
            {
                NextBirthDay = NextBirthDay.AddYears(1);
            }

            txtNextBirthDay.Text= (NextBirthDay- DateTime.Now).Days.ToString();
        }
        private void FillMoreDetailsResults()
        {
            txtBornOn.Text = BirthDate.DayOfWeek.ToString();
            CaculateAndFillNextBirthDay();
        }
        private void FillResults()
        {
            FillCaculatedAgeResults();
            FillTimeLivedResults();
            FillMoreDetailsResults();
        
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes();
            ReturnFocusToDayTextBox();
        }

        private void CalculateResults()
        {
            CalculatedAgeTimeSpan = DateTime.Now - BirthDate;

            DateTime TempAge = new DateTime();
            TempAge += CalculatedAgeTimeSpan;

            CaculatedAge_Years = Convert.ToByte(TempAge.Year);
            CaculatedAge_Months = Convert.ToByte(TempAge.Month);
            CaculatedAge_Days = Convert.ToByte(TempAge.Day);

            CaculatedAge_Years--;
            CaculatedAge_Months--;
            CaculatedAge_Days--;
        }
        private void CaculateAndFillResults()
        {
            CalculateResults();
            FillResults();
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            ClearResults();

            if (ValidateBirthDate())
            {
                CaculateAndFillResults();
            }
        }
    }
}
