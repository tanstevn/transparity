namespace Transparity.Data.Records {
    public record UserInfoRecord(string FirstName, string LastName, string Email,
        string Address1, string? Mobile = null, string? Address2 = null);
}
