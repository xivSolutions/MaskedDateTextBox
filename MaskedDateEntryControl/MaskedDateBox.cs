using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MaskedDateEntryControl
{
    public class MaskedDateTextBox : MaskedTextBox
    {

        // Set up custom event to handle invalid entry:
        public delegate void InvalidDateEntryHandler(object sender, InvalidDateTextEventArgs e);
        public event InvalidDateEntryHandler InvalidDateEntered;

        // Default setting is to require a valid date string before allowing 
        // the user to navigate away from the control:
        public bool _RequireValidEntry = true;

        // The default mask is traditional, USA-centric mm/dd/yyyy format. 
        private const string DEFAULT_MASK = "00/00/0000";
        private const char DEFAULT_PROMPT = '_';

        // A flag is set when control initialization is complete. This 
        // will be used to determine if the Mask property of the control
        // (inherited from the Base class) can be changed. 
        private bool _Initialized = false;


        public MaskedDateTextBox() : this(true) { }


        public MaskedDateTextBox(bool RequireValidEntry = true) : base()
        {
            
            // This is the only mask that will work in the current implementation:
            this.Mask = DEFAULT_MASK;
            this.PromptChar = DEFAULT_PROMPT;

            // Handle Events:
            this.Enter +=new EventHandler(MaskedDateTextBox_SelectAllOnEnter);
            this.PreviewKeyDown +=new PreviewKeyDownEventHandler(MaskedDateBox_PreviewKeyDown);

            // prevent further changes to the mask:
            _Initialized = true;
        }


        /// <summary>
        /// This control is based on text entry of dates in mm/dd/yyyy format. Other masks
        /// are not allowed at this time. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMaskChanged(EventArgs e)
        {
            if (_Initialized)
            {
                throw new NotImplementedException("The Mask is not chageable in this control");
            }
        }


        /// <summary>
        /// Adjust the text entry to maintain proper date string formatting. 
        /// </summary>
        /// <param name="dateTextBox"></param>
        void CorrectDateText(MaskedTextBox dateTextBox)
        {
            // Replace any odd date separators with the mm/dd/yyyy Standard:
            Regex rgx = new Regex(@"(\\|-|\.)");
            string FormattedDate = rgx.Replace(dateTextBox.Text, @"/");

            // Separate the date components as delimmited by standard mm/dd/yyyy formatting:
            string[] dateComponents = FormattedDate.Split('/');
            string month = dateComponents[0].Trim(); ;
            string day = dateComponents[1].Trim();
            string year = dateComponents[2].Trim();

            // We require a two-digit month. If there is only one digit, add a leading zero:
            if (month.Length == 1)
                month = "0" + month;

            // We require a two-digit day. If there is only one digit, add a leading zero:
            if (day.Length == 1)
                day = "0" + day;

            // We require a four-digit year. If there are only two digits, add 
            // two digits denoting the current century as leading numerals:
            if (year.Length == 2)
                year = "20" + year;

            // Put the date back together again with proper delimiters, and 
            dateTextBox.Text = month + "/" + day + "/" + year;
        }


        /// <summary>
        /// Test for entry of common date-delimiting characters, and apply adjustment as needed to 
        /// maintain proper date formatting:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void MaskedDateBox_PreviewKeyDown(object sender, 
                                                PreviewKeyDownEventArgs e)
        {
            MaskedTextBox txt = (MaskedTextBox)sender;

            // Check for common date delimiting characters. When encountered, 
            // adjust the text entry for proper date formatting:
            if (e.KeyCode == Keys.Divide
                || e.KeyCode == Keys.Oem5
                || e.KeyCode == Keys.OemQuestion
                || e.KeyCode == Keys.OemPeriod
                || e.KeyValue == 190
                || e.KeyValue == 110)

                // If any of the above key values are encountered, apply a formatting 
                // check to the text entered so far, and make adjustments as needed. 
                this.CorrectDateText(txt);
        }


        bool IsValidDate(MaskedTextBox dateTextBox)
        {
            // Remove delimiters from the text contained in the control. 
            string DateContents = dateTextBox.Text.Replace("/", "").Trim();

            // if no date was entered, we wil be left with an empty string or whitespace. Otherwise:
            if (!string.IsNullOrEmpty(DateContents) && DateContents != "")
            {
                // Split the original date into components:
                string[] dateSoFar = dateTextBox.Text.Split('/');
                string month = dateSoFar[0].Trim(); ;
                string day = dateSoFar[1].Trim();
                string year = dateSoFar[2].Trim();

                // If the component values are of the proper length for mm/dd/yyyy formatting:
                if (month.Length == 2
                    && day.Length == 2
                    && year.Length == 4
                    && (year.StartsWith("19") || year.StartsWith("20")))
                {
                    // Check to see if the string resolves to a valid date:
                    DateTime d;
                    if (!DateTime.TryParse(dateTextBox.Text, out d))
                    {
                        // The string did NOT resolve to a valid date:
                        return false;
                    }
                    else
                        // The string resolved to a valid date:
                        return true;
                }
                else
                {
                    // The Components are not of the correct size, and automatic adjustment
                    // is unsuccessful:
                    return false;

                } // End if Components are correctly sized
            }
            else
                // The date string is empty or whitespace - no date is a valid return:
                return true;

        } // IsValidDate


        protected override void OnLeave(EventArgs e)
        {
            // Perform a final adjustment of the text entry to fit the mm/dd/yyyy format:
            this.CorrectDateText(this);

            // If the entry is a valid date, fire the leave event. We are done here. 
            if (this.IsValidDate(this))
            {
                base.OnLeave(e);
            }
            else
            {
                this.OnInvalidDateEntry(this, new InvalidDateTextEventArgs(this.Text.Trim()));

                // if a valid date entry is not required, the user is free to navigate away
                // from the control:
                if (!_RequireValidEntry)
                {
                    base.OnLeave(e);
                }
            }
        }


        protected virtual void OnInvalidDateEntry(object sender, InvalidDateTextEventArgs e)
        {
            if (_RequireValidEntry)
            {
                // Force the user to address the problem before navigating away from the control:
                MessageBox.Show(e.Message);
                this.Focus();
                this.MaskedDateTextBox_SelectAllOnEnter(this, new EventArgs());
            }

            // Raise the invalid entry event either way. Client code can determine if and
            // how invalid entry should be dealt with:
            if (InvalidDateEntered != null)
                InvalidDateEntered(this, e);
        }


        /// <summary>
        /// Gets or sets a boolean value indicating whether a valid date string is required
        /// in order to leave the control. Default is true. Invalid date strings which are not empty will
        /// force the user to correct the issue before navigating away from the MaskedDateTextBox. 
        /// </summary>
        public bool RequireValidEntry
        {
            get { return _RequireValidEntry; }
            set { _RequireValidEntry = value; }
        }


        /// <summary>
        /// Required to address a flaw in the implementation of the base MaskedTextBox control provided
        /// with the .net framework. Allows automatic selection of text within the control while the 
        /// mask is set. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MaskedDateTextBox_SelectAllOnEnter(object sender, EventArgs e)
        {
            MaskedTextBox m = (MaskedTextBox)sender;
            this.BeginInvoke((MethodInvoker)delegate()
            {
                m.SelectAll();
            });
        }


        public DateTime? DateValue
        {
            get
            {
                DateTime d;
                DateTime? Result = null;
                if (DateTime.TryParse(this.Text, out d))
                {
                    Result = d;
                }
                return Result;
            }
            set
            {
                string DateString = "";
                if (value.HasValue)
                    DateString = value.Value.ToString("MM/dd/yyyy");
                this.Text = DateString;
            }
        }
    }
}
