using System.Collections.Generic;
using MvvmHelpers;
using Hinnova.Models.NavigationMenu;

namespace Hinnova.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}