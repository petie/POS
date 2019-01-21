using System;

namespace Posnet
{
    public class FiscalException : Exception
    {
        protected int number;
        protected string message;
        protected ExceptionType type;

        internal FiscalException()
        {
        }

        public FiscalException(string message, int? number, ExceptionType? type)
        {
            this.message = message;
            this.number = number.Value;
            this.type = type.Value;
        }

        public FiscalException(int num)
        {
            number = num;
            type = ExceptionType.Fatal;
        }

        public FiscalException(string msg)
        {
            message = msg;
            type = ExceptionType.Fatal;
        }

        public int Number
        {
            get
            {
                return number;
            }
        }

        public override string Message
        {
            get
            {
                return message;
            }
        }

        public ExceptionType Type
        {
            get
            {
                return type;
            }
        }
    }
}
