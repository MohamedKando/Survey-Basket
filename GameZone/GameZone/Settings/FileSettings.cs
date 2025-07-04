namespace GameZone.Settings
{
    public static class FileSettings
    {
        public const string ImagePath = "/assets/Images/Game";
        public const string AllowedExtentions = ".jpg,.jpeg,.png,.mp4";
        public const int MaxAllowedSizeInMB = 1;
        public const int MaxAllowedSizeInBytes = MaxAllowedSizeInMB*1024*1024;
    }
}
