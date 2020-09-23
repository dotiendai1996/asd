using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Hinnova.Editions.Dto;

namespace Hinnova.Web.Areas.App.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}