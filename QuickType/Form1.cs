using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuickType
{
    public partial class Form1 : Form
    {
        // change INITIAL_DELAY to a higher or lower value to change game speed
        const int INITIAL_DELAY = 1500;
        Random _random = new Random();
        Stats _stats = new Stats();
        Words _words = new Words();

        //used to hold the list of characters typed
        private string _stringBuffer = String.Empty;

        public Form1()
        {
            InitializeComponent();
            //HFCSharp Book sets these properties in the form designer            
            this.KeyPreview = true;
            this.Text += " Type the word displayed and press 'enter'"; 
            timer1.Interval = INITIAL_DELAY;
            difficultyProgressBar.Minimum = 0;
            difficultyProgressBar.Maximum = INITIAL_DELAY;

            //Add a word when the form loads, rather than wait for the first tick
            DisplayWord();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DisplayWord();
            if (listBox1.Items.Count > 7)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Game Over");
                timer1.Stop();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //if the key is not 'enter' then add the character to the string buffer
            if (e.KeyCode != Keys.Enter)
            {
                _stringBuffer += e.KeyCode;
            }
            else
            {
                string temp = _stringBuffer.ToLower(); //adjust for shift/capslock
                _stringBuffer = String.Empty; //reset string buffer
                if (listBox1.Items.Contains(temp)) // see if the string is in the list
                {
                    listBox1.Items.Remove(temp); //remove the word
                    listBox1.Refresh(); //refresh the display
                    //speed the game up
                    if (timer1.Interval > 400) timer1.Interval -= 10;
                    if (timer1.Interval > 250) timer1.Interval -= 7;
                    if (timer1.Interval > 100) timer1.Interval -= 5;
                    difficultyProgressBar.Value = INITIAL_DELAY - timer1.Interval;

                    //correct key : update stats
                    _stats.Update(true);
                }
                else
                {
                    //incorrect key : update stats : game speed stays the same
                    _stats.Update(false);
                }
                //update status labels
                correctLabel.Text = "Correct: " + _stats.Correct;
                missedLabel.Text = "Missed: " + _stats.Missed;
                totalLabel.Text = "Total: " + _stats.Total;
                accuracyLabel.Text = "Accuracy: " + _stats.Accuracy + "%";
            }
        }

        // Refactored this to it's own function, rather than have the same line in the
        //  form load and the timer_tick
        private void DisplayWord()
        {
            listBox1.Items.Add(_words.GetRandomWord());
        }
    }
}
