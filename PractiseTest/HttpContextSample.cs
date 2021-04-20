using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PractiseTest
{
    public class HttpContextSample
    {
        public StringBuilder Output { get; set; }
        public HttpContextSample() {
            Output = new StringBuilder();
        }
    }

    public delegate Task RequestDelegate(HttpContextSample context);
}
