using System;

namespace Volo.Abp.Identity
{
    [Serializable]
    public class VueTResultDto<T> : VueResultDto
    {
        public VueTResultDto(ResultEnum code, string message, T result, string type)
        {
            Code = code;
            Message = message;
            Result = result;
            Type = type;
        }

        public VueTResultDto(T result):base()
        {
            Result = result;
        }

        public T Result { get; set; }

    }

}
