using RiotNet;
using RiotNet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Lol_Account_Changer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
        private int intUserSet = 1;
        private List<User> liUser;
        private string strLolPfad;
        public MainWindow()
        {
            InitializeComponent();

            Data objData = new Data();
            liUser = new List<User>();
            liUser = objData.readFile();
            this.createWindowElements(liUser);
            this.strLolPfad = objData.getLolPath();
        }

        public void createWindowElements(List<User> liUser)
        {
            foreach(User objUser in liUser)
            {
                addLabelName(objUser);
                addLabelElo(objUser);
                addStartButton(objUser);
                this.intUserSet++;
            }

           
        }

        public void addStartButton(User objUser)
        {
            Button btn = new Button();
            btn.Click += button_Click;

            btn.Name = "btn" + objUser.getUserID();
            btn.Content = "Start";
            btn.Width = 60;
            btn.Height = 20;
            btn.Margin = new Thickness(400, (intUserSet * 40)-635, 0, 0);
            btn.Foreground = new SolidColorBrush(Colors.Black);
            btn.Background = new SolidColorBrush(Colors.White);

            Grid.SetRow(btn, 0);
            Grid.SetColumn(btn, 0);
            grid.Children.Add(btn);
        }

        protected void button_Click(object sender, EventArgs e)
        {
            string strButtonName = ((Button)sender).Name;
            strButtonName = strButtonName.Replace("btn", "");
            int intUserID = int.Parse(strButtonName);

            User objUser = liUser[intUserID];

            Login objLogin = new Login(this.strLolPfad, objUser);
            objLogin.start();

        }


        public void addLabelElo(User objUser)
        {
            Label lb1 = new Label();
            lb1.Name = "lbElo" + objUser.getUserID();
            lb1.Content = "Lvl: "+objUser.getLevel()+" - "+objUser.getLiga() + " " + objUser.getDivision();
            lb1.Margin = new Thickness(100, intUserSet * 20, 0, 0);
            lb1.Foreground = new SolidColorBrush(Colors.Black);
            lb1.Background = new SolidColorBrush(Colors.White);
            lb1.Width = 140;

            Grid.SetRow(lb1, 0);
            Grid.SetColumn(lb1, 0);
            grid.Children.Add(lb1);
        }

        public void addLabelName(User objUser)
        {
            Label lb1 = new Label();
            lb1.Name = "lbName"+ objUser.getUserID();
            lb1.Content = objUser.getUsername()+" ["+objUser.getSummonerName()+"]";
            lb1.Margin = new Thickness(-180, intUserSet * 20, 0, 0);
            lb1.Foreground = new SolidColorBrush(Colors.Black);
            lb1.Background = new SolidColorBrush(Colors.White);
            lb1.Width = 350;

            Grid.SetRow(lb1, 0);
            Grid.SetColumn(lb1, 0);
            grid.Children.Add(lb1);
        }
   
  }
}