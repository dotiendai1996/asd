using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class GetTextBookForEditOutput
    {
		public CreateOrEditTextBookDto TextBook { get; set; }


    }
}