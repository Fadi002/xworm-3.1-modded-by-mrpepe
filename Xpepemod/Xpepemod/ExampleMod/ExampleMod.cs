using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Xpepemod;

namespace Xpepemodtest
{
    public class Examplemod : IMod
    {
        public void OnLoad()
        {
            Xpepemod.Xpepemod.Log.Info("Examplemod loaded.");
            var theme = Xpepemod.Xpepemod.Themes;

            // Colors from Nord palette (adjusted for UI elements)
            var background = Color.FromArgb(46, 52, 64);         // Nord polar night (dark gray-blue)
            var panelBackground = Color.FromArgb(59, 66, 82);   // Slightly lighter panel
            var foreground = Color.FromArgb(216, 222, 233);     // Nord snowstorm (light gray)
            var accentBlue = Color.FromArgb(94, 129, 172);      // Nord frost (blue)
            var accentLight = Color.FromArgb(136, 192, 208);    // Light blue accent
            var borderColor = Color.FromArgb(68, 76, 94);       // Subtle border color
            var inputBackground = Color.FromArgb(59, 66, 82);
            var buttonBackground = Color.FromArgb(68, 76, 94);

            var defaultFont = new Font("Segoe UI", 9);

            // Helper roundness (if you apply this visually in your controls)
            int cornerRadius = 6;

            // Form
            theme.Form.BackColor = background;
            theme.Form.ForeColor = foreground;
            theme.Form.Font = defaultFont;
            theme.Form.Padding = new Padding(10);

            // Button
            theme.Button.BackColor = buttonBackground;
            theme.Button.ForeColor = foreground;
            theme.Button.Font = defaultFont;
            theme.Button.FlatStyle = FlatStyle.Flat;
            theme.Button.Padding = new Padding(8);
            theme.Button.Margin = new Padding(5);
            theme.Button.BorderColor = borderColor;  // <-- add this to ControlStyle (see note below)
            theme.Button.CornerRadius = cornerRadius; // <-- add this to ControlStyle

            // TextBox
            theme.TextBox.BackColor = inputBackground;
            theme.TextBox.ForeColor = foreground;
            theme.TextBox.Font = defaultFont;
            theme.TextBox.BorderStyle = BorderStyle.FixedSingle;
            theme.TextBox.Padding = new Padding(6);
            theme.TextBox.Margin = new Padding(5);
            theme.TextBox.BorderColor = borderColor;
            theme.TextBox.CornerRadius = cornerRadius;

            // Label
            theme.Label.ForeColor = foreground;
            theme.Label.Font = defaultFont;
            theme.Label.Margin = new Padding(2);

            // ListBox
            theme.ListBox.BackColor = panelBackground;
            theme.ListBox.ForeColor = foreground;
            theme.ListBox.Font = defaultFont;
            theme.ListBox.BorderStyle = BorderStyle.FixedSingle;
            theme.ListBox.BorderColor = borderColor;
            theme.ListBox.CornerRadius = cornerRadius;

            // ListView
            theme.ListView.BackColor = panelBackground;
            theme.ListView.ForeColor = foreground;
            theme.ListView.Font = defaultFont;
            theme.ListView.Margin = new Padding(4);
            theme.ListView.BorderColor = borderColor;
            theme.ListView.CornerRadius = cornerRadius;

            // ComboBox
            theme.ComboBox.BackColor = panelBackground;
            theme.ComboBox.ForeColor = foreground;
            theme.ComboBox.Font = defaultFont;
            theme.ComboBox.Margin = new Padding(4);
            theme.ComboBox.BorderColor = borderColor;
            theme.ComboBox.CornerRadius = cornerRadius;

            // StatusStrip
            theme.StatusStrip.BackColor = panelBackground;
            theme.StatusStrip.ForeColor = foreground;
            theme.StatusStrip.Font = defaultFont;
            theme.StatusStrip.Padding = new Padding(6);
            theme.StatusStrip.BorderColor = borderColor;
            theme.StatusStrip.CornerRadius = cornerRadius;

            // Panel
            theme.Panel.BackColor = background;
            theme.Panel.ForeColor = foreground;
            theme.Panel.Padding = new Padding(8);

            // CheckBox
            theme.CheckBox.ForeColor = foreground;
            theme.CheckBox.Font = defaultFont;
            theme.CheckBox.Margin = new Padding(4);

            // RadioButton
            theme.RadioButton.ForeColor = foreground;
            theme.RadioButton.Font = defaultFont;
            theme.RadioButton.Margin = new Padding(4);

            // TabControl
            theme.TabControl.BackColor = background;
            theme.TabControl.ForeColor = foreground;
            theme.TabControl.Font = defaultFont;
            theme.TabControl.BorderColor = borderColor;
            theme.TabControl.CornerRadius = cornerRadius;

            // GroupBox
            theme.GroupBox.BackColor = background;
            theme.GroupBox.ForeColor = foreground;
            theme.GroupBox.Font = defaultFont;
            theme.GroupBox.Padding = new Padding(8);
            theme.GroupBox.BorderColor = borderColor;
            theme.GroupBox.CornerRadius = cornerRadius;

            // ContextMenuStrip
            theme.ContextMenuStrip.BackColor = panelBackground;
            theme.ContextMenuStrip.ForeColor = foreground;
            theme.ContextMenuStrip.Font = defaultFont;
            theme.ContextMenuStrip.BorderColor = borderColor;
            theme.ContextMenuStrip.CornerRadius = cornerRadius;

            theme.MenuStrip.BackColor = panelBackground;
            theme.MenuStrip.ForeColor = foreground;
            theme.MenuStrip.Font = defaultFont;
            theme.MenuStrip.BorderColor = borderColor;

            // ToolStripDropDown
            theme.ToolStripDropDown.BackColor = panelBackground;
            theme.ToolStripDropDown.ForeColor = foreground;
            theme.ToolStripDropDown.Font = defaultFont;
            theme.ToolStripDropDown.BorderColor = borderColor;
            theme.ToolStripDropDown.CornerRadius = cornerRadius;

            // p00032e = the little dropmenu that has the builder and other stuff
            // p0002ff = the client menu (when you right click on a client)

            Xpepemod.Xpepemod.Events.AddFeature("p00032e", "Open Notepad", () =>
            {
                System.Diagnostics.Process.Start("notepad.exe");
            });

            Xpepemod.Xpepemod.Events.AddFeature("p0002ff", "🧪 Run Test", () =>
            {
                MessageBox.Show("Running test...");
            });

            Xpepemod.Xpepemod.Events.AddFeature("p00032e", "Downloader/Js payload", () =>
            {
                MessageBox.Show("...");
            }, icon: Image.FromFile("shell.png"), shortcut: Keys.F5, tooltip: "Generate JS downloader");

            Xpepemod.Xpepemod.Events.AddFeature("p00032e", "Downloader/---", null);
            
            Xpepemod.Xpepemod.Events.AddFeature("p00032e", "Downloader/vbs payload", () =>
            {
                MessageBox.Show("...");
            }, icon: Image.FromFile("shell.png"), shortcut: Keys.F7, tooltip: "Generate VBS downloader");

            Xpepemod.Xpepemod.Events.AddFeature("p00032e", "Downloader/Powershell payload", () =>
            {
                MessageBox.Show("...");
            }, icon: Image.FromFile("shell.png"), shortcut: Keys.F6, tooltip: "Generate PS1 downloader");

            


            Xpepemod.Xpepemod.Log.Info("Dark theme applied.");
        }
    }
}
