using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace PCSX2_Configurator
{
    class MenuItemPlugin : IGameMenuItemPlugin
    {
        public static IGame selectedGame = null;

        public string Caption
        {
            get
            {
                return "PCSX2 Configurator";
            }
        }

        public Image IconImage
        {
            get
            {
                return Utilities.EmulatorIcon.ToBitmap();
            }
        }

        public bool ShowInBigBox
        {
            get
            {
                return false;
            }
        }

        public bool ShowInLaunchBox
        {
            get
            {
                return true;
            }
        }

        public bool SupportsMultipleGames
        {
            get
            {
                return false;
            }
        }

        public bool GetIsValidForGame(IGame selectedGame)
        {
            MenuItemPlugin.selectedGame = selectedGame;
            return Utilities.IsGameValid(selectedGame);
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return SupportsMultipleGames;
        }

        public void OnSelected(IGame[] selectedGames)
        {
            return;
        }

        public void OnSelected(IGame selectedGame)
        {
            return;
        }
    }
}
