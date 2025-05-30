# Xpepemod

A modding framework for Windows Forms applications.

## Features

- Theme customization for Windows Forms controls
- Event handling and menu customization
- Advanced logging system with colored output
- Mod loading system

## Documentation

Comprehensive API documentation is available in the `docs` directory:

- [HTML Documentation](docs/API.html) - Interactive documentation with syntax highlighting
- [Markdown Documentation](docs/API.md) - Plain text documentation

You can also view the documentation by opening `docs/index.html` in your browser.

## Logging System

Xpepemod includes a powerful logging system with multiple severity levels and color-coded output.

### Log Levels

The logging system supports the following levels (from lowest to highest severity):

1. **Trace** (Dark Gray) - Very detailed information, useful for debugging
2. **Debug** (Gray) - Debugging information
3. **Info** (White) - General information
4. **Warning** (Yellow) - Potential issues that don't prevent operation
5. **Error** (Red) - Errors that may impact functionality
6. **Critical** (Dark Red) - Critical errors that prevent operation
7. **None** - No logging

### Using the Logging System

```csharp
// Access the logging system
var log = Xpepemod.Xpepemod.Log;

// Configure logging
log.MinimumLevel = Xpepemod.LogLevel.Debug; // Only show Debug and higher levels
log.UseColors = true;                       // Enable colored output
log.ShowTimestamp = true;                   // Show timestamps in logs

// Log messages at different levels
log.Trace("Very detailed information");
log.Debug("Debugging information");
log.Info("General information");
log.Warning("Warning message");
log.Error("Error message");
log.Critical("Critical error");
```

### Configuring Log Levels

You can control which messages are displayed by setting the `MinimumLevel` property:

```csharp
// Only show warnings and more severe messages
Xpepemod.Xpepemod.Log.MinimumLevel = Xpepemod.LogLevel.Warning;

// Show all messages including trace
Xpepemod.Xpepemod.Log.MinimumLevel = Xpepemod.LogLevel.Trace;

// Disable logging completely
Xpepemod.Xpepemod.Log.MinimumLevel = Xpepemod.LogLevel.None;
```

## Creating Mods

To create a mod for Xpepemod:

1. Create a new Class Library project targeting .NET Framework 4.7.2
2. Add a reference to Xpepemod.dll
3. Create a class that implements the `Xpepemod.IMod` interface
4. Implement the `OnLoad()` method
5. Set the output path to the Mods folder (e.g., `..\bin\Debug\Mods\`)

### Example Mod

The solution includes an ExampleMod project that demonstrates how to create a mod for Xpepemod. The ExampleMod:

1. Uses the logging system with different levels
2. Applies a dark theme to the application
3. Adds a menu item to the context menu

```csharp
using System;
using System.Windows.Forms;
using System.Drawing;

namespace ExampleMod
{
    public class ExampleMod : IMod
    {
        public void OnLoad()
        {
            // Access the logging system with different levels
            Xpepemod.Xpepemod.Log.Info("ExampleMod loaded successfully");

            // Debug information (only visible if MinimumLevel is set to Debug or lower)
            Xpepemod.Xpepemod.Log.Debug("This is debug information from ExampleMod");

            // Set up a dark theme
            SetupDarkTheme();

            // Add a menu item to the context menu
            Xpepemod.Xpepemod.Events.AddFeature(
                "contextMenuStrip1",  // The name of the control in Form1
                "Example/Show Message", // Menu path
                () => MessageBox.Show("Hello from ExampleMod!"),
                null,
                Keys.None,
                "Shows a message from ExampleMod"
            );
        }

        private void SetupDarkTheme()
        {
            try
            {
                // Set up a dark theme
                var themes = Xpepemod.Xpepemod.Themes;

                // Form background
                themes.Form.BackColor = Color.FromArgb(30, 30, 30);
                themes.Form.ForeColor = Color.White;

                // Controls
                themes.Button.BackColor = Color.FromArgb(60, 60, 60);
                themes.Button.ForeColor = Color.White;

                Xpepemod.Xpepemod.Log.Info("Dark theme applied");
            }
            catch (Exception ex)
            {
                // Log errors with the Error level
                Xpepemod.Xpepemod.Log.Error($"Failed to apply dark theme: {ex.Message}");
            }
        }
    }
}
```

## License

Copyright © 2025
