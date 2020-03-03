using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace CUELegendKeys
{
    /// <summary>
    /// Interaktionslogik für HotspotSettings.xaml
    /// </summary>
    public partial class HotspotSettingsWindow : MetroWindow
    {
        public SettingHotspot SettingHotspot { get; set; } = null;

        private UI.HotspotSettingInput Setting_BorderCut;
        private UI.HotspotSettingInput Setting_CaputreRectX;
        private UI.HotspotSettingInput Setting_CaputreRectY;
        private UI.HotspotSettingInput Setting_CaputreRectW;
        private UI.HotspotSettingInput Setting_CaputreRectH;

        public HotspotSettingsWindow(SettingHotspot settingHotspot)
        {
            this.SettingHotspot = settingHotspot;
            InitializeComponent();
            Headline.Text = this.SettingHotspot.Name;

            Setting_BorderCut = new UI.HotspotSettingInput("BorderCut", SettingHotspot.BorderCut);
            Setting_CaputreRectX = new UI.HotspotSettingInput("X", (int)SettingHotspot.CaptureRect.X);
            Setting_CaputreRectY = new UI.HotspotSettingInput("Y", (int)SettingHotspot.CaptureRect.Y);
            Setting_CaputreRectW = new UI.HotspotSettingInput("Width", (int)SettingHotspot.CaptureRect.Width);
            Setting_CaputreRectH = new UI.HotspotSettingInput("Height", (int)SettingHotspot.CaptureRect.Height);

            SettingsPane.Children.Add(Setting_BorderCut);
            SettingsPane.Children.Add(Setting_CaputreRectX);
            SettingsPane.Children.Add(Setting_CaputreRectY);
            SettingsPane.Children.Add(Setting_CaputreRectW);
            SettingsPane.Children.Add(Setting_CaputreRectH);
        }

        void SaveClick(Object sender, RoutedEventArgs e)
        {
            SettingHotspot.BorderCut = Setting_BorderCut.getValue();
            SettingHotspot.CaptureRect = new Rect(
                Setting_CaputreRectX.getValue(),
                Setting_CaputreRectY.getValue(),
                Setting_CaputreRectW.getValue(),
                Setting_CaputreRectH.getValue());
            
            this.Close();
        }

        void CancelClick(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
        
}
