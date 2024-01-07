namespace NetMenu.AppLib.Auth
{
    public record AppUser
    {
        public bool? Login { get; set; } = null;
        public string? UserId { get; set; } = null;
        public List<string>? UserRoles { get; set; } = null;
        public string? SelectedRole { get; set; } = null;
    }
}
