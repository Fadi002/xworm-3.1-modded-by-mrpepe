using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Xpepemod
{
    public class ControlStyle
    {
        public event Action StyleChanged;

        private Color? _backColor;
        public Color? BackColor
        {
            get => _backColor;
            set { _backColor = value; OnStyleChanged(); }
        }

        private Color? _foreColor;
        public Color? ForeColor
        {
            get => _foreColor;
            set { _foreColor = value; OnStyleChanged(); }
        }

        private Font _font;
        public Font Font
        {
            get => _font;
            set { _font = value; OnStyleChanged(); }
        }

        private BorderStyle? _borderStyle;
        public BorderStyle? BorderStyle
        {
            get => _borderStyle;
            set { _borderStyle = value; OnStyleChanged(); }
        }

        private Padding? _padding;
        public Padding? Padding
        {
            get => _padding;
            set { _padding = value; OnStyleChanged(); }
        }

        private Padding? _margin;
        public Padding? Margin
        {
            get => _margin;
            set { _margin = value; OnStyleChanged(); }
        }

        private Cursor _cursor;
        public Cursor Cursor
        {
            get => _cursor;
            set { _cursor = value; OnStyleChanged(); }
        }

        private ContentAlignment? _textAlign;
        public ContentAlignment? TextAlign
        {
            get => _textAlign;
            set { _textAlign = value; OnStyleChanged(); }
        }

        private Image _image;
        public Image Image
        {
            get => _image;
            set { _image = value; OnStyleChanged(); }
        }

        private FlatStyle? _flatStyle;
        public FlatStyle? FlatStyle
        {
            get => _flatStyle;
            set { _flatStyle = value; OnStyleChanged(); }
        }

        private ContentAlignment? _checkAlign;
        public ContentAlignment? CheckAlign
        {
            get => _checkAlign;
            set { _checkAlign = value; OnStyleChanged(); }
        }

        private RightToLeft? _rightToLeft;
        public RightToLeft? RightToLeft
        {
            get => _rightToLeft;
            set { _rightToLeft = value; OnStyleChanged(); }
        }

        private Color? _borderColor;
        public Color? BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; OnStyleChanged(); }
        }

        private int _cornerRadius;
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; OnStyleChanged(); }
        }

        private void OnStyleChanged() => StyleChanged?.Invoke();
    }

    public class ThemeAPI
    {
        public ControlStyle Form { get; set; } = new ControlStyle();
        public ControlStyle ListBox { get; set; } = new ControlStyle();
        public ControlStyle Button { get; set; } = new ControlStyle();
        public ControlStyle TextBox { get; set; } = new ControlStyle();
        public ControlStyle ListView { get; set; } = new ControlStyle();
        public ControlStyle StatusStrip { get; set; } = new ControlStyle();
        public ControlStyle Label { get; set; } = new ControlStyle();
        public ControlStyle ComboBox { get; set; } = new ControlStyle();
        public ControlStyle Panel { get; set; } = new ControlStyle();
        public ControlStyle CheckBox { get; set; } = new ControlStyle();
        public ControlStyle RadioButton { get; set; } = new ControlStyle();
        public ControlStyle TabControl { get; set; } = new ControlStyle();
        public ControlStyle GroupBox { get; set; } = new ControlStyle();
        public ControlStyle ContextMenuStrip { get; set; } = new ControlStyle();
        public ControlStyle MenuStrip { get; set; } = new ControlStyle();
        public ControlStyle ToolStripDropDown { get; set; } = new ControlStyle();

        private List<ToolStrip> themedToolStrips = new List<ToolStrip>();
        private List<MenuStrip> themedMenuStrips = new List<MenuStrip>();

        public void ApplyTo(Control control)
        {
            ApplyControl(control);
            foreach (Control child in control.Controls)
                ApplyTo(child);
        }

        private void ApplyControl(Control control)
        {
            if (control is Form) ApplyStyle(control, Form);
            else if (control is ListBox) ApplyStyle(control, ListBox);
            else if (control is Button) ApplyStyle(control, Button);
            else if (control is TextBox) ApplyStyle(control, TextBox);
            else if (control is ListView) ApplyStyle(control, ListView);
            else if (control is StatusStrip) ApplyStyle(control, StatusStrip);
            else if (control is Label) ApplyStyle(control, Label);
            else if (control is ComboBox) ApplyStyle(control, ComboBox);
            else if (control is Panel) ApplyStyle(control, Panel);
            else if (control is CheckBox) ApplyStyle(control, CheckBox);
            else if (control is RadioButton) ApplyStyle(control, RadioButton);
            else if (control is TabControl) ApplyStyle(control, TabControl);
            else if (control is GroupBox) ApplyStyle(control, GroupBox);
            else if (control is MenuStrip menuStrip) ApplyMenuStrip(menuStrip);
            else if (control is ToolStrip toolStrip) ApplyToolStrip(toolStrip);

            if (control.ContextMenuStrip != null)
                ApplyContextMenuStrip(control.ContextMenuStrip);
        }

        private void ApplyStyle(Control control, ControlStyle style)
        {
            if (style.BackColor.HasValue) control.BackColor = style.BackColor.Value;
            if (style.ForeColor.HasValue) control.ForeColor = style.ForeColor.Value;
            if (style.Font != null) control.Font = style.Font;
        }

        private void ApplyMenuStrip(MenuStrip menuStrip)
        {
            if (!themedMenuStrips.Contains(menuStrip))
                themedMenuStrips.Add(menuStrip);

            UpdateMenuStrip(menuStrip);
        }

        private void ApplyToolStrip(ToolStrip toolStrip)
        {
            if (!themedToolStrips.Contains(toolStrip))
                themedToolStrips.Add(toolStrip);

            UpdateToolStrip(toolStrip);
        }

        private void UpdateToolStrip(ToolStrip toolStrip)
        {
            if (ToolStripDropDown.BackColor.HasValue)
                toolStrip.BackColor = ToolStripDropDown.BackColor.Value;
            if (ToolStripDropDown.ForeColor.HasValue)
                toolStrip.ForeColor = ToolStripDropDown.ForeColor.Value;
            if (ToolStripDropDown.Font != null)
                toolStrip.Font = ToolStripDropDown.Font;

            foreach (ToolStripItem item in toolStrip.Items)
            {
                if (item is ToolStripDropDownItem dropDownItem)
                    ApplyToolStripMenuItem(dropDownItem);
            }
        }

        private void UpdateMenuStrip(MenuStrip menuStrip)
        {
            if (MenuStrip.BackColor.HasValue)
                menuStrip.BackColor = MenuStrip.BackColor.Value;
            if (MenuStrip.ForeColor.HasValue)
                menuStrip.ForeColor = MenuStrip.ForeColor.Value;
            if (MenuStrip.Font != null)
                menuStrip.Font = MenuStrip.Font;

            foreach (ToolStripItem item in menuStrip.Items)
                ApplyToolStripMenuItem(item as ToolStripDropDownItem);
        }

        public void ApplyContextMenuStrip(ContextMenuStrip cms)
        {
            if (cms == null)
                return;

            if (ToolStripDropDown.BackColor.HasValue)
                cms.BackColor = ToolStripDropDown.BackColor.Value;

            if (ToolStripDropDown.ForeColor.HasValue)
                cms.ForeColor = ToolStripDropDown.ForeColor.Value;

            if (ToolStripDropDown.Font != null)
                cms.Font = ToolStripDropDown.Font;

            foreach (ToolStripItem item in cms.Items)
            {
                var menuItem = item as ToolStripDropDownItem;
                if (menuItem != null)
                    ApplyToolStripMenuItem(menuItem);
                else
                {
                    if (ToolStripDropDown.BackColor.HasValue)
                        item.BackColor = ToolStripDropDown.BackColor.Value;

                    if (ToolStripDropDown.ForeColor.HasValue)
                        item.ForeColor = ToolStripDropDown.ForeColor.Value;

                    if (ToolStripDropDown.Font != null)
                        item.Font = ToolStripDropDown.Font;
                }
            }
        }

        public void ApplyToolStripMenuItem(ToolStripDropDownItem item)
        {
            if (item == null)
                return;

            if (ToolStripDropDown.BackColor.HasValue)
                item.BackColor = ToolStripDropDown.BackColor.Value;

            if (ToolStripDropDown.ForeColor.HasValue)
                item.ForeColor = ToolStripDropDown.ForeColor.Value;

            if (ToolStripDropDown.Font != null)
                item.Font = ToolStripDropDown.Font;

            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                var dropDownItem = subItem as ToolStripDropDownItem;
                if (dropDownItem != null)
                    ApplyToolStripMenuItem(dropDownItem);
                else
                {
                    if (ToolStripDropDown.BackColor.HasValue)
                        subItem.BackColor = ToolStripDropDown.BackColor.Value;

                    if (ToolStripDropDown.ForeColor.HasValue)
                        subItem.ForeColor = ToolStripDropDown.ForeColor.Value;

                    if (ToolStripDropDown.Font != null)
                        subItem.Font = ToolStripDropDown.Font;
                }
            }
        }

    }

    internal static class FormThemeManager
    {
        private static readonly HashSet<Form> ThemedForms = new HashSet<Form>();

        public static void Init()
        {
            // Hook Application.Idle to detect new forms and apply theme
            Application.Idle += (s, e) =>
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (!ThemedForms.Contains(form))
                    {
                        HookFormEvents(form);
                        Xpepemod.Themes.ApplyTo(form);
                        ThemedForms.Add(form);
                    }
                }
            };
        }

        private static void HookFormEvents(Form form)
        {
            form.HandleCreated -= Form_HandleCreated;
            form.HandleCreated += Form_HandleCreated;

            form.Shown -= Form_Shown;
            form.Shown += Form_Shown;
        }

        private static void Form_HandleCreated(object sender, EventArgs e)
        {
            if (sender is Form form)
            {
                Xpepemod.Themes.ApplyTo(form);
            }
        }

        private static void Form_Shown(object sender, EventArgs e)
        {
            if (sender is Form form)
            {
                Xpepemod.Themes.ApplyTo(form);
            }
        }
    }

}
