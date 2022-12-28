using System.Windows;

namespace Common
{
    public class AboutProperties
    {
        public Window Owner { get; set; }
        public string Description { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public string ImageSize { get; set; } = "96";
        public string ApplicationName { get; set; } = "";
        public string ApplicationVersion { get; set; } = "";
        public string Copyright { get; set; } = "";
        public string Background { get; set; } = "Transparent";
        public string ApplicationNameFontSize { get; set; } = "28";
        public string ApplicationVersionFontSize { get; set; } = "16";
        public string Height { get; set; } = "205";
        public string Width { get; set; } = "370";
    }
}
