using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WFBS.Business.Core;

namespace MasterPages.Page
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>

    public partial class Page2 : System.Windows.Controls.Page
    {
        public Page2()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
        }

        /*
        private void txtID_Loaded(object sender, RoutedEventArgs e)
        {

            txtArea.Text = string.Format("{0} | {1}", Global.AreaInvestigacion, Global.NombreUsuario);
        }
        */
    }
}