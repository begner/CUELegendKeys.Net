using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace CUELegendKeys.UI
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class HotspotSettingInput : UserControl
    {
        
        private ConfigurationData m_data;
        public HotspotSettingInput(string label, int value)
        {
            InitializeComponent();
            Label.Text = label;
            m_data = new ConfigurationData();
            m_data.IntValue = value;
            this.DataContext = m_data; 
        }

        public int getValue()
        {
            return m_data.IntValue;
        }
        
    }
    

    public class ConfigurationData : INotifyPropertyChanged
    {
        private int _IntValue;

        public int IntValue
        {
            get { return _IntValue; }
            set { _IntValue = value; OnPropertyChanged("IntValue"); }
        }
        //below is the boilerplate code supporting PropertyChanged events:
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}
