using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Hinnova.Authorization.Users.Dto;
using Hinnova.Management.Dtos;
using Hinnova.Organizations.Dto;

namespace Hinnova.Authorization.Users
{
    [AbpAuthorize]
    public class UserLoginAppService : HinnovaAppServiceBase, IUserLoginAppService
    {
        private readonly IRepository<UserLoginAttempt, long> _userLoginAttemptRepository;

        public UserLoginAppService(IRepository<UserLoginAttempt, long> userLoginAttemptRepository)
        {
            _userLoginAttemptRepository = userLoginAttemptRepository;
        }

        [DisableAuditing]
        public async Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts()
        {
            var userId = AbpSession.GetUserId();

            var loginAttempts = await _userLoginAttemptRepository.GetAll()
                .Where(la => la.UserId == userId)
                .OrderByDescending(la => la.CreationTime)
                .Take(10)
                .ToListAsync();

            return new ListResultDto<UserLoginAttemptDto>(ObjectMapper.Map<List<UserLoginAttemptDto>>(loginAttempts));
        }

        public async Task<GetUserLoginForEdit> GetUserForView(int id)
        {
            var user = await UserManager.GetUserByIdAsync(id);
           

            var output = new GetUserLoginForEdit { User = ObjectMapper.Map<UserEditDto>(user) };

            return output;
        }
    }
}