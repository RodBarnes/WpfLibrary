using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Xml;

namespace Common.ViewModels
{
    public class BaseVM : INotifyPropertyChanged
    {
        // These fields must be set by the MainVM 
        protected AssemblyInfo AssyInfo;
        protected string UserAppDataPath;
        protected string ProgramAppPath;
        protected string SettingsFilePath;

        // Use in AlreadyRunning() to ensure only a single copy of the app is running
        private static Mutex mutex = new Mutex(true, new Guid().ToString());

        public BaseVM() { }

        protected bool AlreadyRunning()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                mutex.ReleaseMutex();
                return false;
            }
            else
            {
                return true;
            }
        }

        #region Event Handlers

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Settings Management

        /// <summary>
        /// Use of the settings expects a Settings.xml file as found in CommonLibrary CopyModules
        /// Each setting name must match a property of the same name in the MainVM in order for the values
        /// to be bound together
        /// </summary>
        private static readonly char userNameDel = '_';
        private static XmlDocument xmlDoc;
        private static XmlNode appNode;
        private static XmlNode defNode;
        private static XmlNode userNode;

        protected void SettingsLoad()
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(SettingsFilePath);

            SettingsRefresh();
        }

        protected void SettingsRefresh()
        {
            defNode = xmlDoc.DocumentElement.SelectSingleNode("/Settings/UserSettings/Default");
            userNode = xmlDoc.DocumentElement.SelectSingleNode($"/Settings/UserSettings/{WindowsIdentity.GetCurrent().Name.Replace('\\', userNameDel)}");

            foreach (XmlNode node in defNode.ChildNodes)
            {
                if (node.Name == "Setting")
                {
                    // Attempt to get a reference to the property and, if found, set its value
                    var prop = this.GetType().GetProperty(node.Attributes["Name"].Value);
                    if (prop != null)
                    {
                        var custNode = userNode?.SelectSingleNode($"Setting[@Name='{node.Attributes["Name"].Value}']");
                        if (custNode != null)
                        {
                            // User has a setting
                            prop.SetValue(this, custNode.Attributes["Value"].Value);
                        }
                        else
                        {
                            var valType = "bool";   // Default for old settings files which may not have the type specified
                            if (node.Attributes["Type"] != null)
                            {
                                valType = node.Attributes["Type"].Value;
                            }

                            //var valName = node.Attributes["Name"].Value;
                            var val = node.Attributes["Value"].Value;
                            switch (valType)
                            {
                                case "token":
                                    // Derive the user-specific default
                                    switch (val)
                                    {
                                        case "*Documents":
                                            prop.SetValue(this, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                                            break;
                                        case "*Common":
                                            prop.SetValue(this, Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));
                                            break;
                                        case "*Roaming":
                                            prop.SetValue(this, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\{AssyInfo.Company}\{AssyInfo.Product}");
                                            break;
                                        case "*Local":
                                            prop.SetValue(this, $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\{AssyInfo.Company}\{AssyInfo.Product}");
                                            break;
                                        case "*Personal":
                                            prop.SetValue(this, Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                                            break;
                                        case "*Desktop":
                                            prop.SetValue(this, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case "bool":
                                    prop.SetValue(this, val);
                                    break;
                                case "int":
                                    int.TryParse(val, out int num);
                                    prop.SetValue(this, num);
                                    break;
                                default:
                                    prop.SetValue(this, val);
                                    break;
                            }
                        }
                    }
                }
            }

            appNode = xmlDoc.DocumentElement.SelectSingleNode("/Settings/AppSettings");
            foreach (XmlNode node in appNode.ChildNodes)
            {
                if (node.Name == "Setting")
                {
                    // Attempt to get a reference to the property and, if found, set its value
                    var prop = this.GetType().GetProperty(node.Attributes["Name"].Value);
                    if (prop != null)
                    {
                        prop.SetValue(this, node.Attributes["Value"].Value);
                    }
                }
            }
        }

        protected void SettingsClear()
        {
            if (xmlDoc != null)
            {
                // Remove user settings and return to using default settings
                var mainNode = xmlDoc.DocumentElement.SelectSingleNode($"/Settings/UserSettings");
                mainNode.RemoveChild(userNode);
                xmlDoc.Save(SettingsFilePath);
                SettingsRefresh();
            }
        }

        protected void SettingsSave(bool force = false)
        {
            if (xmlDoc != null)
            {
                var saveSettings = force;
                if (defNode != null)
                {
                    // User Settings
                    foreach (XmlNode node in defNode.ChildNodes)
                    {
                        if (node.Name == "Setting")
                        {
                            // Attempt to get a reference to the property and, if found, set its value
                            var prop = this.GetType().GetProperty(node.Attributes["Name"].Value);
                            if (prop != null)
                            {
                                var settingValue = node.Attributes["Value"].Value;
                                var propValue = prop.GetValue(this).ToString();
                                var custNode = userNode?.SelectSingleNode($"Setting[@Name='{node.Attributes["Name"].Value}']");
                                if (custNode != null)
                                {
                                    settingValue = custNode.Attributes["Value"].Value;
                                }
                                if (settingValue != propValue)
                                {
                                    if (custNode == null)
                                    {
                                        // Create new node
                                        custNode = node.Clone();
                                    }
                                    custNode.Attributes["Value"].Value = propValue;
                                    if (userNode == null)
                                    {
                                        // Create a new user section
                                        userNode = xmlDoc.CreateElement(WindowsIdentity.GetCurrent().Name.Replace('\\', userNameDel));
                                    }
                                    userNode.AppendChild(custNode);
                                    saveSettings = true;
                                }
                            }
                        }
                    }

                    if (userNode != null)
                    {
                        // Append the new node that was created.
                        var mainNode = xmlDoc.DocumentElement.SelectSingleNode("/Settings/UserSettings");
                        mainNode.AppendChild(userNode);
                    }
                }

                if (appNode != null)
                {
                    // App settings
                    foreach (XmlNode node in appNode.ChildNodes)
                    {
                        if (node.Name == "Setting")
                        {
                            // Attempt to get a reference to the property and, if found, set its value
                            var prop = this.GetType().GetProperty(node.Attributes["Name"].Value);
                            if (prop != null)
                            {
                                var settingValue = node.Attributes["Value"].Value;
                                var propValue = prop.GetValue(this).ToString();
                                if (settingValue != propValue)
                                {
                                    node.Attributes["Value"].Value = propValue;
                                    saveSettings = true;
                                }
                            }
                        }
                    }
                }

                if (saveSettings)
                {
                    // Save the changes
                    xmlDoc.Save(SettingsFilePath);
                }
            }
        }

        #endregion
    }
}
