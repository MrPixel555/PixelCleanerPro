# PixelCleaner Pro
PixelCleaner Pro is a Windows application for managing and cleaning system cache files, developed with C# and WinForms. This application helps users identify, manage, and clean temporary files and system cache.
Main features:
# 1. System scan:
o Search for cache files in default paths (AppData, Temp, etc.)
o Ability to add custom paths
o Identify cache files based on name patterns (cache, tmp, temp, etc.)
# 2. File management:
o Display a list of cache files with details (name, size, associated program, path, and modification date)
o Ability to select files to clean
o Filter by program and minimum size
# 3. Visual features:
o Pie chart to display the distribution of file sizes by programs
o Dark and light theme support
o Progress bar to display scan status
# 4. Security:
o Need to run with Administrator access
o Logging of operations
# 5. Other features:
o Ability to export reports to CSV format
o Display scan errors (in developer mode)
o Log console for debugging
• .NET Framework 4.8
• Windows Forms
• System.Windows.Forms.DataVisualization for charts
# Notes:
• The application uses the async pattern for scanning operations so that the UI does not block.
• Optimal memory management with GC.Collect while scanning large files.
• Responsive UI using Invoke to update the UI from other threads.
This program is useful for users who want to free up disk space by cleaning temporary and cache files, or developers who want to check the cache files of various applications.

