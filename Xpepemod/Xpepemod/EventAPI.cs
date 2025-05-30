using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace Xpepemod
{
    public class EventAPI
    {
        private Form mainForm;
        private bool initialized = false;

        private readonly List<QueuedFeature> queued = new List<QueuedFeature>();

        public void Initialize(Form form)
        {
            mainForm = form;
            initialized = true;
            ApplyQueuedFeatures();
        }

        public void AddFeature(string controlName, string featureName, Action onClick, Image icon = null, Keys shortcut = Keys.None, string tooltip = null)
        {
            if (!initialized || mainForm == null || !mainForm.IsHandleCreated)
            {
                queued.Add(new QueuedFeature
                {
                    Target = controlName,
                    FeatureName = featureName,
                    Callback = onClick,
                    Icon = icon,
                    Shortcut = shortcut,
                    Tooltip = tooltip
                });

                Application.Idle -= WaitForFormReady;
                Application.Idle += WaitForFormReady;
                return;
            }

            ApplyFeature(controlName, featureName, onClick, icon, shortcut, tooltip);
        }

        private void WaitForFormReady(object sender, EventArgs e)
        {
            if (mainForm == null)
            {
                mainForm = Application.OpenForms
                    .Cast<Form>()
                    .FirstOrDefault(f => f.GetType().Name == "Form1");

                if (mainForm == null)
                    return; // Keep waiting
            }

            if (!mainForm.IsHandleCreated)
                return;

            initialized = true;
            Application.Idle -= WaitForFormReady;
            ApplyQueuedFeatures();
        }

        private void ApplyQueuedFeatures()
        {
            foreach (var f in queued.ToList())
            {
                ApplyFeature(f.Target, f.FeatureName, f.Callback, f.Icon, f.Shortcut, f.Tooltip);
                queued.Remove(f);
            }
        }

        private void ApplyFeature(string memberName, string path, Action action, Image icon = null, Keys shortcut = Keys.None, string tooltip = null)
        {
            object rawControl = null;

            var member = mainForm.GetType()
                .GetMembers(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .FirstOrDefault(m =>
                    m.Name == memberName &&
                    (
                        (m is FieldInfo fi && typeof(object).IsAssignableFrom(fi.FieldType)) ||
                        (m is PropertyInfo pi && typeof(object).IsAssignableFrom(pi.PropertyType))
                    ));

            if (member == null)
            {
                if (initialized)
                    Xpepemod.Log.Warning($"Couldn't find control: '{memberName}'");
                return;
            }

            if (member is FieldInfo field)
                rawControl = field.GetValue(mainForm);
            else if (member is PropertyInfo prop)
                rawControl = prop.GetValue(mainForm);

            ToolStripItemCollection topLevelItems = null;

            if (rawControl is ToolStripDropDownItem dropdown)
            {
                topLevelItems = dropdown.DropDownItems;
            }
            else if (rawControl is ContextMenuStrip contextMenu)
            {
                topLevelItems = contextMenu.Items;
            }
            else
            {
                Xpepemod.Log.Warning($"'{memberName}' is not a supported control (dropdown or context menu).");
                return;
            }

            // Find or create "User Features" root
            ToolStripMenuItem userFeatures = topLevelItems
                .OfType<ToolStripMenuItem>()
                .FirstOrDefault(item => item.Text == "User Features");

            if (userFeatures == null)
            {
                topLevelItems.Add(new ToolStripSeparator());
                userFeatures = new ToolStripMenuItem("User Features");
                topLevelItems.Add(userFeatures);
            }

            // Build nested menu path (e.g., "Inject Tools/Payload 1")
            var parts = path.Split('/');
            ToolStripMenuItem parent = userFeatures;

            for (int i = 0; i < parts.Length; i++)
            {
                string current = parts[i];

                if (current == "---")
                {
                    parent.DropDownItems.Add(new ToolStripSeparator());
                    continue;
                }

                if (i == parts.Length - 1)
                {
                    var featureItem = new ToolStripMenuItem(current);
                    if (icon != null)
                        featureItem.Image = icon;
                    if (shortcut != Keys.None)
                        featureItem.ShortcutKeys = shortcut;
                    if (!string.IsNullOrEmpty(tooltip))
                        featureItem.ToolTipText = tooltip;

                    featureItem.Click += (s, e) => action?.Invoke();
                    parent.DropDownItems.Add(featureItem);
                }
                else
                {
                    var nextParent = parent.DropDownItems
                        .OfType<ToolStripMenuItem>()
                        .FirstOrDefault(item => item.Text == current);

                    if (nextParent == null)
                    {
                        nextParent = new ToolStripMenuItem(current);
                        parent.DropDownItems.Add(nextParent);
                    }

                    parent = nextParent;
                }
            }
        }




        private class QueuedFeature
        {
            public string Target;
            public string FeatureName;
            public Action Callback;
            public Image Icon { get; set; }
            public Keys Shortcut { get; set; }
            public string Tooltip { get; set; }
        }
    }
}
