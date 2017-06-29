using System;
using System.Windows.Forms;
using System.Drawing;

namespace BetDog
{
    public class Dog
    {
        public string Name = "";
        public PictureBox DogPic;

        public int StartingPos;
        public int RacetrackLength;
        public int Location;
        public Random random;

        public void Init(int length)
        {
            StartingPos = 0;
            RacetrackLength = length;
        }

        public bool Run()
        {
            int runLenght = random.Next(3, 12);
            Point p = DogPic.Location;
            p.X = p.X + runLenght;
            DogPic.Location = p;

            if (p.X > RacetrackLength - 80)
                return false;
            else
                return true;
        }

        public void RestartToBegin()
        {
            DogPic.Location = new Point(StartingPos, DogPic.Location.Y);
        }
    }

    public class Guy
    {
        public string Name = "";
        public string myLabel = "";
        public int Cash = 0;

        public RadioButton btn;
        public Bet MyBet;
        public void UpdateLabel()
        {
            btn.Text = Name + " Has " + Cash;
        }

        public void ClearBet()
        {
            MyBet.Amount = 0;
            MyBet.Dog = 0;
        }

        public bool PlaceBet(int amount, int dogNum)
        {
            if (Cash < amount)
                return false;

            MyBet.Amount = amount;
            MyBet.Dog = dogNum;
            return true;
        }

        public void Collect(int winner)
        {
            Cash = Cash + MyBet.PayOut(winner);
            UpdateLabel();
        }
    }

    public class Bet
    {
        public int Amount;
        public int Dog;
        public Guy Bettor;

        public string GetDescription()
        {
            return "";
        }

        public int PayOut(int winner)
        {
            int getMoney = 0;
            if (winner == Dog)
                getMoney = Amount;
            else
                getMoney = -Amount;

            return getMoney;
        }
    }
}
