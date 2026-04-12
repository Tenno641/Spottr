namespace UserManagement.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class User
    {
        public static Guid Id => Guid.CreateVersion7();
        public static string Name => "User-Name";
        public static int Age => 18;
        public static string Email => "Example@Example.com";
        public static string HashedPassword => "gei35iwinjkn3";
    }
}