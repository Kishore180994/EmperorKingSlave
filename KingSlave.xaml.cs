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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public struct set{
        public Image i;
        public bool s;
     }


    public partial class KingSlave : Window
    {
        int height = 480;
        int width = 590;
        int userSelectedIndex = 0;
        int aiSelectedIndex = 0;
        Image[] set1,temp;
        set[] set2;
        Image[] set3,set4;
        Image[] wholeset;
        string[] imgNames;
        string selectedCard;
        string userCard= "selectCard";
        bool ready = false;
        bool clicked = false;
        Image selectedImage;
        int temp1,temp2,ai;
        private int aiScore;
        private int userScore;
        int card = 0;
        List<int> numbers = new List<int>();
        public KingSlave(int x)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            card = x;
            LoadImages();
        }
        
        private string userSelectedCard()
        {
            if (selectedImage != null) return selectedImage.Name;
            else return "selectCard";
        }
        
        private string AISelectRandomCard()
        {
            
            Random rnd = new Random();
            //set1 = set1.OrderBy(x => rnd.Next()).ToArray();
            //set3 = set3.OrderBy(x => rnd.Next()).ToArray();

            //Remove the number from this

            int y=0;
            do
            {
                y = rnd.Next(0, 5);
                Debug.WriteLine("Random Generated: "+y);
            } while (numbers.Contains(y));
            numbers.Add(y);
            
            Image selected = set1[y];
            //Debug.WriteLine("Selected Card: "+y+" "+selectedCard.Name);
            return selected.Name;
        }
        
        private void LoadImages()
        {
            Debug.WriteLine("card: " + card);
            wholeset = new Image[] { King, c1, c2, c3, c4, Slave, c5, c6, c7, c8, lcs, lcs1, lcs2, lcs3, lcs4,
                lcs5, lcs6, lcs7, lcs8, lcs9 };
            imgNames =new string[] { "emperor","c1","c2","c3","c4", "slave", "c5",
                "c6","c7","c8","lcs0", "lcs1", "lcs2", "lcs3", "lcs4", "lcs5",
                "lcs6", "lcs7", "lcs8", "lcs9" };

            for (int i = 0; i < wholeset.Length; i++) processImages(wholeset[i],imgNames[i]);
            if (card == 0)
            {
                set1 = new Image[] { King, c1, c2, c3, c4 };
                temp = new Image[] { Slave, c5, c6, c7, c8 };
            }
            else
            {
                set1 = new Image[] { Slave, c1, c2, c3, c4 };
                temp = new Image[] { King, c5, c6, c7, c8 };
            }
            set2 = new set[5];
            for(int i = 0; i < 5; i++)
            {
                set2[i].i = temp[i];
                set2[i].s = false;
            }


            set3 = new Image[] { lcs, lcs1, lcs2, lcs3, lcs4 };
            set4 = new Image[] { lcs5, lcs6, lcs7, lcs8, lcs9 };
        }

       
        private async void AssignCardsAsync()
        {
            await Task.Delay(700);
            for (int i = 0; i < set1.Length; i++)
             {
                  set1[i].SetValue(Grid.RowProperty, 0);
                  set1[i].SetValue(Grid.ColumnProperty, i + 1);
             }
             for (int i = 0; i < set2.Length; i++)
             {
                 set2[i].i.SetValue(Grid.RowProperty, 2);
                 set2[i].i.SetValue(Grid.ColumnProperty, i + 1);
              }
        }

        private void selectUserCard(String name, int v1, int v2)
        {
            Image temp = new Image();
            if (selectedImage != null)
            {
                temp = selectedImage;
                Debug.WriteLine(temp1 + " " + temp2);
                Grid.SetRow(temp, temp1);
                Grid.SetColumn(temp, temp2);
                //temp.Height += 30;
                //temp.Width += 30;
            }
            Image s = new Image();
            for (int i = 0; i < set2.Length; i++)
            {
                if (set2[i].i.Name == name)
                {
                    s = set2[i].i;
                    userSelectedIndex = i;
                    set2[i].s = true;
                    temp1 = Grid.GetRow(s);
                    temp2 = Grid.GetColumn(s);
                    Grid.SetRow(s, 1);
                    Grid.SetColumn(s, 4);
                    //s.Height -= 30;
                    //s.Width -= 30;
                }
            }


            //rowIndex = v2;
            //columnIndex = v1;
            selectedImage = s;
            //Grid.SetRow(selectedImage,1);
            //Grid.SetColumn(selectedImage,4);
        }

        //Mouse Events
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            clicked = !clicked;
            var mouseWasDown = e.Source as FrameworkElement;
            //Debug.WriteLine("Mouse Entered: "+ mouseWasDown.Name+" "+Grid.GetColumn(mouseWasDown));
            selectUserCard(mouseWasDown.Name, Grid.GetColumn(mouseWasDown), Grid.GetRow(mouseWasDown));
        }

        //Buttons
        private void set_Click(object sender, RoutedEventArgs e)
        {
            userCard = userSelectedCard();
            selectedCard = AISelectRandomCard();
            //Debug.WriteLine("After Random: ");
            //for (int i = 0; i < 5; i++)
            //{
            //    Debug.WriteLine(set1[i].Name + " && " + set3[i].Name);
            //}
            if (userCard == "selectCard")
                MessageBox.Show("Please select Card!!!");
            else
            {
                //MessageBox.Show(selectedCard);
                for (int i = 0; i < set1.Length; i++)
                {
                    //Debug.WriteLine("Card: "+set1[i].Name+" "+"Selected Card: "+selectedCard);
                    if (set1[i].Name == selectedCard)
                    {
                        ai = i;
                        aiSelectedIndex = i;
                        //Debug.Write("SelectedIndex: "+i+" SelectedCard: "+set1[i].Name+"\n");
                        Grid.SetRow(set1[i], 1);
                        Grid.SetColumn(set1[i], 2);
                        MoveTo(set3[i], 0, 0);
                        Grid.SetRow(set3[i], 1);
                        Grid.SetColumn(set3[i], 2);
                        //set1[i].Height -= 30;
                        //set1[i].Width -= 30;
                    }
                }
                ready = true;
            }

           
        }

        private void GO_Click(object sender, RoutedEventArgs e)
        {
            if(ready == false)
            {
                MessageBox.Show("Please SET your card!!");
            }
            else
            {
                //Final Evaluation
                flipImage(set3[ai]);
                Grid.SetColumn(set3[ai],0);
                Grid.SetRow(set3[ai], 0);
                if (selectedCard == c1.Name || selectedCard == c2.Name || selectedCard == c3.Name || selectedCard == c4.Name) selectedCard = "c";
                if (userCard == c5.Name || userCard == c6.Name || userCard == c7.Name || userCard  == c8.Name) userCard  = "c";

                Debug.WriteLine(selectedCard+" "+userCard+" = "+(selectedCard==userCard));
                //selectedCard && userCard
                if(selectedCard == userCard)
                {
                    //Remove the cards from stac
                    MessageBox.Show("Draw!!");
                    Debug.WriteLine(set2[userSelectedIndex].i.Name+" "+ set1[aiSelectedIndex].Name);
                    Grid.SetRow(set2[userSelectedIndex].i,0);
                    Grid.SetColumn(set2[userSelectedIndex].i, 0);
                    Grid.SetRow(set1[aiSelectedIndex], 0);
                    Grid.SetColumn(set1[aiSelectedIndex], 0);
                    userCard = "selectCard";
                    selectedImage = null;
                }
                else
                {
                    if (userCard == "King" && selectedCard == "Slave")
                    {
                        aiScore++;
                        MessageBox.Show("You Lost!! AI Wins!! AI score is " + aiScore);
                        Reset();
                        MessageBox.Show("Press Start for a New Game |->|");
                    }
                    else if (userCard == "Slave" && selectedCard == "King")
                    {
                        userScore++;
                        MessageBox.Show("Congratulations!! You Won!! <3 Your score is "+ userScore);
                        Reset();
                        MessageBox.Show("Press Start for a New Game |->|");
                    }
                    else if (userCard == "King")
                    {
                        userScore++;
                        MessageBox.Show("Congratulations!! You Won!! <3 Your score is " + userScore);
                        Reset();
                        MessageBox.Show("Press Start for a New Game |->|");
                    }
                    else if (userCard == "Slave")
                    {
                        aiScore++;
                        MessageBox.Show("You Lost!! AI Wins!! AI score is "+aiScore);
                        Reset();
                        MessageBox.Show("Press Start for a New Game |->|");
                    }
                    else if (selectedCard == "King")
                    {
                        aiScore++;
                        MessageBox.Show("You Lost!! AI Wins!! AI score is " + aiScore);
                        Reset();
                        MessageBox.Show("Press Start for a New Game |->|");
                    }
                    else if (selectedCard == "Slave")
                    {
                        userScore++;
                        MessageBox.Show("Congratulations!! You Won!! <3 Your score is " + userScore);
                        Reset();
                        MessageBox.Show("Press Start for a New Game |->|");
                    }
                    else
                    {
                        MessageBox.Show("Please select Card |=|");
                    }
                    
                    //Reset the game
                }
                playerScore.Content = userScore;
                aipoints.Content = aiScore;
                Debug.WriteLine("User: "+ userScore+" && "+"AI: "+aiScore);
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            
            Reset();
            Random rnd = new Random();
            //Debug.WriteLine("Before Random: ");
            //for(int i = 0; i < 5; i++)
            //{
            //    Debug.WriteLine(set1[i].Name+" && "+set3[i].Name);
            //}
            set1 = set1.OrderBy(x => rnd.Next()).ToArray();
            set3 = set3.OrderBy(x => rnd.Next()).ToArray();
            
            Randomizer.Randomize(set2);
            Randomizer.Randomize(set4);
            for (int i = 0; i < set1.Length; i++)
            {
                MoveTo(set3[i], 0, (width / 6) * (i + 1));
                MoveTo(set4[i], (height / 2), (width / 6) * (i + 1));
            }
            
            AssignCardsAsync();
            MessageBox.Show("Select FLIP to initiate the Game!");
        }


        //User-Defined Functions
        private void processImages(Image im, string v)
        {
            //im.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/" + v + ".png", UriKind.Absolute));
            im.Source = new BitmapImage(new Uri("pack://application:,,,/Images/"+v+".png"));
        }

        private void MoveTo(Image target, double newX, double newY)
        {
            var top = 0;
            var left = 0;
            //MessageBox.Show();
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(top, newY - top, TimeSpan.FromSeconds(0.5));
            DoubleAnimation anim2 = new DoubleAnimation(left, newX - left, TimeSpan.FromSeconds(0.5));
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            trans.BeginAnimation(TranslateTransform.YProperty, anim2);
        }

        private void flip_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < set4.Length; i++)
            {
                flipImage(set4[i]);
            }
        }

        private void flipImage(Image img)
        {
            img.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform flipTrans = new ScaleTransform();
            flipTrans.ScaleX = -1;
            //flipTrans.ScaleY = -1;
            img.RenderTransform = flipTrans;
        }




        private void Reset()
        {
            ready = false;
            clicked = false;
            selectedImage = null;
            numbers.Clear();
            for (int i = 0; i < set1.Length; i++)
            {
                set1[i].SetValue(Grid.RowProperty, 0);
                set1[i].SetValue(Grid.ColumnProperty, 0);
                set3[i].SetValue(Grid.RowProperty, 0);
                set3[i].SetValue(Grid.ColumnProperty, 0);
            }
            for (int i = 0; i < set2.Length; i++)
            {
                set2[i].i.SetValue(Grid.RowProperty, 0);
                set2[i].i.SetValue(Grid.ColumnProperty, 0);
                set2[i].s = false;
                set4[i].SetValue(Grid.RowProperty, 0);
                set4[i].SetValue(Grid.ColumnProperty, 0);
            }
        }

    }


    public class Randomizer
    {
        public static void Randomize<T>(T[] items)
        {
            Random rand = new Random();
            
            for (int i = 0; i < items.Length - 1; i++)
            {
                int j = rand.Next(i, items.Length);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }
    }
}
