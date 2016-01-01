using FirewallEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsFirewallAdministrator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnLoadRules_Click(object sender, RoutedEventArgs e)
        {
            CommandLineFirewall clFirewall = new CommandLineFirewall();
            var rules = clFirewall.GetRules();
            gridFirewall.ItemsSource = rules;
        }

        private void btnBlockExecutable_Click(object sender, RoutedEventArgs e)
        {
            CommandLineFirewall clFirewall = new CommandLineFirewall();
            var rules = clFirewall.AddProgramRule(@"C:\Users\NachoPC\Downloads\VisualBoyAdvance-1.8.0-beta3\VisualBoyAdvance.exe", FirewallRule.EDirection.In, FirewallRule.EAction.Block, FirewallRule.EProtocol.TCP);

        }
    }
}
