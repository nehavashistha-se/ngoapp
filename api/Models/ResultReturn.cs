using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static api.Models.Enums;

namespace api.Models
{
    public class ResultReturn
    {
        public ResultStatus Status_Code { get; set; }
public string Exception {get;set;}
public int? Id {get;set;}
    }
}
