using System;
using System.Collections.Generic;
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
    class Configurator
    {
        public static ToolStripMenuItem useRemoteSettings;
        public static ToolStripMenuItem configure;

        public static ToolStripMenuItem AddUseRemoteSettingsToSubMenu()
        {
            var contextMenuStripField = PluginHelper.LaunchBoxMainForm.GetType().GetField("contextMenuStrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var contextMenuStrip = (ContextMenuStrip)contextMenuStripField.GetValue(PluginHelper.LaunchBoxMainForm);

            for (int i = 0; i < contextMenuStrip.Items.Count; ++i)
            {
                if (contextMenuStrip.Items[i].Text == "PCSX2 Configurator")
                {
                    return (ToolStripMenuItem)(contextMenuStrip.Items[i] as ToolStripMenuItem).DropDownItems.Add("Use Remote Settings");
                }
            }

            return null;
        }

        public static ToolStripMenuItem GetConfigureFromContextMenu()
        {
            var contextMenuStripField = PluginHelper.LaunchBoxMainForm.GetType().GetField("contextMenuStrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var contextMenuStrip = (ContextMenuStrip)contextMenuStripField.GetValue(PluginHelper.LaunchBoxMainForm);

            for (int i = 0; i < contextMenuStrip.Items.Count; ++i)
            {
                if (contextMenuStrip.Items[i].Name == "configureContextToolStripMenuItem")
                {
                    return (ToolStripMenuItem)contextMenuStrip.Items[i];
                }
            }

            return null;
        }

        public static void SetOnSelectionChangeEvent(EventHandler eventHandler)
        {
            var gamesControlPropertyInfo = PluginHelper.LaunchBoxMainForm.GetType().GetProperty("GamesControl");
            var gamesControl = gamesControlPropertyInfo.GetValue(PluginHelper.LaunchBoxMainForm);

            var selectedGameChangedEventInfo = gamesControl.GetType().GetEvent("SelectedGameChanged");
            selectedGameChangedEventInfo.AddEventHandler(gamesControl, eventHandler);
        }

        private static bool IsKnownGame(IGame game)
        {
            var gameTitle = Utilities.GetSafeTitle(game);
            gameTitle = gameTitle.Replace(" ", "%20");
            gameTitle = gameTitle.Replace("&", "and");
            gameTitle = gameTitle.ToLower();

            var url = "https://github.com/roguesaloon/launchbox-plugin_pcsx2-configurator/tree/master/Game%20Configs/";
            url = url + gameTitle;

            HttpWebResponse response = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowReadStreamBuffering = false;
            request.AllowWriteStreamBuffering = false;
            request.Method = "HEAD";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                response.Close();
            }
            catch { }

            return response != null && response.StatusCode == HttpStatusCode.OK;
        }

        public static void OnUseRemoteSettingClick()
        {

        }

        public static void OnConfigureClick()
        {

        }

        public static void OnSelectionChange()
        {
            if(MenuItemPlugin.selectedGame != null && Utilities.IsGameValid(MenuItemPlugin.selectedGame))
            {
                configure.Enabled = true;
                Utilities.HideContextMenuItem("PCSX2 Configurator", !IsKnownGame(MenuItemPlugin.selectedGame));
            }
            else
            {
                Utilities.HideContextMenuItem("PCSX2 Configurator", true);
            }
        }
    }
}
