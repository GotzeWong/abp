using Volo.Abp.Data;

namespace Volo.Abp.Identity
{
    /// <summary>
    /// Represents an organization unit (OU).
    /// </summary>
    public static class OrganizationUnitExtensions
    {
        public const string IsSalePropertyName = "IsSale";

        public static void SetSale(this OrganizationUnit ou, bool isSale)
        {
            ou.SetProperty(IsSalePropertyName, isSale);
        }

        public static bool GetSale(this OrganizationUnit ou)
        {
            return ou.GetProperty<bool>(IsSalePropertyName);
        }
    }
}
