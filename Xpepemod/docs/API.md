# Xpepemod API Documentation

Xpepemod is a modding framework for Xworm v3.1 that provides theming, event handling, and logging capabilities.

## Table of Contents

- [Logging API](#logging-api)
- [Theme API](#theme-api)
- [Event API](#event-api)
- [Mod Interface](#mod-interface)
- [Mod Loader](#mod-loader)


## Logging API

The logging system provides multiple severity levels with color-coded output.

```csharp
namespace Xpepemod
{
    public enum LogLevel
    {
        Trace = 0,
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4,
        Critical = 5,
        None = 6
    }

    public class LogAPI
    {
        public LogLevel MinimumLevel { get; set; }
        public bool UseColors { get; set; }
        public bool ShowTimestamp { get; set; }

        public void Trace(string message);
        public void Debug(string message);
        public void Info(string message);
        public void Warning(string message);
        public void Error(string message);
        public void Critical(string message);
    }
}
```

### Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `MinimumLevel` | `LogLevel` | `LogLevel.Info` | The minimum severity level of messages to display |
| `UseColors` | `bool` | `true` | Whether to use colors in the console output |
| `ShowTimestamp` | `bool` | `false` | Whether to include timestamps in log messages |

### Methods

| Method | Description | Output Format |
|--------|-------------|--------------|
| `Trace(string message)` | Logs a trace message (dark gray) | `[Xpepemod][TRACE] message` |
| `Debug(string message)` | Logs a debug message (gray) | `[Xpepemod][DEBUG] message` |
| `Info(string message)` | Logs an informational message (white) | `[Xpepemod][INFO] message` |
| `Warning(string message)` | Logs a warning message (yellow) | `[Xpepemod][WARNING] message` |
| `Error(string message)` | Logs an error message (red) | `[Xpepemod][ERROR] message` |
| `Critical(string message)` | Logs a critical error message (dark red) | `[Xpepemod][CRITICAL] message` |

### Log Levels

| Level | Value | Color | Description |
|-------|-------|-------|-------------|
| `Trace` | 0 | Dark Gray | Very detailed information, useful for debugging |
| `Debug` | 1 | Gray | Debugging information |
| `Info` | 2 | White | General information |
| `Warning` | 3 | Yellow | Potential issues that don't prevent operation |
| `Error` | 4 | Red | Errors that may impact functionality |
| `Critical` | 5 | Dark Red | Critical errors that prevent operation |
| `None` | 6 | N/A | No logging |

## Theme API

The theming system allows customization of the appearance of Windows Forms controls.

```csharp
namespace Xpepemod
{
    public class ControlStyle
    {
        public event Action StyleChanged;

        public Color? BackColor { get; set; }
        public Color? ForeColor { get; set; }
        public Font Font { get; set; }
        public BorderStyle? BorderStyle { get; set; }
        public Padding? Padding { get; set; }
        public Color? BorderColor { get; set; }
        public int CornerRadius { get; set; }
        // Additional properties...
    }

    public class ThemeAPI
    {
        public ControlStyle Form { get; set; }
        public ControlStyle ListBox { get; set; }
        public ControlStyle Button { get; set; }
        public ControlStyle TextBox { get; set; }
        public ControlStyle ListView { get; set; }
        public ControlStyle StatusStrip { get; set; }
        public ControlStyle Label { get; set; }
        public ControlStyle ComboBox { get; set; }
        public ControlStyle Panel { get; set; }
        public ControlStyle CheckBox { get; set; }
        public ControlStyle RadioButton { get; set; }
        public ControlStyle TabControl { get; set; }
        public ControlStyle GroupBox { get; set; }
        public ControlStyle ContextMenuStrip { get; set; }
        public ControlStyle MenuStrip { get; set; }
        public ControlStyle ToolStripDropDown { get; set; }

        public void ApplyTo(Control control);
    }
}
```

### ControlStyle Properties

| Property | Type | Description |
|----------|------|-------------|
| `BackColor` | `Color?` | Background color of the control |
| `ForeColor` | `Color?` | Text color of the control |
| `Font` | `Font` | Font used for text in the control |
| `BorderStyle` | `BorderStyle?` | Border style of the control |
| `Padding` | `Padding?` | Padding inside the control |
| `BorderColor` | `Color?` | Color of the control's border |
| `CornerRadius` | `int` | Radius of rounded corners (if supported) |

### ThemeAPI Properties

Each property represents a style for a specific type of control:

| Property | Type | Description |
|----------|------|-------------|
| `Form` | `ControlStyle` | Style for Form controls |
| `ListBox` | `ControlStyle` | Style for ListBox controls |
| `Button` | `ControlStyle` | Style for Button controls |
| `TextBox` | `ControlStyle` | Style for TextBox controls |
| `ListView` | `ControlStyle` | Style for ListView controls |
| `StatusStrip` | `ControlStyle` | Style for StatusStrip controls |
| `Label` | `ControlStyle` | Style for Label controls |
| `ComboBox` | `ControlStyle` | Style for ComboBox controls |
| `Panel` | `ControlStyle` | Style for Panel controls |
| `CheckBox` | `ControlStyle` | Style for CheckBox controls |
| `RadioButton` | `ControlStyle` | Style for RadioButton controls |
| `TabControl` | `ControlStyle` | Style for TabControl controls |
| `GroupBox` | `ControlStyle` | Style for GroupBox controls |
| `ContextMenuStrip` | `ControlStyle` | Style for ContextMenuStrip controls |
| `MenuStrip` | `ControlStyle` | Style for MenuStrip controls |
| `ToolStripDropDown` | `ControlStyle` | Style for ToolStripDropDown controls |

### ThemeAPI Methods

| Method | Description |
|--------|-------------|
| `ApplyTo(Control control)` | Applies the theme to the specified control and all its child controls |

## Event API

The event handling system allows adding custom functionality to the application.

```csharp
namespace Xpepemod
{
    public class EventAPI
    {
        public void Initialize(Form form);
        public void AddFeature(string controlName, string featureName, Action onClick,
                              Image icon = null, Keys shortcut = Keys.None, string tooltip = null);
    }
}
```

### Methods

| Method | Description |
|--------|-------------|
| `Initialize(Form form)` | Initializes the event system with the main form |
| `AddFeature(string controlName, string featureName, Action onClick, Image icon = null, Keys shortcut = Keys.None, string tooltip = null)` | Adds a custom feature to a control |

### AddFeature Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `controlName` | `string` | The name of the control to add the feature to (usually a menu or context menu) |
| `featureName` | `string` | The name/path of the feature (can include submenus using '/' as a separator) |
| `onClick` | `Action` | The action to perform when the feature is activated |
| `icon` | `Image` | Optional icon to display next to the feature |
| `shortcut` | `Keys` | Optional keyboard shortcut for the feature |
| `tooltip` | `string` | Optional tooltip text for the feature |

## Mod Interface

The mod interface defines the contract for creating mods for Xpepemod.

```csharp
namespace Xpepemod
{
    public interface IMod
    {
        void OnLoad();
    }
}
```

### Methods

| Method | Description |
|--------|-------------|
| `OnLoad()` | Called when the mod is loaded by the mod loader |

## Mod Loader

The mod loader is responsible for loading mods from the Mods directory.

```csharp
namespace Xpepemod
{
    public static class ModLoader
    {
        public static void LoadMods();
    }
}
```

### Methods

| Method | Description |
|--------|-------------|
| `LoadMods()` | Loads all mods from the Mods directory |

### Mod Loading Process

1. The mod loader looks for DLL files in the `Mods` directory
2. For each DLL, it loads the assembly and searches for types that implement the `IMod` interface
3. For each mod type found, it creates an instance and calls the `OnLoad()` method
4. If an error occurs during loading, it logs the error and continues with the next mod

## Example Usage

### Creating a Simple Mod

```csharp
using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyMod
{
    public class MyMod : Xpepemod.IMod
    {
        public void OnLoad()
        {
            // Log information
            Xpepemod.Xpepemod.Log.Info("MyMod loaded successfully");

            // p00032e = the little dropmenu that has the builder and other stuff
            // p0002ff = the client menu (when you right click on a client)
            Xpepemod.Xpepemod.Events.AddFeature(
                "p00032e",
                "My Mod/Do Something",
                () => MessageBox.Show("Hello from MyMod!"),
                null,
                Keys.None,
                "Does something cool"
            );

            // Customize the theme
            Xpepemod.Xpepemod.Themes.Button.BackColor = Color.DarkBlue;
            Xpepemod.Xpepemod.Themes.Button.ForeColor = Color.White;
        }
    }
}
```
