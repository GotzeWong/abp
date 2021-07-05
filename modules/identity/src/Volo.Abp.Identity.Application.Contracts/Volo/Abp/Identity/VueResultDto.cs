using System;

namespace Volo.Abp.Identity
{
    [Serializable]
    public class VueResultDto
    {
        public VueResultDto()
        {
            Code = ResultEnum.SUCCESS;
            Message = "ok";
            Type = "success";
        }

        public ResultEnum Code { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

    }

    public enum ResultEnum
    {
        SUCCESS = 0,
        ERROR = 1,
        TIMEOUT = 401,
    }
}
