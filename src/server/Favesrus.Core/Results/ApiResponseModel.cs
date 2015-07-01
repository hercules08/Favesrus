using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Core.Results
{
    public class ApiResponseModel<T>
    {

        public ApiResponseModel(bool success, T model)
        {
            Success = success;
            Model = model;
        }

        public bool Success { get; set; }
        public T Model { get; set; }
        public string ApiResponseMessage { get; set; }
    }
}
