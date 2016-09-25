using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NugetPublisher.Desktop.UI
{
    public partial class Form1 : Form
    {
        private string[] _fileNames;
        private readonly string _nugetPath;
        private readonly string _nugetPackagesPath;
        private readonly Regex _assemblyNameRegex;
        private readonly Regex _nuspecSanityRegex;
        private readonly Regex _assemblyVersionRegex;

        public Form1()
        {
            InitializeComponent();
            _nugetPath = string.Format(@"{0}Nuget\nuget.exe", Path.GetPathRoot(Environment.SystemDirectory));
            _nugetPackagesPath = string.Format(@"{0}Nuget\Packages", Path.GetPathRoot(Environment.SystemDirectory));
            StatusLabel.Text = @"Idle";

            _assemblyNameRegex = new Regex(@"^(?:[a-z]:(?:\\\w+?\.?\w+)+)?\\(?:((?:\w+\.?)+)\.dll)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _nuspecSanityRegex = new Regex(@"\s*(?:<\w+>https?:\/\/\w+\<\/\w+>|<tags>.*<\/tags>|.*<\/?dependenc.*>)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

            _assemblyVersionRegex = new Regex(@"(?:<version>([\d\.]+)<\/version>)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            PushToServerButton.Enabled = Regex.IsMatch(ServerUrlTextBox.Text, @"https?://\w+\:?\d{0,5}\/?.*");
        }

        private void SelectArtifactsButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
             {
                 Multiselect = true,
                 DefaultExt = ".dll",
                 AddExtension = true,
                 Filter = @".NET Assemblies (*.dll) | *.dll"
             };

            var result = dialog.ShowDialog();

            if (!Directory.Exists(_nugetPackagesPath + @"\lib"))
            {
                Directory.CreateDirectory(_nugetPackagesPath + @"\lib");
            }

            _fileNames = new string[dialog.FileNames.Length];

            if (result == DialogResult.OK)
            {
                ArtifactsListView.Items.Clear();

                for (int idx = 0; idx < dialog.SafeFileNames.Length; ++idx)
                {
                    var fileName = dialog.SafeFileNames[idx];

                    _fileNames[idx] = string.Format(@"{0}\lib\{1}", _nugetPackagesPath, fileName);

                    if (dialog.FileNames[idx] != _fileNames[idx]) // avoids circular copying
                    {
                        File.Copy(dialog.FileNames[idx], _fileNames[idx], true);
                    }

                    ArtifactsListView.Items.Add(fileName);
                }

                //CreateNuspecButton.Enabled = true;
                CreatePackageButton.Enabled = true;
            }
        }

        private async void CreatePackageButton_Click(object sender, EventArgs e)
        {
            var controlManager = new FormControlManager(this);

            await controlManager.ToggleControlsOfType<Button>(async () =>
            {
                StatusLabel.Text = @"Generating packages...";

                if (!await RunNuget(@"spec -a ""lib\{0}.dll"" -force{1}", _fileNames))
                {
                    StatusLabel.Text = @"Error generating NuSpec file(s)";
                    return;
                }

                var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

                Parallel.ForEach(_fileNames, options, async fileName =>
                {
                    var match = _assemblyNameRegex.Match(fileName);

                    if (!match.Success) return;

                    var nuspecFile = string.Format(@"{0}\{1}.nuspec", _nugetPackagesPath, match.Groups[1].Value);

                    if (!File.Exists(nuspecFile)) return;

                    var fileText = File.ReadAllText(nuspecFile);

                    if (!string.IsNullOrEmpty(fileText))
                    {
                        using (var writer = File.CreateText(nuspecFile))
                        {
                            var sanitizedNuspecMarkup = _nuspecSanityRegex.Replace(fileText, string.Empty);
                            await writer.WriteAsync(sanitizedNuspecMarkup);
                        }
                    }

                });


                if (await RunNuget(@"pack ""{0}.nuspec""{1}", _fileNames))
                {
                    StatusLabel.Text = @"Package(s) generated successfully.";
                    return;
                }

                StatusLabel.Text = @"Unable to generate all packages. Check folder write permissions.";
            },
            new HashSet<Button> { CreateNuspecButton });
        }

        private async void CreateNuspecButton_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = @"Generating spec files...";

            if (await RunNuget(@"spec -a ""lib\{0}.dll"" -force{1}", _fileNames))
            {
                StatusLabel.Text = @"NuSpec file(s) generated successfully.";
                return;
            }

            StatusLabel.Text = @"Unable to generate all nuspec file(s). Check folder write permissions.";
        }

        private async void PushToServerButton_Click(object sender, EventArgs e)
        {
            var controlManager = new FormControlManager(this);

            await controlManager.ToggleControlsOfType<Button>(async () =>
            {
                StatusLabel.Text = @"Publishing packages to " + ServerUrlTextBox.Text + @"...";

                if (ArtifactsListView.Items.Count == 0)
                {
                    StatusLabel.Text = @"Please select your artifacts first";
                    return;
                }

                if (string.IsNullOrEmpty(ApiKeyTextBox.Text))
                {
                    StatusLabel.Text = @"Please specify an API key";
                    return;
                }

                if (await RunNuget(@"push ""{0}.nupkg"" -Source " + ServerUrlTextBox.Text + " -ApiKey " + ApiKeyTextBox.Text + " -DisableBuffering" + "{1}", _fileNames, true))
                {
                    StatusLabel.Text = @"Packages successfully published.";
                    return;
                }

                StatusLabel.Text = @"Unable to publish all packages. Check your Internet Connection, Nuget URL and ApiKey.";
            },
            new HashSet<Button> { CreateNuspecButton });
        }

        private async Task<bool> RunNuget(string argumentFormat, string[] fileNames, bool detectAssemblyVersion = false)
        {
            Task downloadTask = DownloadNugetIfNotExist();

            return await downloadTask.ContinueWith((antecedent) =>
            {
                argumentFormat = string.Format("{0} {1}", _nugetPath, argumentFormat);

                var commands = new StringBuilder();

                var length = fileNames.Length;

                for (var idx = 0; idx < length; ++idx)
                {
                    if (File.ReadAllBytes(fileNames[idx]).Length == 0) continue;

                    var match = _assemblyNameRegex.Match(fileNames[idx]);

                    if (!match.Success) continue;

                    var packageName = match.Groups[1].Value;
                    var nuspecPath = string.Format(@"{0}\{1}.nuspec", _nugetPackagesPath, packageName);

                    if (detectAssemblyVersion && File.Exists(nuspecPath)) // when pushing to my nuget server
                    {
                        match = _assemblyVersionRegex.Match(File.ReadAllText(nuspecPath));
                        if (!match.Success) continue;

                        var version = match.Groups[1].Value;

                        if (version.EndsWith(".0.0.0"))
                        {
                            version = version.Substring(0, version.Length - 2);
                        }

                        packageName = string.Format(@"{0}.{1}", packageName, version);
                    }

                    commands.Append(string.Format(argumentFormat, packageName, length > 1 && idx < length - 1 ? " & " : string.Empty));
                }

                var successCount = 0;

                using (var process = new Process())
                {

                    var startInfo = new ProcessStartInfo
                    {
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        Arguments =
                            "/c cd " + _nugetPackagesPath + " & " + commands +
                            (detectAssemblyVersion ? " & del /f /a:- *.nuspec && del /f /a:- *.nupkg" : string.Empty),
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                    };

                    process.StartInfo = startInfo;

                    process.Start();

                    while (!process.StandardOutput.EndOfStream)
                    {
                        string result = (process.StandardOutput.ReadLine() ?? string.Empty).ToLower();

                        if (result.Contains("success") || result.Contains("package was pushed"))
                        {
                            ++successCount;
                        }
                    }
                }

                return successCount == _fileNames.Length;
            });
        }

        private async Task DownloadNugetIfNotExist()
        {
            if (File.Exists(_nugetPath)) return;

            StatusLabel.Text = @"Downloading nuget...";

            //var address = @"https://dist.nuget.org/win-x86-commandline/v3.4.4/NuGet.exe";
            var address = @"https://dist.nuget.org/win-x86-commandline/v3.5.0-rc1/NuGet.exe";

            try
            {
                using (var client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(address, _nugetPath);
                }
            }
            catch (Exception exception)
            {
                StatusLabel.Text = @"Fatal: " + exception.Message;
            }
        }
    }
}
