namespace ApiCourse.Models
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }=string.Empty;
        public DateTime ExpiresIn { get; set; } 
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime? RevokedOn { get; set;}

        public bool IsExpired => DateTime.UtcNow >= ExpiresIn;

        public bool isActive => RevokedOn == null && !IsExpired;
    }
}
