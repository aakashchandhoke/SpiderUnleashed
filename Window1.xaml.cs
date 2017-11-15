/*
 * Created by SharpDevelop.
 * User: dell
 * Date: 04-Oct-17
 * Time: 11:45 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Net;
//using System.Net.Http;
using System.Text.RegularExpressions;

namespace SpiderUnleashed
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
            lbNames.Content = "Aakash Chandhoke\nAkhilesh Kumar\nShubham Kumar";

        }
		void btnGenerate_Click(object sender, RoutedEventArgs e)
		{
			try {
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "*.txt|*.txt";
				sfd.ShowDialog();
				string filePath =  sfd.FileName;
				//System.Windows.MessageBox.Show(filePath);
				string typeOfWebsite;
				if (cbWebsites.SelectedIndex == 0) {
					typeOfWebsite = "edu";
				}
				else {
					System.Windows.MessageBox.Show("wrong input");
					return;
				}
				string searchString = tbSearchString.Text;
				int numberOfResults = int.Parse(tbNoOfResults.Text);
				doGenerate(filePath, typeOfWebsite, searchString, numberOfResults);
                System.Windows.MessageBox.Show("Done");



            }
			catch (Exception) {
				System.Windows.MessageBox.Show("Error in input");
			}
			
			
			
			
		}
		
		public bool doGenerate(string filePath, string typeOfWebsite, string searchString, int numberOfResults) {
			
			
			
			
			string proxyFile = HelperFunctions.GetDesktopPath() + "\\proxy.txt";

            LinksCrawler lc = new LinksCrawler();
            
            lc.Proxy = null;
            
            if ((bool)chkProxy.IsChecked == true) {
            	lc.Proxy = HelperFunctions.CreateWebProxy(tbProxyAddress.Text,int.Parse(tbProxyPort.Text), tbProxyUser.Text, tbProxyPass.Text);
			}
            
            lc.SearchSite = typeOfWebsite;
            lc.NumberOfLinks = numberOfResults;
            lc.SearchString = searchString;

            string[] results = lc.GetResults();

            
            string outputFilePath = filePath;
            if (File.Exists(outputFilePath)) {
                File.Delete(outputFilePath);
            }
            File.WriteAllLines(outputFilePath, results);
            return true;
		}

			
	}
}