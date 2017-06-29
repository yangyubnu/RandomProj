using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetDog
{
    public partial class Form1 : Form
    {
        int CurSelect = 0;

        Guy[] GuyArray = new Guy[3];
        Dog[] DogArray = new Dog[4];

        public Form1()
        {
            InitializeComponent();
            InitGuyData();
            InitDogData();

            radioButton1.Select();
        }

        public void InitGuyData()
        {
            GuyArray[0] = new Guy() { Name = "Ako", Cash = 100, MyBet = new Bet() {Bettor = GuyArray[0] }, btn = radioButton1, myLabel = "" };
            GuyArray[1] = new Guy() { Name = "Bob", Cash = 100, MyBet = new Bet() { Bettor = GuyArray[1] }, btn = radioButton2, myLabel = "" };
            GuyArray[2] = new Guy() { Name = "Cow", Cash = 100, MyBet = new Bet() { Bettor = GuyArray[2] }, btn = radioButton3, myLabel = "" };

            for (int i = 0; i < GuyArray.Length; i++)
                GuyArray[i].UpdateLabel();
        }

        public void InitDogData()
        {
            Random randomRun = new Random();

            DogArray[0] = new Dog() { Name = "dog1", DogPic = Dog1, random = randomRun };
            DogArray[1] = new Dog() { Name = "dog2", DogPic = Dog2, random = randomRun };
            DogArray[2] = new Dog() { Name = "dog3", DogPic = Dog3, random = randomRun };
            DogArray[3] = new Dog() { Name = "dog4", DogPic = Dog4, random = randomRun };

            for (int i = 0; i < DogArray.Length; i++)
                DogArray[i].Init(Scene.Size.Width);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GuyArray[0].UpdateLabel();
            label1.Text = "Mininum bet " + numericUpDown1.Minimum;
            label2.Text = "Ako";
            CurSelect = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            GuyArray[1].UpdateLabel();
            label1.Text = "Mininum bet " + numericUpDown1.Minimum;
            label2.Text = "Bob";
            CurSelect = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            GuyArray[2].UpdateLabel();
            label1.Text = "Mininum bet " + numericUpDown1.Minimum;
            label2.Text = "Cow";
            CurSelect = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GuyArray[CurSelect].PlaceBet((int)numericUpDown1.Value, (int)numericUpDown2.Value))
            {
                if (CurSelect == 0)
                    textBox1.Text = "Ako's bet " + numericUpDown1.Value + " on #" + numericUpDown2.Value + " Dog";
                else if (CurSelect == 1)
                    textBox2.Text = "Bob's bet " + numericUpDown1.Value + " on #" + numericUpDown2.Value + " Dog";
                else if (CurSelect == 2)
                    textBox3.Text = "Cow's bet " + numericUpDown1.Value + " on #" + numericUpDown2.Value + " Dog";
            }
            else
            {
                if (CurSelect == 0)
                    textBox1.Text = "Ako's has't placed a bet! ";
                else if (CurSelect == 1)
                    textBox2.Text = "Bob's has't placed a bet! ";
                else if (CurSelect == 2)
                    textBox3.Text = "Cow's has't placed a bet! ";

                GuyArray[CurSelect].ClearBet();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DogArray.Length; i++)
                DogArray[i].RestartToBegin();

            button1.Enabled = false;
            button2.Enabled = false;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int winner = 0;

            for (int i = 0; i < DogArray.Length; i++)
            {
                if (!DogArray[i].Run())
                {
                    timer1.Stop();
                    winner = i + 1;
                    Settlement(winner);
                    break;
                } 
            }
        }

        public void Settlement(int winner)
        {
            MessageBox.Show("Dog #" + winner + "is WIN!");

            button1.Enabled = true;
            button2.Enabled = true;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            for (int i = 0; i < GuyArray.Length; i++)
                GuyArray[i].Collect(winner);

            for (int i = 0; i < DogArray.Length; i++)
                DogArray[i].RestartToBegin();
        }
    }
}
