using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{
    class Utilities
    {
        public static string PluginDirectory
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        public static string SettingsFile
        {
            get
            {
                return PluginDirectory + "\\Settings.ini";
            }
        }

        public static string FullEmulatorPath
        {
            get
            {
                foreach (var emulator in PluginHelper.DataManager.GetAllEmulators())
                {
                    if (emulator.Title.Contains("PCSX2"))
                    {
                        var appPath = emulator.ApplicationPath;
                        appPath = (!Path.IsPathRooted(appPath)) ? Directory.GetCurrentDirectory() + "\\" + appPath : appPath;

                        return appPath;
                    }
                }

                MessageBox.Show("It appears you do not have PCSX2 added to LaunchBox\nWhich this plugin needs to function correctly");

                return null;
            }
        }

        public static Icon EmulatorIcon
        {
            get
            {
                return Icon.ExtractAssociatedIcon(FullEmulatorPath);
            }
        }

        public static string GetSafeTitle(IGame game)
        {
            var gameTitle = game.Title;

            foreach (char c in Path.GetInvalidFileNameChars())
                gameTitle = gameTitle.Replace(c.ToString(), "");

            return gameTitle;
        }

        public static bool IsGameValid(IGame game)
        {
            var emulator = PluginHelper.DataManager.GetEmulatorById(game.EmulatorId);
            return (emulator != null && (emulator.Title.Contains("PCSX2") || ((emulator.Title.Contains("Rocket Launcher") || emulator.Title.Contains("RocketLauncher")) && game.Platform == "Sony Playstation 2")));
        }

        public static void HideContextMenuItem(string itemText, bool hide)
        {
            // Gets the context menu from Launchbox Main Form
            var contextMenuStripField = PluginHelper.LaunchBoxMainForm.GetType().GetField("contextMenuStrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var contextMenuStrip = (ContextMenuStrip)contextMenuStripField.GetValue(PluginHelper.LaunchBoxMainForm);

            var hiddenItems = new bool[contextMenuStrip.Items.Count];

            // Hides or shows the menu item with this plugins caption
            for (int i = 0; i < contextMenuStrip.Items.Count; ++i)
            {
                if (contextMenuStrip.Items[i].Text == itemText)
                {
                    contextMenuStrip.Items[i].Visible = !hide;
                    hiddenItems[i] = hide;
                    break;
                }
            }


            // If all plugins in context menu are hidden, also hide the last seperator
            for (int i = contextMenuStrip.Items.Count - 1, itemsHidden = 0; i > -1; --i)
            {
                if (contextMenuStrip.Items[i].GetType() == typeof(ToolStripSeparator))
                {
                    contextMenuStrip.Items[i].Visible = (itemsHidden == contextMenuStrip.Items.Count - i - 1) ? false : true;
                    break;
                }

                if (hiddenItems[i] == true)
                {
                    itemsHidden++;
                }
            }
        }


    }
}
