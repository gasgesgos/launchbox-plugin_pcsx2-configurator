using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;

namespace PCSX2_Configurator
{
    public partial class SettingsForm : Form
    {
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0XA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.ClipRectangle);
            e.Graphics.DrawImage(Image.FromFile(Utilities.PluginDirectory + "\\Assets\\background.png"), new Rectangle(0, 0, this.Width, this.Height));
        }

        private static Image checkmark;
        private static PrivateFontCollection privateFontCollection;

        public SettingsForm()
        {
            InitializeComponent();

            checkmark = new Bitmap(Image.FromFile(Utilities.PluginDirectory + "\\Assets\\checkmark.png"), new Size(16 * this.Width / 400, 16 * this.Height / 400));
            LoadFont();
            LoadFromIniFile();
            this.Icon = Utilities.EmulatorIcon;
         }

        private void LoadFont()
        {
            privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(Utilities.PluginDirectory + "\\Assets\\FixedsysExcelsiorAscii.ttf");

            var Fixedsys9 = new Font(privateFontCollection.Families[0], 9, FontStyle.Regular);
            var Fixedsys10 = new Font(privateFontCollection.Families[0], 10, FontStyle.Regular);

            useIndependantMemoryCardsLBL.Font = Fixedsys10;
            useFileSettingsLBL.Font = Fixedsys10;
            useWindowSettingsLBL.Font = Fixedsys10;
            useLogSettingsLBL.Font = Fixedsys10;
            useFolderSettingsLBL.Font = Fixedsys10;
            useVMSettingsLBL.Font = Fixedsys10;
            useGSdxPluginSettingsLBL.Font = Fixedsys10;
            useSPU2xPluginSettingsLBL.Font = Fixedsys10;
            useLilyPadPluginSettingsLBL.Font = Fixedsys10;
            configDirLBL.Font = Fixedsys9;
            configDirTXT.Font = Fixedsys9;
        }

        private void LoadFromIniFile()
        {
           if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseIndependantMemoryCards", Utilities.SettingsFile) == "true")
                useIndependantMemoryCardsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentFileSettings", Utilities.SettingsFile) == "true")
                useCurrentFileSettingsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentWindowSettings", Utilities.SettingsFile) == "true")
                useCurrentWindowSettingsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentLogSettings", Utilities.SettingsFile) == "true")
                useCurrentLogSettingsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentFolderSettings", Utilities.SettingsFile) == "true")
                useCurrentFolderSettingsCHK.Image = checkmark;

            if(IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentVMSettings", Utilities.SettingsFile) == "true")
                useCurrentVMSettingsCHK.Image = checkmark;

            if(IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentGSdxPluginSettings", Utilities.SettingsFile) == "true")
                useCurrentGSdxPluginSettingsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentSPU2xPluginSettings", Utilities.SettingsFile) == "true")
                useCurrentSPU2xPluginSettingsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentLilyPadPluginSettings", Utilities.SettingsFile) == "true")
                useCurrentLilyPadPluginSettingsCHK.Image = checkmark;

            configDirTXT.Text =
                IniFileHelper.ReadValue("PCSX2_Configurator", "ConfigsDirectoryPath", Utilities.SettingsFile);
        }

        private void WriteToIniFile()
        {
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseIndependantMemoryCards", 
                useIndependantMemoryCardsCHK.Image == checkmark ? "true" : "false", Utilities.SettingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFileSettings",
                useCurrentFileSettingsCHK.Image == checkmark ? "true" : "false", Utilities.SettingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentWindowSettings",
                useCurrentWindowSettingsCHK.Image == checkmark ? "true" : "false", Utilities.SettingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLogSettings",
                useCurrentLogSettingsCHK.Image == checkmark ? "true" : "false", Utilities.SettingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFolderSettings",
                useCurrentFolderSettingsCHK.Image == checkmark ? "true" : "false", Utilities.SettingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentVMSettings",
               useCurrentVMSettingsCHK.Image == checkmark ? "true" : "false", Utilities.SettingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentGSdxPluginSettings",
                useCurrentGSdxPluginSettingsCHK.Image == checkmark ? "true" : "false", Utilities.SettingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentSPU2xPluginSettings",
                useCurrentSPU2xPluginSettingsCHK.Image == checkmark ? "true" : "false", Utilities.SettingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLilyPadPluginSettings",
                useCurrentLilyPadPluginSettingsCHK.Image == checkmark ? "true" : "false", Utilities.SettingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "ConfigsDirectoryPath",
                configDirTXT.Text, Utilities.SettingsFile);
        }

        private void configDirBTN_Click(object sender, EventArgs e)
        {
            if (configDirDLG.ShowDialog() == DialogResult.OK)
            {
                configDirTXT.Text = configDirDLG.SelectedPath;
            }
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            WriteToIniFile();
            Close();
        }

        private void useIndependantMemoryCardsCHK_Click(object sender, EventArgs e)
        {
            useIndependantMemoryCardsCHK.Image = (useIndependantMemoryCardsCHK.Image != checkmark) ?  checkmark : null;
        }

        private void useCurrentFileSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentFileSettingsCHK.Image = (useCurrentFileSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentWindowSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentWindowSettingsCHK.Image = (useCurrentWindowSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentLogSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentLogSettingsCHK.Image = (useCurrentLogSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentFolderSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentFolderSettingsCHK.Image = (useCurrentFolderSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentVMSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentVMSettingsCHK.Image = (useCurrentVMSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentGSdxPluginSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentGSdxPluginSettingsCHK.Image = (useCurrentGSdxPluginSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentSPU2xPluginSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentSPU2xPluginSettingsCHK.Image = (useCurrentSPU2xPluginSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentLilyPadPluginSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentLilyPadPluginSettingsCHK.Image = (useCurrentLilyPadPluginSettingsCHK.Image != checkmark) ? checkmark : null;
        }
    }

    public class SettingsPlugin : ISystemMenuItemPlugin
    {
        private static SettingsForm settingsForm = null;

        public string Caption
        {
            get
            {
                return "PCSX2 Configurator Settings";
            }
        }

        public Image IconImage
        {
            get
            {
                return Utilities.EmulatorIcon.ToBitmap();
            }
        }

        public bool ShowInLaunchBox
        {
            get
            {
                return true;
            }
        }

        public bool ShowInBigBox
        {
            get
            {
                return false;
            }
        }

        public bool AllowInBigBoxWhenLocked
        {
            get
            {
                return ShowInBigBox;
            }
        }

        public void OnSelected()
        {
            if (settingsForm != null) settingsForm.Close();
            settingsForm = new SettingsForm();

            settingsForm.StartPosition = FormStartPosition.Manual;
            settingsForm.Location = new Point(
                PluginHelper.LaunchBoxMainForm.Location.X + (int)((PluginHelper.LaunchBoxMainForm.Width - settingsForm.Width) * 0.5f),
                PluginHelper.LaunchBoxMainForm.Location.Y + (int)((PluginHelper.LaunchBoxMainForm.Height - settingsForm.Height) * 0.5f));
            settingsForm.Show();
        }
    }
}
