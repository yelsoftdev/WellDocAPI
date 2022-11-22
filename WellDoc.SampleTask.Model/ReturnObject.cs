using System;
using System.Collections.Generic;
using System.Text;

namespace WellDoc.SampleTask.Model
{
    public class ReturnObject<T>
    {
        public bool isStatus { get; set; }

        public string code { get; set; }

        public string message { get; set; }

        public T data { get; set; }
    }
}
