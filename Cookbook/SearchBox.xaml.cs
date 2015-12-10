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

namespace Cookbook
{
    /// <summary>
    /// Interaction logic for SearchBox.xaml
    /// </summary>
    public partial class SearchBox : Window
    {
        public SearchBox()
        {
            InitializeComponent();
            searchTextBox.Focus();
        }

        public bool MatchAllTerms 
        { 
            get
            { 
                return (bool)matchAllRadioButton.IsChecked; 
            }
        }

        public bool CaseSensitive 
        {
            get
            {
                return (bool)caseSensitiveCheckBox.IsChecked;
            } 
        }

        public bool SearchAllRecipes 
        { 
            get
            {
                return (bool)searchAllRadioButton.IsChecked;
            }
        }

        public String SearchString
        {
            get { return searchTextBox.Text; }
            set { searchTextBox.Text = value; }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        
     
    }
}
