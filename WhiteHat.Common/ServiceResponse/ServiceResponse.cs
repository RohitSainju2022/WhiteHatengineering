using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Common.Static.Enum;

namespace WhiteHat.Common.ServiceResponse
{
    public class ServiceResponse<T>
    {
        public T Result { get; set; }
        public ServiceResponseTypes Type { get; set; }

        public String Displaymessage { get; set; }

        public String ErrorCode { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public string ErrorMessage
        {
            get
            {
                return Errors != null && Errors.Any() ? string.Join(", ", Errors) : string.Empty;
            }
        }
    }
}
