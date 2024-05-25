namespace BuyMovies.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string email { get; set; }
        public string UserName { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }

}
