using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Form1.cs, Nikkala Thomson, CIT 301C

namespace Math_Quiz
{
    public partial class Form1 : Form
    {
        Random randomizer = new Random();   // Generate random number
        int addend1; int addend2;  // Store numbers for the addition problem
        int minuend;  int subtrahend;  // Store the numbers for the subtraction problem
        int multiplicand;  int multiplier; // Store the numbers for the multiplication problem
        int dividend;  int divisor;  // Store the numbers for the division problem
        int timeLeft; //track remaining time

        public void StartTheQuiz()
        {
            // Generate random numbers for the addition process
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convert to strings
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // Set NumericUpDown control to zero
            sum.Value = 0;

            // Fill in the subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timeLabel.BackColor = Color.LightGray;  // Set default background color to LightGray
            timer1.Start();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // Reward user for success
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                if (timeLeft==6)  // Set background color to red at 5 seconds left
                {
                    timeLabel.BackColor = Color.Red;
                }
                // Keep counting down
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show 
                // a MessageBox, then fill in the answers.
                timer1.Stop();
                timeLabel.BackColor = Color.LightGray;  // Reset timer background color
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        /// <returns>True if correct, false otherwise.</returns>
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                 && (minuend - subtrahend == difference.Value)
                 && (multiplicand * multiplier == product.Value)
                 && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }


        // The answer_Enter functions convert the user's answers to integers
        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        //ring bell if addition correct
        private void ring_Bell(object sender, EventArgs e)
        {
            if (addend1 + addend2 == sum.Value)
                SystemSounds.Beep.Play();
        }

        //ring bell if subtraction correct
        private void ring_bell2(object sender, EventArgs e)
        {
            if (minuend - subtrahend == difference.Value)
                SystemSounds.Beep.Play();
        }

        private void ring_bell3(object sender, EventArgs e)
        {
            if (multiplicand * multiplier == product.Value)
                SystemSounds.Beep.Play();
        }

        private void ring_bell4(object sender, EventArgs e)
        {
            if (dividend / divisor == quotient.Value)
                SystemSounds.Beep.Play();
        }
    }
}
