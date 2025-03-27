using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
<<<<<<< HEAD
using System.Drawing;
=======
using System.Threading;
using System.Drawing;
using System.Security.Principal;
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
<<<<<<< HEAD
using System.Security.Principal;
=======
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PixelCleanerPro
{
    public partial class Form1 : Form
    {
        private ProgressBar progressBar;
        private Label statusLabel;
        private Label totalSizeLabel;
        private ListView cacheListView;
        private Button scanButton;
        private Button cleanButton;
        private Button exportButton;
        private Button settingsButton;
        private CheckBox darkModeCheck;
        private ComboBox filterProgramCombo;
        private NumericUpDown minSizeFilter;
        private Chart sizeChart;
        private CheckBox developerLogsCheck;
<<<<<<< HEAD
        private CheckBox selectAllCheck;

=======
        
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
        private List<CacheFile> cacheFiles = new List<CacheFile>();
        private List<string> scanErrors = new List<string>();
        private string[] customPaths = Array.Empty<string>();
        private string logFilePath = Path.Combine(Application.StartupPath, "PixelCleanerLog.txt");
        private volatile bool isCancelled;

<<<<<<< HEAD
=======
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
        public Form1()
        {
            if (!IsRunningAsAdministrator())
            {
<<<<<<< HEAD
                MessageBox.Show("This application requires Administrator privileges to run.", "Administrator Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
=======
                MessageBox.Show("This application requires Administrator privileges to run. Please run it as an Administrator.", "Administrator Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                Environment.Exit(1);
            }

            InitializeComponent();
            InitializeUI();
            InitializeLogging();
            isCancelled = false;
        }

        private bool IsRunningAsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private void InitializeLogging()
        {
            logFilePath = Path.Combine(Application.StartupPath, "PixelCleaner.log");
            try
            {
                if (!File.Exists(logFilePath))
                    File.Create(logFilePath).Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating log file: {ex.Message}");
            }
        }

        private void LogMessage(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, $"[{DateTime.Now}] {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing to log file: {ex.Message}");
            }
        }

<<<<<<< HEAD
        private void InitializeUI()
        {
            this.Text = "PixelCleaner Pro (1.0.0.7.45)";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.Size = new Size(1230, 540);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(242, 243, 244);

            progressBar = new ProgressBar
            {
                Location = new Point(20, 20),
                Size = new Size(1180, 8),
                Style = ProgressBarStyle.Continuous,
                ForeColor = Color.FromArgb(0, 120, 215),
                BackColor = Color.FromArgb(225, 225, 225)
            };

            statusLabel = new Label
            {
                Location = new Point(20, 40),
                Size = new Size(400, 20),
                Text = "Status: Ready",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(31, 31, 31)
            };
            totalSizeLabel = new Label
            {
                Location = new Point(430, 40),
                Size = new Size(200, 20),
                Text = "Total Size: 0 MB",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(31, 31, 31)
            };

            selectAllCheck = new CheckBox
            {
                Text = "Select All",
                Location = new Point(20, 70),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 9F),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(31, 31, 31)
            };
            selectAllCheck.CheckedChanged += SelectAllCheck_CheckedChanged;

            cacheListView = new ListView
            {
                Location = new Point(20, 100),
                Size = new Size(760, 360),
                View = View.Details,
                CheckBoxes = true,
                FullRowSelect = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(31, 31, 31)
            };
            cacheListView.Columns.AddRange(new ColumnHeader[]
            {
                new ColumnHeader { Text = "File Name", Width = 150 },
                new ColumnHeader { Text = "Size (MB)", Width = 80 },
                new ColumnHeader { Text = "Program", Width = 150 },
                new ColumnHeader { Text = "Path", Width = 275 },
                new ColumnHeader { Text = "Last Modified", Width = 103 }
            });
            cacheListView.OwnerDraw = true;
            cacheListView.DrawColumnHeader += (s, e) =>
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 120, 215)), e.Bounds);
                e.Graphics.DrawString(e.Header.Text, new Font("Segoe UI", 9F, FontStyle.Bold), Brushes.White, e.Bounds, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            };
            cacheListView.DrawItem += (s, e) =>
            {
                e.DrawBackground();
                if (e.ItemIndex % 2 == 0)
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(245, 245, 245)), e.Bounds);

                Rectangle checkBoxRect = new Rectangle(e.Bounds.Left + 2, e.Bounds.Top + (e.Bounds.Height - 16) / 2, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkBoxRect, e.Item.Checked ? ButtonState.Checked : ButtonState.Normal);

                using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
                {
                    Rectangle firstColumnBounds = new Rectangle(e.Bounds.X + 22, e.Bounds.Y, cacheListView.Columns[0].Width - 22, e.Bounds.Height);
                    e.Graphics.DrawString(e.Item.Text, e.Item.Font, Brushes.Red, firstColumnBounds, sf);

                    int offsetX = cacheListView.Columns[0].Width;
                    for (int i = 1; i < e.Item.SubItems.Count; i++)
                    {
                        var subItem = e.Item.SubItems[i];
                        Rectangle subItemBounds = new Rectangle(e.Bounds.X + offsetX, e.Bounds.Y, cacheListView.Columns[i].Width, e.Bounds.Height);
                        e.Graphics.DrawString(subItem.Text, e.Item.Font, Brushes.Red, subItemBounds, sf);
                        offsetX += cacheListView.Columns[i].Width;
                    }
                }
            };

            sizeChart = new Chart
            {
                Location = new Point(800, 100),
                Size = new Size(400, 360),
                ChartAreas = { new ChartArea("MainArea") { BackColor = Color.Transparent } },
                Series = { new Series("CacheSize") { ChartType = SeriesChartType.Pie, BorderWidth = 2 } },
                BackColor = Color.Transparent
            };

            scanButton = CreateFluentButton("Scan", new Point(20, 470));
            cleanButton = CreateFluentButton("Clean", new Point(150, 470));
            exportButton = CreateFluentButton("Export Report", new Point(280, 470));
            settingsButton = CreateFluentButton("Add Folder", new Point(410, 470));

            darkModeCheck = new CheckBox
            {
                Text = "Dark Mode",
                Location = new Point(540, 480),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 9F),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(31, 31, 31)
            };
            developerLogsCheck = new CheckBox
            {
                Text = "Developer Logs",
                Location = new Point(540, 463),
                Size = new Size(110, 20),
                Font = new Font("Segoe UI", 9F),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(31, 31, 31)
            };
            filterProgramCombo = new ComboBox
            {
                Location = new Point(670, 472),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(31, 31, 31)
            };
            minSizeFilter = new NumericUpDown
            {
                Location = new Point(934, 475),
                Size = new Size(80, 25),
                Minimum = 0,
                Maximum = 100000,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(31, 31, 31)
            };

            this.Controls.Add(progressBar);
            this.Controls.Add(statusLabel);
            this.Controls.Add(totalSizeLabel);
            this.Controls.Add(selectAllCheck);
            this.Controls.Add(cacheListView);
            this.Controls.Add(sizeChart);
            this.Controls.Add(new Label { Text = "Min Size (MB):", Location = new Point(850, 475), Size = new Size(84, 20), Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(31, 31, 31) });
            this.Controls.Add(filterProgramCombo);
            this.Controls.Add(minSizeFilter);
            this.Controls.Add(developerLogsCheck);
            this.Controls.Add(darkModeCheck);
            this.Controls.Add(settingsButton);
            this.Controls.Add(exportButton);
            this.Controls.Add(cleanButton);
            this.Controls.Add(scanButton);

            scanButton.Click += ScanButton_Click;
            cleanButton.Click += CleanButton_Click;
            exportButton.Click += ExportButton_Click;
            settingsButton.Click += SettingsButton_Click;
            darkModeCheck.CheckedChanged += DarkModeCheck_CheckedChanged;
            filterProgramCombo.SelectedIndexChanged += FilterProgramCombo_SelectedIndexChanged;
            minSizeFilter.ValueChanged += MinSizeFilter_ValueChanged;
            sizeChart.PostPaint += DrawCustomPieLabels;

            var chartToolTip = new ToolTip();
            sizeChart.MouseMove += (s, e) =>
            {
                var hitTest = sizeChart.HitTest(e.X, e.Y);
                if (hitTest?.ChartElementType == ChartElementType.DataPoint && hitTest.PointIndex >= 0)
                {
                    var point = sizeChart.Series["CacheSize"].Points[hitTest.PointIndex];
                    chartToolTip.SetToolTip(sizeChart, point.ToolTip);
                }
                else
                {
                    chartToolTip.SetToolTip(sizeChart, "");
                }
            };

            ApplyTheme(false);
        }

        private Button CreateFluentButton(string text, Point location)
        {
            var button = new Button
            {
                Text = text,
                Location = location,
                Size = new Size(120, 35),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            button.MouseEnter += (s, e) =>
            {
                if (darkModeCheck.Checked)
                    button.BackColor = Color.FromArgb(0, 255, 128);
                else
                    button.BackColor = Color.FromArgb(0, 150, 255);
            };
            button.MouseLeave += (s, e) =>
            {
                if (darkModeCheck.Checked)
                    button.BackColor = Color.FromArgb(0, 204, 106);
                else
                    button.BackColor = Color.FromArgb(0, 120, 215);
            };
            return button;
        }
=======
private void InitializeUI()
{
    this.Text = "PixelCleaner Pro (1.0.0.6.29)";
    this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
    this.Size = new Size(1230, 540);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.MaximizeBox = false;
    this.FormBorderStyle = FormBorderStyle.FixedSingle;

    progressBar = new ProgressBar { Location = new Point(10, 10), Width = 1195, Height = 20 };
    
    statusLabel = new Label { Location = new Point(10, 35), Width = 400, Text = "Status: Ready" };
    totalSizeLabel = new Label { Location = new Point(420, 35), Width = 200, Text = "Total Size: 0 MB" };

    var selectAllCheck = new CheckBox 
    { 
        Text = "Select All", 
        Location = new Point(10, 60), 
        Width = 100 
    };
    selectAllCheck.CheckedChanged += SelectAllCheck_CheckedChanged;

    cacheListView = new ListView
    {
        Location = new Point(10, 85),
        Size = new Size(760, 363),
        View = View.Details,
        CheckBoxes = true,
        FullRowSelect = true
    };
    cacheListView.Columns.AddRange(new ColumnHeader[]
    {
        new ColumnHeader { Text = "File Name", Width = 150 },
        new ColumnHeader { Text = "Size (MB)", Width = 80 },
        new ColumnHeader { Text = "Program", Width = 150 },
        new ColumnHeader { Text = "Path", Width = 275 },
        new ColumnHeader { Text = "Last Modified", Width = 100 }
    });

    sizeChart = new Chart
    {
        Location = new Point(800, 60),
        Size = new Size(400, 400),
        ChartAreas = { new ChartArea("MainArea") },
        Series = { new Series("CacheSize") { 
            ChartType = SeriesChartType.Pie,
            IsValueShownAsLabel = false
        }}
    };

    scanButton = new Button { Text = "Scan", Location = new Point(10, 470), Width = 100 };
    cleanButton = new Button { Text = "Clean", Location = new Point(120, 470), Width = 100 };
    exportButton = new Button { Text = "Export Report", Location = new Point(230, 470), Width = 100 };
    settingsButton = new Button { Text = "Add Folder", Location = new Point(340, 470), Width = 100 };
    darkModeCheck = new CheckBox { Text = "Dark Mode", Location = new Point(450, 470), Width = 100 };
    developerLogsCheck = new CheckBox { Text = "Developer Logs", Location = new Point(450, 450), Width = 110 };
    filterProgramCombo = new ComboBox { Location = new Point(560, 470), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
    minSizeFilter = new NumericUpDown { Location = new Point(720, 470), Width = 80, Minimum = 0, Maximum = 10000 };

    this.Controls.AddRange(new Control[] {
        progressBar, statusLabel, totalSizeLabel, selectAllCheck, cacheListView, sizeChart,
        scanButton, cleanButton, exportButton, settingsButton, darkModeCheck,
        developerLogsCheck, filterProgramCombo, minSizeFilter,
        new Label { Text = "Min Size (MB):", Location = new Point(720, 450), Width = 100 }
    });

    scanButton.Click += ScanButton_Click;
    cleanButton.Click += CleanButton_Click;
    exportButton.Click += ExportButton_Click;
    settingsButton.Click += SettingsButton_Click;
    darkModeCheck.CheckedChanged += DarkModeCheck_CheckedChanged;
    filterProgramCombo.SelectedIndexChanged += FilterProgramCombo_SelectedIndexChanged;
    minSizeFilter.ValueChanged += MinSizeFilter_ValueChanged;
    sizeChart.PostPaint += DrawCustomPieLabels;

    var chartToolTip = new ToolTip();
    sizeChart.MouseMove += (s, e) => {
        var hitTest = sizeChart.HitTest(e.X, e.Y);
        if (hitTest?.ChartElementType == ChartElementType.DataPoint && hitTest.PointIndex >= 0)
        {
            var point = sizeChart.Series["CacheSize"].Points[hitTest.PointIndex];
            chartToolTip.SetToolTip(sizeChart, point.ToolTip);
        }
        else
        {
            chartToolTip.SetToolTip(sizeChart, "");
        }
    };

    ApplyTheme(false);
}



>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac

        private void DrawCustomPieLabels(object sender, ChartPaintEventArgs e)
        {
            if (sizeChart.Series["CacheSize"].Points.Count == 0) return;

            var graphics = e.ChartGraphics.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            RectangleF plotArea = e.ChartGraphics.GetAbsoluteRectangle(sizeChart.ChartAreas["MainArea"].Position.ToRectangleF());
            float centerX = plotArea.Left + plotArea.Width / 2;
            float centerY = plotArea.Top + plotArea.Height / 2;
            float radius = Math.Min(plotArea.Width, plotArea.Height) / 2 * 0.7f;

            float startAngle = 0;
            foreach (DataPoint point in sizeChart.Series["CacheSize"].Points)
            {
                float sweepAngle = (float)(360 * (point.YValues[0] / sizeChart.Series["CacheSize"].Points.Sum(p => p.YValues[0])));
                float midAngle = startAngle + sweepAngle / 2;

                point.ToolTip = $"{point.LegendText}\nSize: {point.YValues[0]:F2}MB\nPercent: {(point.YValues[0] / sizeChart.Series["CacheSize"].Points.Sum(p => p.YValues[0]) * 100):F1}%";
<<<<<<< HEAD

=======
                
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                float textRadius = radius * 0.5f;
                float textX = centerX + textRadius * (float)Math.Cos(midAngle * Math.PI / 180);
                float textY = centerY + textRadius * (float)Math.Sin(midAngle * Math.PI / 180);

                string labelText = $"{point.YValues[0]:F1}MB\n{(point.YValues[0] / sizeChart.Series["CacheSize"].Points.Sum(p => p.YValues[0]) * 100):F0}%";
                float fontSize = CalculateAdaptiveFontSize(sweepAngle, labelText.Length);
<<<<<<< HEAD

                using (Font font = new Font("Segoe UI", fontSize, FontStyle.Bold))
                using (Brush textBrush = new SolidBrush(darkModeCheck.Checked ? Color.White : Color.FromArgb(31, 31, 31)))
                {
                    SizeF textSize = graphics.MeasureString(labelText, font);
                    GraphicsState state = graphics.Save();
                    graphics.TranslateTransform(textX, textY);
                    graphics.RotateTransform(midAngle > 180 ? midAngle + 180 : midAngle);
                    graphics.DrawString(
                        labelText,
                        font,
                        textBrush,
                        new RectangleF(-textSize.Width / 2, -textSize.Height / 2, textSize.Width, textSize.Height),
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
                    graphics.Restore(state);
                }

                startAngle += sweepAngle;
            }
        }

=======
                
                using Font font = new Font("Segoe UI", fontSize, FontStyle.Bold);
                using Brush textBrush = new SolidBrush(darkModeCheck.Checked ? Color.White : Color.Black);
                
                SizeF textSize = graphics.MeasureString(labelText, font);
                GraphicsState state = graphics.Save();
                graphics.TranslateTransform(textX, textY);
                graphics.RotateTransform(midAngle > 180 ? midAngle + 180 : midAngle);
                graphics.DrawString(
                    labelText,
                    font,
                    textBrush,
                    new RectangleF(-textSize.Width / 2, -textSize.Height / 2, textSize.Width, textSize.Height),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
                graphics.Restore(state);
                
                startAngle += sweepAngle;
            }
        }
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
        private float CalculateAdaptiveFontSize(float sweepAngle, int textLength)
        {
            float baseSize = sweepAngle / 5f;
            float lengthFactor = 20f / textLength;
            return Math.Min(12, Math.Max(6, baseSize * lengthFactor));
        }

        public class CacheFile
        {
            public string FileName { get; set; } = string.Empty;
            public string FullPath { get; set; } = string.Empty;
            public double SizeMB { get; set; }
            public string Program { get; set; } = string.Empty;
            public DateTime LastModified { get; set; }
        }

        private readonly string[] cachePatterns = new[] { "cache", "tmp", "temp", "log", "dat", "crash", "bak", "thumbnail", "iconcache", "webcache", "old" };

        private string GuessProgram(string path)
        {
            try
            {
                string parentFolder = Path.GetFileName(Path.GetDirectoryName(path))?.ToLower() ?? "unknown";
                if (parentFolder.Contains("chrome")) return "Google Chrome";
                if (parentFolder.Contains("firefox")) return "Mozilla Firefox";
                if (parentFolder.Contains("edge")) return "Microsoft Edge";
                if (parentFolder.Contains("adobe")) return "Adobe Software";
                if (parentFolder.Contains("discord")) return "Discord";
                if (parentFolder.Contains("steam")) return "Steam";
                if (parentFolder.Contains("nvidia")) return "NVIDIA Drivers";
                if (parentFolder.Contains("windows")) return "Windows System";
                return parentFolder.StartsWith("unknown") ? "Unknown" : parentFolder;
            }
            catch (Exception ex)
            {
                LogMessage($"Error guessing program for path {path}: {ex.Message}");
                return "Unknown";
            }
        }

        private (List<string> Files, int TotalFiles) GetFilesNonRecursive(string path, string[] excludedSubPaths)
        {
            var directories = new Queue<string>();
            directories.Enqueue(path);
            var visited = new HashSet<string>();
            var resultFiles = new List<string>();
            int totalFiles = 0;

            while (directories.Count > 0)
            {
                var currentDir = directories.Dequeue();
                if (visited.Contains(currentDir)) continue;
                visited.Add(currentDir);

                if (excludedSubPaths.Any(ep => currentDir.ToLower().Contains(ep.ToLower())))
                    continue;

                try
                {
                    var files = Directory.EnumerateFiles(currentDir, "*.*", SearchOption.TopDirectoryOnly);
                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileName(file).ToLower();
                        if (cachePatterns.Any(pattern => fileName.Contains(pattern)))
                        {
                            resultFiles.Add(file);
                            totalFiles++;
                        }
                    }

                    var subDirs = Directory.EnumerateDirectories(currentDir, "*", SearchOption.TopDirectoryOnly);
                    foreach (var subDir in subDirs)
                    {
                        if (!visited.Contains(subDir) && !excludedSubPaths.Any(ep => subDir.ToLower().Contains(ep.ToLower())))
                        {
                            directories.Enqueue(subDir);
                        }
                    }
                }
                catch (Exception ex)
                {
                    scanErrors.Add($"Error accessing path {currentDir}: {ex.Message}");
                    LogMessage($"Error accessing path {currentDir}: {ex.Message}");
                }
            }

            return (resultFiles, totalFiles);
        }

        private void ScanWorker_DoWork()
        {
            LogMessage("Starting scan operation...");
            cacheFiles = new List<CacheFile>();
            scanErrors.Clear();

            string[] defaultPaths = {
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Path.GetTempPath()
            };

<<<<<<< HEAD
            var excludedSubPaths = new[] {
                "Application Data",
=======
            var excludedSubPaths = new[] { 
                "Application Data", 
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                "Local Settings",
                "ElevatedDiagnostics",
                "History",
                "Microsoft\\Windows\\INetCache",
                "Temporary Internet Files",
                "is-",
                "AppData\\LocalLow",
                "Microsoft\\Windows",
                "Windows\\Temp",
                "36983cbf-63a7-4a1a-85e3-dff91fc72e17",
                "4ea07c7e-2008-42c7-b738-c25b53011b7f",
                "5a0d86ec-83b9-4517-8249-051b608db064",
                "d1da97f7-a182-403b-abfd-7531df6989d3"
            };

            int processedFiles = 0;
            int totalFiles = 0;

            try
            {
                LogMessage("Calculating total files...");
                foreach (var basePath in defaultPaths.Concat(customPaths).Distinct())
                {
                    if (isCancelled)
                    {
                        LogMessage("Scan cancelled by user");
                        return;
                    }

                    try
                    {
                        if (!Directory.Exists(basePath)) continue;
                        var (files, count) = GetFilesNonRecursive(basePath, excludedSubPaths);
                        totalFiles += count;
                    }
                    catch (Exception ex)
                    {
                        scanErrors.Add($"Error counting files in {basePath}: {ex.Message}");
                        LogMessage($"Error counting files in {basePath}: {ex.Message}");
                    }
                }

                LogMessage($"Total files to scan: {totalFiles}");

                foreach (var basePath in defaultPaths.Concat(customPaths).Distinct())
                {
                    if (isCancelled)
                    {
                        LogMessage("Scan cancelled by user");
                        return;
                    }

                    try
                    {
                        if (!Directory.Exists(basePath)) continue;
                        var (files, _) = GetFilesNonRecursive(basePath, excludedSubPaths);
                        foreach (var file in files)
                        {
                            if (isCancelled)
                            {
                                LogMessage("Scan cancelled by user");
                                return;
                            }

                            ProcessFileInBatch(file, ref processedFiles, totalFiles);
                        }
                    }
                    catch (Exception ex)
                    {
                        scanErrors.Add($"Error processing base path {basePath}: {ex.Message}");
                        LogMessage($"Error processing base path {basePath}: {ex.Message}");
                    }
                }
            }
            catch (OutOfMemoryException ex)
            {
                scanErrors.Add($"Memory error: {ex.Message}");
                LogMessage($"Memory error: {ex.Message}");
<<<<<<< HEAD
=======
                LogMessage($"Memory error: {ex.Message}");
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                UpdateStatus("Error: Memory limit exceeded");
            }
            catch (Exception ex)
            {
                scanErrors.Add($"General error: {ex.Message}");
                LogMessage($"General error: {ex.Message}");
<<<<<<< HEAD
=======
                LogMessage($"General error: {ex.Message}");
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                UpdateStatus("Error processing files");
            }
            finally
            {
                LogMessage($"Scan completed. {cacheFiles.Count} files processed");
            }
        }

        private void ProcessFileInBatch(string filePath, ref int processedFiles, int totalFiles)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                var cacheFile = new CacheFile
                {
                    FileName = Path.GetFileName(filePath),
                    FullPath = filePath,
                    SizeMB = Math.Round(fileInfo.Length / (1024.0 * 1024.0), 2),
                    Program = GuessProgram(filePath),
                    LastModified = fileInfo.LastWriteTime
                };

                lock (cacheFiles)
                {
                    cacheFiles.Add(cacheFile);
                }

                processedFiles++;
                if (processedFiles % 100 == 0)
                {
                    int progress = totalFiles > 0 ? Math.Min(100, (int)((processedFiles / (double)totalFiles) * 100)) : 0;
                    UpdateProgress(progress, $"Scanning: {processedFiles} of {totalFiles} files");
<<<<<<< HEAD
=======
                    
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                    GC.Collect();
                    LogMessage($"Processed {processedFiles} files. Memory usage: {GC.GetTotalMemory(false)/1024/1024} MB");
                }
            }
            catch (Exception ex)
            {
                scanErrors.Add($"Error processing file {filePath}: {ex.Message}");
                LogMessage($"Error processing file {filePath}: {ex.Message}");
            }
        }

        private void UpdateProgress(int percentage, string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgress(percentage, status)));
                return;
            }

            progressBar.Value = percentage;
            statusLabel.Text = status;
        }

        private void UpdateStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatus(status)));
                return;
            }

            statusLabel.Text = status;
        }

        private void ScanCompleted()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ScanCompleted));
                return;
            }

            try
            {
                progressBar.Value = 100;
                statusLabel.Text = $"Scan complete - {cacheFiles.Count} files found";
                UpdateDisplay();

                if (scanErrors.Any() && developerLogsCheck.Checked)
                {
                    ShowErrorForm();
                }
<<<<<<< HEAD

=======
                
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                scanButton.Enabled = true;
            }
            catch (Exception ex)
            {
                LogMessage($"Error updating UI: {ex.Message}");
            }
        }

        private void ShowErrorForm()
        {
            Form errorForm = new Form
            {
                Text = "Scan Errors",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent,
<<<<<<< HEAD
                FormBorderStyle = FormBorderStyle.FixedSingle
=======
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MaximizeBox = false,
                MinimizeBox = false
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
            };

            TextBox errorTextBox = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                ReadOnly = true,
<<<<<<< HEAD
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.None
=======
                Font = new Font("Segoe UI", 10)
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
            };

            errorTextBox.Text = string.Join(Environment.NewLine, scanErrors);
            errorForm.Controls.Add(errorTextBox);
            ApplyThemeToForm(errorForm, darkModeCheck.Checked);
            errorForm.ShowDialog();
        }

        private void ApplyThemeToForm(Form form, bool isDarkMode)
        {
            if (isDarkMode)
            {
<<<<<<< HEAD
                form.BackColor = Color.FromArgb(32, 32, 32);
                form.ForeColor = Color.FromArgb(230, 230, 230);
=======
                form.BackColor = Color.FromArgb(30, 30, 30);
                form.ForeColor = Color.FromArgb(220, 220, 220);
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                foreach (Control control in form.Controls)
                {
                    if (control is TextBox textBox)
                    {
<<<<<<< HEAD
                        textBox.BackColor = Color.FromArgb(50, 50, 50);
                        textBox.ForeColor = Color.FromArgb(230, 230, 230);
=======
                        textBox.BackColor = Color.FromArgb(45, 45, 45);
                        textBox.ForeColor = Color.FromArgb(220, 220, 220);
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (control is CheckBox checkBox)
                    {
                        checkBox.BackColor = Color.FromArgb(30, 30, 30);
                        checkBox.ForeColor = Color.FromArgb(220, 220, 220);
                    }
                    else
                    {
                        control.BackColor = Color.FromArgb(30, 30, 30);
                        control.ForeColor = Color.FromArgb(220, 220, 220);
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                    }
                }
            }
            else
            {
<<<<<<< HEAD
                form.BackColor = Color.FromArgb(242, 243, 244);
                form.ForeColor = Color.FromArgb(31, 31, 31);
=======
                form.BackColor = SystemColors.Control;
                form.ForeColor = SystemColors.ControlText;
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                foreach (Control control in form.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.BackColor = Color.White;
<<<<<<< HEAD
                        textBox.ForeColor = Color.FromArgb(31, 31, 31);
=======
                        textBox.ForeColor = Color.Black;
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (control is CheckBox checkBox)
                    {
                        checkBox.BackColor = SystemColors.Control;
                        checkBox.ForeColor = SystemColors.ControlText;
                    }
                    else
                    {
                        control.BackColor = SystemColors.Control;
                        control.ForeColor = SystemColors.ControlText;
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                    }
                }
            }
        }

        private void ApplyTheme(bool isDarkMode)
        {
            if (isDarkMode)
            {
<<<<<<< HEAD
                this.BackColor = Color.FromArgb(32, 32, 32);
                progressBar.BackColor = Color.FromArgb(50, 50, 50);
                progressBar.ForeColor = Color.FromArgb(0, 204, 106);
                statusLabel.ForeColor = Color.FromArgb(230, 230, 230);
                totalSizeLabel.ForeColor = Color.FromArgb(230, 230, 230);
                cacheListView.BackColor = Color.FromArgb(50, 50, 50);
                cacheListView.ForeColor = Color.FromArgb(230, 230, 230);
                sizeChart.BackColor = Color.FromArgb(32, 32, 32);
                sizeChart.ChartAreas[0].BackColor = Color.FromArgb(32, 32, 32);
                scanButton.BackColor = Color.FromArgb(0, 204, 106);
                cleanButton.BackColor = Color.FromArgb(0, 204, 106);
                exportButton.BackColor = Color.FromArgb(0, 204, 106);
                settingsButton.BackColor = Color.FromArgb(0, 204, 106);
                darkModeCheck.ForeColor = Color.FromArgb(230, 230, 230);
                developerLogsCheck.ForeColor = Color.FromArgb(230, 230, 230);
                selectAllCheck.ForeColor = Color.FromArgb(230, 230, 230);
                filterProgramCombo.BackColor = Color.FromArgb(50, 50, 50);
                filterProgramCombo.ForeColor = Color.FromArgb(230, 230, 230);
                minSizeFilter.BackColor = Color.FromArgb(50, 50, 50);
                minSizeFilter.ForeColor = Color.FromArgb(230, 230, 230);
            }
            else
            {
                this.BackColor = Color.FromArgb(242, 243, 244);
                progressBar.BackColor = Color.FromArgb(225, 225, 225);
                progressBar.ForeColor = Color.FromArgb(0, 120, 215);
                statusLabel.ForeColor = Color.FromArgb(31, 31, 31);
                totalSizeLabel.ForeColor = Color.FromArgb(31, 31, 31);
                cacheListView.BackColor = Color.White;
                cacheListView.ForeColor = Color.FromArgb(31, 31, 31);
                sizeChart.BackColor = Color.FromArgb(242, 243, 244);
                sizeChart.ChartAreas[0].BackColor = Color.FromArgb(242, 243, 244);
                scanButton.BackColor = Color.FromArgb(0, 120, 215);
                cleanButton.BackColor = Color.FromArgb(0, 120, 215);
                exportButton.BackColor = Color.FromArgb(0, 120, 215);
                settingsButton.BackColor = Color.FromArgb(0, 120, 215);
                darkModeCheck.ForeColor = Color.FromArgb(31, 31, 31);
                developerLogsCheck.ForeColor = Color.FromArgb(31, 31, 31);
                selectAllCheck.ForeColor = Color.FromArgb(31, 31, 31);
                filterProgramCombo.BackColor = Color.White;
                filterProgramCombo.ForeColor = Color.FromArgb(31, 31, 31);
                minSizeFilter.BackColor = Color.White;
                minSizeFilter.ForeColor = Color.FromArgb(31, 31, 31);
            }
            foreach (Control control in this.Controls)
            {
                if (control is Label label && label != statusLabel && label != totalSizeLabel)
                {
                    label.ForeColor = isDarkMode ? Color.FromArgb(230, 230, 230) : Color.FromArgb(31, 31, 31);
=======
                this.BackColor = Color.FromArgb(30, 30, 30);
                this.ForeColor = Color.FromArgb(220, 220, 220);
                progressBar.BackColor = Color.FromArgb(45, 45, 45);
                progressBar.ForeColor = Color.FromArgb(100, 200, 100);
                statusLabel.BackColor = Color.FromArgb(30, 30, 30);
                statusLabel.ForeColor = Color.FromArgb(220, 220, 220);
                totalSizeLabel.BackColor = Color.FromArgb(30, 30, 30);
                totalSizeLabel.ForeColor = Color.FromArgb(220, 220, 220);
                cacheListView.BackColor = Color.FromArgb(45, 45, 45);
                cacheListView.ForeColor = Color.FromArgb(220, 220, 220);
                sizeChart.BackColor = Color.FromArgb(30, 30, 30);
                sizeChart.ChartAreas[0].BackColor = Color.FromArgb(30, 30, 30);
                sizeChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.FromArgb(220, 220, 220);
                sizeChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.FromArgb(220, 220, 220);
                scanButton.BackColor = Color.FromArgb(60, 60, 60);
                scanButton.ForeColor = Color.FromArgb(220, 220, 220);
                cleanButton.BackColor = Color.FromArgb(60, 60, 60);
                cleanButton.ForeColor = Color.FromArgb(220, 220, 220);
                exportButton.BackColor = Color.FromArgb(60, 60, 60);
                exportButton.ForeColor = Color.FromArgb(220, 220, 220);
                settingsButton.BackColor = Color.FromArgb(60, 60, 60);
                settingsButton.ForeColor = Color.FromArgb(220, 220, 220);
                darkModeCheck.BackColor = Color.FromArgb(30, 30, 30);
                darkModeCheck.ForeColor = Color.FromArgb(220, 220, 220);
                developerLogsCheck.BackColor = Color.FromArgb(30, 30, 30);
                developerLogsCheck.ForeColor = Color.FromArgb(220, 220, 220);
                filterProgramCombo.BackColor = Color.FromArgb(45, 45, 45);
                filterProgramCombo.ForeColor = Color.FromArgb(220, 220, 220);
                minSizeFilter.BackColor = Color.FromArgb(45, 45, 45);
                minSizeFilter.ForeColor = Color.FromArgb(220, 220, 220);
                foreach (Control control in this.Controls)
                {
                    if (control is Label label && label != statusLabel && label != totalSizeLabel)
                    {
                        label.BackColor = Color.FromArgb(30, 30, 30);
                        label.ForeColor = Color.FromArgb(220, 220, 220);
                    }
                }
            }
            else
            {
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;
                progressBar.BackColor = SystemColors.Control;
                progressBar.ForeColor = SystemColors.Highlight;
                statusLabel.BackColor = SystemColors.Control;
                statusLabel.ForeColor = SystemColors.ControlText;
                totalSizeLabel.BackColor = SystemColors.Control;
                totalSizeLabel.ForeColor = SystemColors.ControlText;
                cacheListView.BackColor = Color.White;
                cacheListView.ForeColor = Color.Black;
                sizeChart.BackColor = SystemColors.Control;
                sizeChart.ChartAreas[0].BackColor = SystemColors.Control;
                sizeChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = SystemColors.ControlText;
                sizeChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = SystemColors.ControlText;
                scanButton.BackColor = SystemColors.Control;
                scanButton.ForeColor = SystemColors.ControlText;
                cleanButton.BackColor = SystemColors.Control;
                cleanButton.ForeColor = SystemColors.ControlText;
                exportButton.BackColor = SystemColors.Control;
                exportButton.ForeColor = SystemColors.ControlText;
                settingsButton.BackColor = SystemColors.Control;
                settingsButton.ForeColor = SystemColors.ControlText;
                darkModeCheck.BackColor = SystemColors.Control;
                darkModeCheck.ForeColor = SystemColors.ControlText;
                developerLogsCheck.BackColor = SystemColors.Control;
                developerLogsCheck.ForeColor = SystemColors.ControlText;
                filterProgramCombo.BackColor = Color.White;
                filterProgramCombo.ForeColor = Color.Black;
                minSizeFilter.BackColor = Color.White;
                minSizeFilter.ForeColor = Color.Black;
                foreach (Control control in this.Controls)
                {
                    if (control is Label label && label != statusLabel && label != totalSizeLabel)
                    {
                        label.BackColor = SystemColors.Control;
                        label.ForeColor = SystemColors.ControlText;
                    }
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                }
            }
        }

        private void SelectAllCheck_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox == null) return;

            foreach (ListViewItem item in cacheListView.Items)
            {
                item.Checked = checkBox.Checked;
            }
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            cacheListView.Items.Clear();
            cacheFiles.Clear();
            scanErrors.Clear();
            progressBar.Value = 0;
            statusLabel.Text = "Status: Scanning...";
            LogMessage("Scan started");
            scanButton.Enabled = false;
            isCancelled = false;

            Task.Factory.StartNew(() =>
            {
                ScanWorker_DoWork();
                if (!isCancelled)
                {
                    ScanCompleted();
                }
<<<<<<< HEAD
            }, TaskCreationOptions.LongRunning);
=======
            }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
        }

        private void UpdateDisplay()
        {
            cacheListView.BeginUpdate();
            try
            {
                UpdateListView();
                UpdateChart();
                UpdateProgramFilter();
                double totalSize = cacheFiles.Sum(f => f.SizeMB);
                totalSizeLabel.Text = $"Total Size: {totalSize:F2} MB";
            }
            finally
            {
                cacheListView.EndUpdate();
            }
        }

        private void UpdateListView()
        {
            cacheListView.BeginUpdate();
            try
            {
                cacheListView.Items.Clear();
                double minSize = (double)minSizeFilter.Value;
                string selectedProgram = filterProgramCombo.SelectedItem?.ToString() ?? "All Programs";

                var filteredFiles = cacheFiles
<<<<<<< HEAD
                    .Where(f => (minSize == 0 || f.SizeMB >= minSize) &&
                                (selectedProgram == null || selectedProgram == "All Programs" || f.Program == selectedProgram))
=======
                    .Where(f => 
                        (minSize == 0 || f.SizeMB >= minSize) &&
                        (selectedProgram == null || selectedProgram == "All Programs" || f.Program == selectedProgram))
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                    .OrderByDescending(f => f.SizeMB);

                foreach (var cache in filteredFiles)
                {
<<<<<<< HEAD
                    var item = new ListViewItem(new[]
                    {
                        cache.FileName ?? "Unknown",
                        cache.SizeMB.ToString("F2"),
                        cache.Program ?? "Unknown",
                        cache.FullPath ?? "Unknown",
                        cache.LastModified.ToString("yyyy-MM-dd HH:mm")
=======
                    var item = new ListViewItem(new[] { 
                        cache.FileName ?? "Unknown", 
                        cache.SizeMB.ToString("F2"), 
                        cache.Program ?? "Unknown", 
                        cache.FullPath ?? "Unknown", 
                        cache.LastModified.ToString("yyyy-MM-dd HH:mm") 
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                    });
                    cacheListView.Items.Add(item);
                }
            }
            finally
            {
                cacheListView.EndUpdate();
            }
        }

        private void UpdateChart()
        {
            sizeChart.Series["CacheSize"].Points.Clear();
            var totalSize = cacheFiles.Sum(f => f.SizeMB);
            var grouped = cacheFiles
                .GroupBy(f => f.Program ?? "Unknown")
                .Select(g => new { Program = g.Key, Size = g.Sum(f => f.SizeMB) })
                .OrderByDescending(g => g.Size)
                .Take(10);

            foreach (var group in grouped)
            {
                DataPoint point = new DataPoint();
                point.SetValueY(group.Size);
                point.LegendText = group.Program;
                point.Label = "";
                sizeChart.Series["CacheSize"].Points.Add(point);
            }
        }

        private void UpdateProgramFilter()
        {
            filterProgramCombo.Items.Clear();
            filterProgramCombo.Items.Add("All Programs");

            var programSizes = cacheFiles
                .GroupBy(f => f.Program ?? "Unknown")
                .Select(g => new { Program = g.Key, TotalSizeMB = g.Sum(f => f.SizeMB) })
                .OrderByDescending(g => g.TotalSizeMB);

            filterProgramCombo.Items.AddRange(programSizes.Select(g => g.Program).ToArray());
            filterProgramCombo.SelectedIndex = 0;
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            var selectedFiles = cacheListView.CheckedItems.Cast<ListViewItem>().Select(item => item.SubItems[3].Text).ToList();
            if (selectedFiles.Count == 0)
            {
                MessageBox.Show("Please select files to clean!");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete {selectedFiles.Count} files?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int successCount = 0;
                int failCount = 0;
                foreach (var file in selectedFiles)
                {
<<<<<<< HEAD
                    try
=======
                    try 
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                    {
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                            successCount++;
                            LogMessage($"File deleted: {file}");
                        }
                        else
                        {
                            failCount++;
                            LogMessage($"File not found: {file}");
                        }
<<<<<<< HEAD
                    }
=======
                    } 
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
                    catch (UnauthorizedAccessException)
                    {
                        failCount++;
                        LogMessage($"Access denied for file: {file}");
                    }
                    catch (Exception ex)
                    {
                        failCount++;
                        LogMessage($"Error deleting file {file}: {ex.Message}");
                    }
                }
                ScanButton_Click(this, EventArgs.Empty);
                MessageBox.Show($"Cleaning complete! {successCount} files deleted, {failCount} files failed.");
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV Files (*.csv)|*.csv", FileName = $"CacheReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        sw.WriteLine("File Name,Size (MB),Program,Path,Last Modified");
                        foreach (var cache in cacheFiles)
                            sw.WriteLine($"\"{cache.FileName}\",{cache.SizeMB:F2},\"{cache.Program}\",\"{cache.FullPath}\",{cache.LastModified:yyyy-MM-dd HH:mm}");
                    }
                    MessageBox.Show("Report saved!");
                    LogMessage($"Report saved: {sfd.FileName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving report: {ex.Message}");
                    LogMessage($"Error saving report: {ex.Message}");
                }
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    customPaths = customPaths.Concat(new[] { dialog.SelectedPath }).Distinct().ToArray();
                    MessageBox.Show($"Path {dialog.SelectedPath} added!");
                    LogMessage($"Custom path added: {dialog.SelectedPath}");
                }
            }
        }

        private void DarkModeCheck_CheckedChanged(object sender, EventArgs e)
        {
            ApplyTheme(darkModeCheck.Checked);
        }

        private void FilterProgramCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void MinSizeFilter_ValueChanged(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
<<<<<<< HEAD
            this.ClientSize = new Size(1200, 500);
=======
            this.ClientSize = new System.Drawing.Size(1200, 500);
>>>>>>> 6e36e7699602eda7d07db44dfa42a9b51711edac
            this.Name = "Form1";
            this.ResumeLayout(false);
        }
    }
}