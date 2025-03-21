using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoWord.SharedKernel.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string Error { get; }

        public Result(bool isSuccess, T? value, string error)
        {
            this.IsSuccess = isSuccess;
            this.Value = value;
            this.Error = error;
        }

        //Factory method for success
        public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty);

        public static Result<T> Failure(string error) => new Result<T>(false, default, string.Empty);
    }
}
