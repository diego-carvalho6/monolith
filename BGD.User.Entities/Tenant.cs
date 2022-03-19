namespace BGD.User.Entities
{
    public class Tenant
    {
        public string? TenantName { get; set; }

        public Tenant()
        {
            TenantName = "default";
        }
    }
}