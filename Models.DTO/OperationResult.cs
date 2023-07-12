using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class OperationResult<T>
    {
        public T Value { get; private set; }
        public bool Success { get; private set; }
        public string FailureCode { get; private set; }
        public string FailureMessage { get; private set; }

        public OperationResult(T value, bool success = true, string? failureCode = null, string? failureMessage = null)
        {
            this.Value = value;
            this.Success = success;
            this.FailureCode = failureCode;
            this.FailureMessage = failureMessage;
        }
    }
}
