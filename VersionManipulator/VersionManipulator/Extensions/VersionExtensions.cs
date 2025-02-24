namespace VersionManipulator.Extensions
{
    public static class VersionExtensions
    {
        public static Entities.Version ParseVersion(this string version)
        {
            if (string.IsNullOrWhiteSpace(version)) { throw new ArgumentException("The file has no content"); };

            var ver = version.Split(".");

            if (ver.Length != 4) { throw new FormatException("The version does not match the standard"); }

            var parsedVersion = new Entities.Version();

            try
            {
                parsedVersion.Major = int.Parse(ver[0]);
                parsedVersion.Minor = int.Parse(ver[1]);
                parsedVersion.Build = int.Parse(ver[2]);
                parsedVersion.Revision = int.Parse(ver[3]);
            }
            catch (Exception)
            {
                throw;
            }

            return parsedVersion;
        }

        public static void IncreaseBuild(this Entities.Version version)
        {
            version.Build++;
        }

        public static void IncreaseRevision(this Entities.Version version)
        {
            version.Revision++;
        }

        public static string GetString(this Entities.Version version)
        {
            var versionString = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            return versionString;
        }
    }
}
