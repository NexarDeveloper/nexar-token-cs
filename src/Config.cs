using System;

namespace Nexar.Token
{
    /// <summary>
    /// This configuration is only needed for internal Nexar development.
    /// Clients just use "https://identity.nexar.com/" as Authority.
    /// </summary>
    static class Config
    {
        public static Mode NexarMode { get; }
        public static string Authority { get; }

        static Config()
        {
            var mode = Environment.GetEnvironmentVariable("NEXAR_MODE");
            NexarMode = mode == null ? Mode.Dev : (Mode)Enum.Parse(typeof(Mode), mode, true);

            switch (NexarMode)
            {
                case Mode.Prod:
                    Authority = "https://identity.nexar.com/";
                    break;
                case Mode.Dev:
                    Authority = "https://identity.nexar.com/";
                    break;
                default:
                    throw new Exception();
            }
        }

        public enum Mode
        {
            Prod,
            Dev,
        }
    }
}
