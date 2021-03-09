using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cotr.Service;

namespace Cotr.WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // update date
            var sm = new ServiceManager();
            sm.CotService.UpdateCotData();
            // set form elements
            string[] markets = sm.SymbolService.GetAllMarketSymbols().ToArray();
            this.marketComboBox.Items.AddRange(markets);
        }

        private void marketComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                MessageBox.Show(marketComboBox.SelectedItem.ToString());
            }
        }
    }
}