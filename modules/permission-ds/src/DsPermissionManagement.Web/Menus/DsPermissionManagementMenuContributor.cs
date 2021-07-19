using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace DsPermissionManagement.Web.Menus
{
    public class DsPermissionManagementMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(DsPermissionManagementMenus.Prefix, displayName: "DsPermissionManagement", "~/DsPermissionManagement", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}