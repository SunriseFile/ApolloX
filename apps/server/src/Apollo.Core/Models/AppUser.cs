namespace Apollo.Core.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DisplayUserName { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
    }
}
