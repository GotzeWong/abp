﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.Abp.Identity
{
    public interface IOrganizationUnitAppService : ICrudAppService<OrganizationUnitDto, Guid, GetOrganizationUnitsInput, OrganizationUnitCreateDto, OrganizationUnitUpdateDto>
    {
        Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync();
        Task<List<OrganizationUnitParentDto>> GetArrangedListAsync();
    }
}
