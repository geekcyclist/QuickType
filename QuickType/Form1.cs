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
        private string _stringBuffer = String.Empty;
        
        public Form1()
        {
            InitializeComponent();
            //HFCSharp Book sets these properties in the form designer            
            this.KeyPreview = true;
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
            //NOT HERE EVEN THOUGH THE BOOK PUT CODE HERE!
            if (e.KeyCode != Keys.Enter)
            {
                _stringBuffer += e.KeyCode;
            }
            else
            {
                string temp = _stringBuffer.ToLower();
                _stringBuffer = String.Empty;
                //If the user pressed the right key, remove it and increase the speed (decrease interval)
                //if (listBox1.Items.Contains(e.KeyCode))
                if (listBox1.Items.Contains(temp))
                {
                    //listBox1.Items.Remove(e.KeyCode);
                    listBox1.Items.Remove(temp);
                    listBox1.Refresh();
                    if (timer1.Interval > 400) timer1.Interval -= 10;
                    if (timer1.Interval > 250) timer1.Interval -= 7;
                    if (timer1.Interval > 100) timer1.Interval -= 5;
                    difficultyProgressBar.Value = INITIAL_DELAY - timer1.Interval;

                    //correct key : update stats
                    _stats.Update(true);
                }
                else
                {
                    //incorrect key
                    _stats.Update(false);
                }
                //update status labels
                correctLabel.Text = "Correct: " + _stats.Correct;
                missedLabel.Text = "Missed: " + _stats.Missed;
                totalLabel.Text = "Total: " + _stats.Total;
                accuracyLabel.Text = "Accuracy: " + _stats.Accuracy + "%";
            }

        }


        private void DisplayWord()
        {
            listBox1.Items.Add(_words.GetRandomWord());
        }
    }
}
