using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MaskedDateTextBoxDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // The anniversary text box is for display only:
            this.mtbAnniversary.Enabled = false;
            this.mtbEnterDate.Leave += new EventHandler(mtbEnterDate_Leave);

            // ++++ Uncomment the following two lines to see the result if valid date values are not required:  -->>>>>

            //this.mtbEnterDate.RequireValidEntry = false;
            //this.mtbEnterDate.InvalidDateEntered += new MaskedDateEntryControl.MaskedDateBox.InvalidDateEntryHandler(mtbEnterDate_InvalidDateEntered);
        }



        void mtbEnterDate_Leave(object sender, EventArgs e)
        {
            this.OnDateEntered();
        }


        private void OnDateEntered()
        {
            string hasAnniversary = "The Anniversary of {0} is {1}";
            string InvalidDate = "You have entered an invalide date";
            string output = "";

            var oldDate = mtbEnterDate.DateValue;
            DateTime newDate;
            

            if (oldDate.HasValue)
            { 
                // Retrieve the actual value from the nullable DateTime object:
                newDate = oldDate.Value.AddYears(1);
                output = string.Format(hasAnniversary, oldDate.Value.ToShortDateString(), newDate.ToShortDateString());
                this.mtbAnniversary.DateValue = newDate;
            }
            else
            {
                // No value present means the date is invalid or null:
                output = InvalidDate;
                this.mtbAnniversary.DateValue = null;
            }

            // Show the results in the Richtextbox:
            this.rtbOutput.AppendText(output);

        }


        void mtbEnterDate_InvalidDateEntered(object sender, MaskedDateEntryControl.InvalidDateTextEventArgs e)
        {
            MessageBox.Show("We caught this validation issue in the client code - You have entered an invalid date value");
        }
    }
}
