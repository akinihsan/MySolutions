using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Service.DTO
{
    public class ServiceResultDto
    {
        public ServiceResultDto(bool Successful, string ErrorMessage = "", string ResultId = "")
        {
            this.Successful = Successful;
            this.ErrorMessage = ErrorMessage;
            this.ResultId = ResultId;
        }
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }
        public string ResultId { get; set; }
    }
}
