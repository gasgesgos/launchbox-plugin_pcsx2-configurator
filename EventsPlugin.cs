using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;

namespace PCSX2_Configurator
{
    class EventsPlugin : ISystemEventsPlugin
    {
        public void OnEventRaised(string eventType)
        {
            switch (eventType)
            {
                case "LaunchBoxStartupCompleted":
                    LaunchBoxStartupCompleted();
                    break;
                case "PluginInitialized":
                    PluginIntialized();
                    break;
                default:
                    break;
            }
        }

        private void LaunchBoxStartupCompleted()
        {
            Configurator.useRemoteSettings = Configurator.AddUseRemoteSettingsToSubMenu();
            Configurator.configure = Configurator.GetConfigureFromContextMenu();

            Configurator.useRemoteSettings.Click += (sender, e) => Configurator.OnUseRemoteSettingClick();
            Configurator.configure.Click += (sender, e) => Configurator.OnConfigureClick();
            Configurator.SetOnSelectionChangeEvent((sender, e) => Configurator.OnSelectionChange());
        }

        private void PluginIntialized()
        {
            DownloadSVN();
            CreateSettingsFile();
            ExtractWidescreenPatches();
        }

        private void DownloadSVN()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\SVN"))
            {
                try
                {
                    new WebClient().DownloadFile("https://www.visualsvn.com/files/Apache-Subversion-1.9.7.zip", Directory.GetCurrentDirectory() + "\\SVN.zip");
                    ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + "\\SVN.zip", Directory.GetCurrentDirectory() + "\\SVN");
                    File.Delete(Directory.GetCurrentDirectory() + "\\SVN.zip");
                }
                catch { }
            }
        }

        private void CreateSettingsFile()
        {
            if (!File.Exists(Utilities.SettingsFile))
            {
                File.Create(Utilities.SettingsFile).Dispose();

                IniFileHelper.WriteValue("PCSX2_Configurator", "UseIndependantMemoryCards", "true", Utilities.SettingsFile);
                IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFileSettings", "true", Utilities.SettingsFile);
                IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentWindowSettings", "true", Utilities.SettingsFile);
                IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLogSettings", "true", Utilities.SettingsFile);
                IniFileHelper.WriteValue("PCSX2_Configurator", "AllowAllSettings", "false", Utilities.SettingsFile);
                IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFolderSettings", "false", Utilities.SettingsFile);
                IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentVMSettings", "false", Utilities.SettingsFile);
                IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentGSdxPluginSettings", "false", Utilities.SettingsFile);
                IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLilyPadPluginSettings", "false", Utilities.SettingsFile);
                IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentSPU2xPluginSettings", "false", Utilities.SettingsFile);
                IniFileHelper.WriteValue("PCSX2_Configurator", "ConfigsDirectoryPath", "default", Utilities.SettingsFile);
            }
        }

        private void ExtractWidescreenPatches()
        {
            try
            {
                ZipFile.ExtractToDirectory(Path.GetDirectoryName(Utilities.FullEmulatorPath) + "\\cheats_ws.zip", Path.GetDirectoryName(Utilities.FullEmulatorPath) + "\\cheats_ws");
            }
            catch { }
        }


    }
}
