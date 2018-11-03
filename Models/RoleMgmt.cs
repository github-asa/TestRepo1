namespace J2BIOverseasOps.Models
{
        public class Roles
        {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int NumOfUsers { get; set; }
        public int NumOfPermissions { get; set; }
        }

    public class Permissions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumOfRoles { get; set; }
    }

    public class RoleToPermissionMap
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
    }

    public class RoleToUserMap
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class UserToDestinationMap
    {
        public int UserId { get; set; }
        public int[] DestinationIds { get; set; }
    }
}