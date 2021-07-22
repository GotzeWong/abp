using System;
using System.IO;
using System.Threading.Tasks;
using AgentHub.Shared.OSSUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.TenantManagement
{
    [Controller]
    [RemoteService(Name = TenantManagementRemoteServiceConsts.RemoteServiceName)]
    [Area("multi-tenancy")]
    [Route("api/multi-tenancy/tenants")]
    public class TenantController : AbpController, ITenantAppService //TODO: Throws exception on validation if we inherit from Controller
    {
        protected ITenantAppService TenantAppService { get; }

        private readonly IConfiguration _configuration;

        public TenantController(ITenantAppService tenantAppService,
            IConfiguration configuration)
        {
            TenantAppService = tenantAppService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TenantDto> GetAsync(Guid id)
        {
            return TenantAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<TenantDto>> GetListAsync(GetTenantsInput input)
        {
            return TenantAppService.GetListAsync(input);
        }

        [HttpPost]
        public virtual Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            ValidateModel();
            return TenantAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TenantDto> UpdateAsync(Guid id, TenantUpdateDto input)
        {
            return TenantAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return TenantAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}/default-connection-string")]
        public virtual Task<string> GetDefaultConnectionStringAsync(Guid id)
        {
            return TenantAppService.GetDefaultConnectionStringAsync(id);
        }

        [HttpPut]
        [Route("{id}/default-connection-string")]
        public virtual Task UpdateDefaultConnectionStringAsync(Guid id, string defaultConnectionString)
        {
            return TenantAppService.UpdateDefaultConnectionStringAsync(id, defaultConnectionString);
        }

        [HttpDelete]
        [Route("{id}/default-connection-string")]
        public virtual Task DeleteDefaultConnectionStringAsync(Guid id)
        {
            return TenantAppService.DeleteDefaultConnectionStringAsync(id);
        }


        /// <summary>
        /// 上传logo
        /// </summary>
        /// <param name="id">tenantId</param>
        /// <param name="file">商标图片</param>
        [HttpPost]
        [Route("{id}/upload-tm")]
        public virtual async Task<string> UploadTrademarkAsync(Guid id, [FromForm]IFormFile file)
        {
            //var tenantId = CurrentTenant.Id;

            var datetime = DateTime.Now.ToString("yyyy-MM-dd");

            var filename = Guid.NewGuid() +Path.GetExtension(file.FileName);

            //Todo 判断是否是图片
            //Todo 判断是否重复上传

            var objectName = id + "/" + datetime + "/" + filename;

            var ossConfig = new OssConfig(_configuration["Oss:AccessKeyId"], 
                _configuration["Oss:AccessKeySecret"]);
            var tm_url = OssHelper.PutObject(ossConfig, OssConfig.BUCKET_TRADEMARK, objectName, file);

            await TenantAppService.UpdateTrademarkAsync(id, tm_url);
            
            return tm_url;
        }

        [HttpPut]
        [Route("{id}/update-tm")]
        public virtual Task UpdateTrademarkAsync(Guid id, string TrademarkUrl)
        {
            return TenantAppService.UpdateTrademarkAsync(id, TrademarkUrl);
        }

        /// <summary>
        /// 下载logo
        /// </summary>
        /// <param name="id">商家Id</param>
        [HttpGet]
        [Route("{id}/tm")]
        public virtual async Task<string> GetTrademarkAsync(Guid id)
        {
            return await TenantAppService.GetTrademarkAsync(id);
        }

        
    }
}