using System;


namespace MaskedDateEntryControl
{
    public class InvalidDateTextEventArgs : EventArgs
    {

        private string _Message = "" 
            + "Text does not resolve to a valid date. "
            + "Enter a date in mm/dd/yyyy format, or clear the text to represent an empty date.";

        private string _InvalidDateString = "";


        public InvalidDateTextEventArgs(string InvalidDateString) : base()
        {
            _InvalidDateString = InvalidDateString;
        }


        public InvalidDateTextEventArgs(string InvalidDateString, string Message) 
            : this(InvalidDateString)
            {
                _Message = Message;
            }


        public String Message
        {
            get { return _Message; }
            set { _Message = value; }
        }


        public String InvalidDateString
        {
            get { return _InvalidDateString; }
        }


    }
}
