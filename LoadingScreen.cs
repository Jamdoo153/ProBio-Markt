using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pro_Bio_Markt
{
    public partial class LoadingScreen : Form
    {
        private int loadingBarValue;
        public LoadingScreen()
        {
            InitializeComponent();
        }
        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            LoadingBarTimer.Start();
        }
        private void LoadingBarTimer_Tick(object sender, EventArgs e)
        {
            loadingBarValue += 1;

            lblLoadingProgress.Text = loadingBarValue.ToString() +"%";
            LoadingProgressBar.Value = loadingBarValue;

            if (loadingBarValue >= LoadingProgressBar.Maximum)
            { 
                LoadingBarTimer.Stop();
            }

        }

        
    }
}
