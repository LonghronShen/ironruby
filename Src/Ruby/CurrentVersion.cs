using System;
using System.Reflection;

namespace IronRuby
{
    public static class CurrentVersion
    {
        public const string AssemblyCopyright = "Copyright (c) IronRuby Contributors. All rights reserved.";

        public static readonly Version Version = typeof(CurrentVersion).Assembly
#if SILVERLIGHT
            .GetAssemblyName()
#else
            .GetName()
#endif
            .Version;

        public static int Major
        {
            get
            {
                return Version.Major;
            }
        }

        public static int Minor
        {
            get
            {
                return Version.Minor;
            }
        }

        public static int Micro
        {
            get
            {
                return Version.Revision;
            }
        }

        public static string ReleaseLevel
        {
            get
            {
                return GitVersionInformation.BranchName;
            }
        }

        public static int ReleaseSerial
        {
            get
            {
                return 0;
            }
        }

        public static string ShortReleaseLevel
        {
            get
            {
                if (ReleaseLevel.StartsWith("release/alpha"))
                {
                    return "a";
                }
                if (ReleaseLevel.StartsWith("release/beta"))
                {
                    return "b";
                }
                if (ReleaseLevel.StartsWith("release/candidate"))
                {
                    return "rc";
                }
                if (ReleaseLevel.StartsWith("release/v"))
                {
                    return "f";
                }
                return "d";
            }
        }

        public static readonly string AssemblyVersion = GitVersionInformation.AssemblySemVer;
        public static readonly string AssemblyFileVersion = GitVersionInformation.AssemblySemFileVer;
        public static readonly string AssemblyInformationalVersion = GitVersionInformation.InformationalVersion;

        public static readonly string Series = $"{Major}.{Minor}";
        public static readonly string DisplayVersion = AssemblyVersion;
        public static readonly string DisplayName = $"IronRuby {GitVersionInformation.InformationalVersion}";
    }
}
