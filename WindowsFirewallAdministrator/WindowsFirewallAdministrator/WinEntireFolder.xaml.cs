using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FirewallEngine;
using System.IO;
using System.Linq;
using W = System.Windows.Forms;
using WindowsFirewallAdministrator.Tools;
using D = System.Drawing;

namespace WindowsFirewallAdministrator
{
	/// <summary>
	/// Interaction logic for WinEntireFolder.xaml
	/// </summary>
	public partial class WinEntireFolder : Window
	{
        class Executable
        {
            public ImageSource Icon { get; set; }
            public string Name { get; set; }
            public string RelativePath { get; set; }
            public string Status { get; set; }
        }
        W.FolderBrowserDialog folderDialog;
		public WinEntireFolder()
		{
			this.InitializeComponent();
            folderDialog = new W.FolderBrowserDialog();
		}

        private void btnChangeFolder_Click(object sender, RoutedEventArgs e)
        {
            if (folderDialog.ShowDialog() == W.DialogResult.OK)
            {
                this.lblFolderPath.Content = folderDialog.SelectedPath;
                LoadFolder(folderDialog.SelectedPath);
            }
        }

        private void LoadFolder(string fullFolderPath)
        {
            List<Executable> executableList = new List<Executable>();
            foreach (var file in Directory.EnumerateFiles(fullFolderPath, "*.exe", SearchOption.AllDirectories))
            {
                FileInfo fileInfo = new FileInfo(file);
                var icon = D.Icon.ExtractAssociatedIcon(fileInfo.FullName);
                executableList.Add(new Executable()
                {
                    Icon = icon.ToImageSource(),
                    Name = fileInfo.Name,
                    RelativePath = fileInfo.Directory.FullName.Substring(fullFolderPath.Length),
                    Status = "xD"
                });
            }
            
            this.gridExecutables.ItemsSource = executableList;
        }
	}
}