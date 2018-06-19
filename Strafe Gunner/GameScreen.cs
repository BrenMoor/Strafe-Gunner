using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Media;


namespace Strafe_Gunner
{
    public partial class GameScreen : UserControl
    {
        //Lists for boxes and bullets
        List<int> xBox = new List<int>();
        List<int> yBox = new List<int>();
        List<int> xBullet = new List<int>();
        List<int> yBullet = new List<int>();
        List<string> directionBullet = new List<string>();
        List<int> xBullet1 = new List<int>();
        List<int> yBullet1 = new List<int>();
        List<string> directionBullet1 = new List<string>();
        List<Rectangle> Box = new List<Rectangle>();
        List<int> BoxHealth = new List<int>();
        //Shot variables
        int player1ShotCount, player2ShotCount;
        int shotSpeed = 20;
        //Power ups
        int p2ShotRate = 7;
        int p1ShotRate = 7;
        Boolean topPowerVisible = true, bottomPowerVisible = true;
        //Variables for both players
        int widthPlayer1 = 40;
        int heightPlayer1 = 40;
        int widthPlayer2 = 40;
        int heightPlayer2 = 40;
        int p1Speed = 5;
        int p2Speed = 5;
        //Variables for player 1
        int xPlayer1 = 1220;
        int yPlayer1 = 345;
        //Variables for player 2
        int xPlayer2 = 25;
        int yPlayer2 = 345;
        //Score integers
        int player1Score, player2Score;
        //Countdown variable (I know a boolean would have worked better but 0 and 1 work fine.)
        int Countdown = 0;
        //Fonts for score and countdown
        Font scoreTokarev = new Font("Tokarev", 40, FontStyle.Bold);
        Font countTokarev = new Font("Tokarev", 80, FontStyle.Bold);
        //Brush for 
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        //Booleans for keys for player 1
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, qKeyDown, mKeyDown, aKeyDown, wKeyDown, sKeyDown, dKeyDown, facingUp1, facingUp2, facingLeft1, facingLeft2, facingRight1, facingRight2, facingDown1, facingDown2;
        public GameScreen()
        {
            InitializeComponent();
    
           // music.PlayLooping();
            //Left side four front boxes
            xBox.Add(580);
            xBox.Add(580);
            xBox.Add(580);
            xBox.Add(580);
            yBox.Add(400);
            yBox.Add(350);
            yBox.Add(300);
            yBox.Add(250);
            //Right side front boxes
            xBox.Add(650);
            xBox.Add(650);
            xBox.Add(650);
            xBox.Add(650);
            yBox.Add(400);
            yBox.Add(350);
            yBox.Add(300);
            yBox.Add(250);
            //Very top 2 boxes
            xBox.Add(470);
            xBox.Add(760);
            yBox.Add(100);
            yBox.Add(100);
            //Second from bottom
            xBox.Add(470);
            xBox.Add(760);
            yBox.Add(550);
            yBox.Add(550);
            //Bottom double boxes
            xBox.Add(250);
            xBox.Add(980);
            xBox.Add(250);
            xBox.Add(980);
            yBox.Add(450);
            yBox.Add(450);
            yBox.Add(500);
            yBox.Add(500);
            //Top double boxes
            xBox.Add(250);
            xBox.Add(980);
            xBox.Add(250);
            xBox.Add(980);
            yBox.Add(200);
            yBox.Add(200);
            yBox.Add(150);
            yBox.Add(150);
            //Middle double boxes
            xBox.Add(400);
            xBox.Add(400);
            xBox.Add(830);
            xBox.Add(830);
            yBox.Add(350);
            yBox.Add(300);
            yBox.Add(300);
            yBox.Add(350);
            for (int i = 0; i < 24; i++)
            {
                BoxHealth.Add(0);
            }
            for (int i = 0; i < xBox.Count; i++)
            {
                Box.Add(new Rectangle(xBox[i], yBox[i], 50, 50));
            }
        }
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            if (topPowerVisible == true)
            {
                e.Graphics.DrawImage(Properties.Resources.FastShot, 615, 150, 50, 50);
            }
            if (bottomPowerVisible == true)
            {
                e.Graphics.DrawImage(Properties.Resources.FastMove, 615, 525, 50, 50);
            }
            e.Graphics.DrawString(player2Score + "", scoreTokarev, blackBrush, 585, 15);
            e.Graphics.DrawString(player1Score + "", scoreTokarev, blackBrush, 643, 15);
            //Player1
            if (facingUp1 == true)
                e.Graphics.DrawImage(Properties.Resources.BLUETANKUP, xPlayer1, yPlayer1, widthPlayer1, heightPlayer1);
            if (facingDown1 == true)
                e.Graphics.DrawImage(Properties.Resources.BLUETANKDOWN, xPlayer1, yPlayer1, widthPlayer1, heightPlayer1);
            if (facingRight1 == true)
                e.Graphics.DrawImage(Properties.Resources.BLUETANKRIGHT, xPlayer1, yPlayer1, widthPlayer1, heightPlayer1);
            if (facingLeft1 == true)
                e.Graphics.DrawImage(Properties.Resources.BLUETANK, xPlayer1, yPlayer1, widthPlayer1, heightPlayer1);
            if (facingUp1 == false && facingRight1 == false && facingLeft1 == false && facingDown1 == false)
                e.Graphics.DrawImage(Properties.Resources.BLUETANK, xPlayer1, yPlayer1, widthPlayer1, heightPlayer1);
            //Player2
            if (facingUp2 == true)
                e.Graphics.DrawImage(Properties.Resources.REDTANKUP, xPlayer2, yPlayer2, widthPlayer2, heightPlayer2);
            if (facingDown2 == true)
                e.Graphics.DrawImage(Properties.Resources.REDTANKDOWN1, xPlayer2, yPlayer2, widthPlayer2, heightPlayer2);
            if (facingLeft2 == true)
                e.Graphics.DrawImage(Properties.Resources.REDTANKLEFT, xPlayer2, yPlayer2, widthPlayer2, heightPlayer2);
            if (facingRight2 == true)
                e.Graphics.DrawImage(Properties.Resources.REDTANK, xPlayer2, yPlayer2, widthPlayer2, heightPlayer2);
            if (facingUp2 == false && facingRight2 == false && facingLeft2 == false && facingDown2 == false)
                e.Graphics.DrawImage(Properties.Resources.REDTANK, xPlayer2, yPlayer2, widthPlayer2, heightPlayer2);
            //Drawing bullets
            for (int i = 0; i < directionBullet.Count; i++)
            {
                e.Graphics.FillEllipse(blackBrush, xBullet[i], yBullet[i], 10, 10);
            }
            for (int i = 0; i < directionBullet1.Count; i++)
            {
                e.Graphics.FillEllipse(blackBrush, xBullet1[i], yBullet1[i], 10, 10);
            }
            //Drawing boxes
            for (int i = 0; i < xBox.Count; i++)
            {
                e.Graphics.DrawImage(Properties.Resources.Box, xBox[i], yBox[i], 50, 50);
            }
        }
        //Key down for both players
        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    leftArrowDown = true;
                    facingLeft1 = true;
                    facingDown1 = false;
                    facingRight1 = false;
                    facingUp1 = false;
                    break;
                case Keys.S:
                    downArrowDown = true;
                    facingLeft1 = false;
                    facingDown1 = true;
                    facingRight1 = false;
                    facingUp1 = false;
                    break;
                case Keys.D:
                    rightArrowDown = true;
                    facingLeft1 = false;
                    facingDown1 = false;
                    facingRight1 = true;
                    facingUp1 = false;
                    break;
                case Keys.W:
                    upArrowDown = true;
                    facingLeft1 = false;
                    facingDown1 = false;
                    facingRight1 = false;
                    facingUp1 = true;
                    break;
                case Keys.Z:
                    mKeyDown = true;
                    break;
                case Keys.Space:
                    qKeyDown = true;
                    break;
                case Keys.Left:
                    aKeyDown = true;
                    facingLeft2 = true;
                    facingDown2 = false;
                    facingRight2 = false;
                    facingUp2 = false;
                    break;
                case Keys.Up:
                    wKeyDown = true;
                    facingLeft2 = false;
                    facingDown2 = false;
                    facingRight2 = false;
                    facingUp2 = true;
                    break;
                case Keys.Down:
                    sKeyDown = true;
                    facingLeft2 = false;
                    facingDown2 = true;
                    facingRight2 = false;
                    facingUp2 = false;
                    break;
                case Keys.Right:
                    dKeyDown = true;
                    facingLeft2 = false;
                    facingDown2 = false;
                    facingRight2 = true;
                    facingUp2 = false;
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {     
            if (player2Score == 3)
            {
                Countdown = 1; 
                gameTimer.Stop();
                Form gs= this.FindForm();
                gs.Controls.Remove(this);
                EndScreen es = new EndScreen();
                gs.Controls.Add(es);
                es.Focus();
                es.Location = new Point((Screen.PrimaryScreen.Bounds.Width - es.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - es.Height) / 2);

            }
            if (player1Score == 3)
            {
                Countdown = 1;
                gameTimer.Stop();
                Form gs = this.FindForm();
                gs.Controls.Remove(this);
                EndScreen2 es2 = new EndScreen2();
                gs.Controls.Add(es2);
                es2.Focus();
                es2.Location = new Point((Screen.PrimaryScreen.Bounds.Width - es2.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - es2.Height) / 2);

            }
            if (Countdown == 0)
            {
                CountDown();
            }
            player1ShotCount++;
            player2ShotCount++;
            int x1 = xPlayer1;
            int y1 = yPlayer1;
            int x2 = xPlayer2;
            int y2 = yPlayer2;
            //Player 1 movement (move with arrow keys)
            if (leftArrowDown == true)
                xPlayer1 = xPlayer1 - p1Speed;
            if (rightArrowDown == true)
                xPlayer1 = xPlayer1 + p1Speed;
            if (upArrowDown == true)
                yPlayer1 = yPlayer1 - p1Speed;
            if (downArrowDown == true)
                yPlayer1 = yPlayer1 + p1Speed;
            //Player 2 movement (move with WASD)
            if (aKeyDown == true)
                xPlayer2 = xPlayer2 - p2Speed;
            if (dKeyDown == true)
                xPlayer2 = xPlayer2 + p2Speed;
            if (wKeyDown == true)
                yPlayer2 = yPlayer2 - p2Speed;
            if (sKeyDown == true)
                yPlayer2 = yPlayer2 + p2Speed;
            //Player 1 borders
            if (yPlayer1 < 71)
                yPlayer1 = yPlayer1 + p1Speed;
            if (xPlayer1 < 6)
                xPlayer1 = xPlayer1 + p1Speed;
            if (yPlayer1 > 671)
                yPlayer1 = yPlayer1 - p1Speed;
            if (xPlayer1 > 1253)
                xPlayer1 = xPlayer1 - p1Speed;
            //Player 2 borders
            if (yPlayer2 < 71)
                yPlayer2 = yPlayer2 + p2Speed;
            if (xPlayer2 < 6)
                xPlayer2 = xPlayer2 + p2Speed;
            if (yPlayer2 > 671)
                yPlayer2 = yPlayer2 - p2Speed;
            if (xPlayer2 > 1253)
                xPlayer2 = xPlayer2 - p2Speed;
            //Player shots
            if (mKeyDown == true && facingUp1 == true && player1ShotCount > p1ShotRate)
            {
                xBullet.Add(xPlayer1 + 15);
                yBullet.Add(yPlayer1);
                directionBullet.Add("Up");
                player1ShotCount = 0;    
            }
            if (mKeyDown == true && facingDown1 == true && player1ShotCount > p1ShotRate)
            {
                xBullet.Add(xPlayer1 + 15);
                yBullet.Add(yPlayer1 + 28);
                directionBullet.Add("Down");
                player1ShotCount = 0;
            }
            if (mKeyDown == true && facingLeft1 == true && player1ShotCount > p1ShotRate)
            {
                xBullet.Add(xPlayer1);
                yBullet.Add(yPlayer1 + 13);
                directionBullet.Add("Left");
                player1ShotCount = 0;
            }
            if (mKeyDown == true && facingRight1 == true && player1ShotCount > p1ShotRate)
            {
                xBullet.Add(xPlayer1 + 30);
                yBullet.Add(yPlayer1 + 14);
                directionBullet.Add("Right");
                player1ShotCount = 0;   
            }
            if (qKeyDown == true && facingUp2 == true && player2ShotCount > p2ShotRate)
            {
                xBullet1.Add(xPlayer2 + 15);
                yBullet1.Add(yPlayer2);
                directionBullet1.Add("Up");
                player2ShotCount = 0;
            }
            if (qKeyDown == true && facingDown2 == true && player2ShotCount > p2ShotRate)
            {
                xBullet1.Add(xPlayer2 + 15);
                yBullet1.Add(yPlayer2 + 28);
                directionBullet1.Add("Down");
                player2ShotCount = 0;
            }
            if (qKeyDown == true && facingLeft2 == true && player2ShotCount > p2ShotRate)
            {
                xBullet1.Add(xPlayer2);
                yBullet1.Add(yPlayer2 + 13);
                directionBullet1.Add("Left");
                player2ShotCount = 0;
            }
            if (qKeyDown == true && facingRight2 == true && player2ShotCount > p2ShotRate)
            {
                xBullet1.Add(xPlayer2 + 30);
                yBullet1.Add(yPlayer2 + 14);
                directionBullet1.Add("Right");
                player2ShotCount = 0;
            }
            //Detremining direction bullet should travel
            for (int i = 0; i < directionBullet.Count; i++)
            {
                if (directionBullet[i] == "Up")
                {
                    yBullet[i] = yBullet[i] - shotSpeed;
                }
                if (directionBullet[i] == "Down")
                {
                    yBullet[i] = yBullet[i] + shotSpeed;
                }
                if (directionBullet[i] == "Left")
                {
                    xBullet[i] = xBullet[i] - shotSpeed;
                }
                if (directionBullet[i] == "Right")
                {
                    xBullet[i] = xBullet[i] + shotSpeed;
                }
            }
            for (int i = 0; i < directionBullet1.Count; i++)
            {
                if (directionBullet1[i] == "Up")
                {
                    yBullet1[i] = yBullet1[i] - shotSpeed;
                }
                if (directionBullet1[i] == "Down")
                {
                    yBullet1[i] = yBullet1[i] + shotSpeed;
                }
                if (directionBullet1[i] == "Left")
                {
                    xBullet1[i] = xBullet1[i] - shotSpeed;
                }
                if (directionBullet1[i] == "Right")
                {
                    xBullet1[i] = xBullet1[i] + shotSpeed;
                }
            }
            //Setting rectangles for walls and players
            Rectangle Player1 = new Rectangle(xPlayer1, yPlayer1, widthPlayer1, heightPlayer1);
            Rectangle Player2 = new Rectangle(xPlayer2, yPlayer2, widthPlayer2, heightPlayer2);
            Rectangle TopWall = new Rectangle(0, 30, 1920, 30);
            Rectangle LeftWall = new Rectangle(3, 0, 3, 1080);
            Rectangle BottomWall = new Rectangle(0, 694, 1920, 694);
            Rectangle RightWall = new Rectangle(1273, 0, 1273, 1080);

            //Intercepts with boxes, players, walls
            bool boxHit = false;
            for (int i = 0; i < directionBullet.Count; i++)
            {
                if (boxHit)
                {
                    break;
                }
                Rectangle Bullet = new Rectangle(xBullet[i], yBullet[i], 5, 5);
                for (int b = 0; b < Box.Count; b++)
                {
                    if (Bullet.IntersectsWith(Box[b]))
                    {
                        xBullet.RemoveAt(i);
                        yBullet.RemoveAt(i);
                        directionBullet.RemoveAt(i);
                        BoxHealth[b]++;
                        boxHit = true;
                        break;
                    }
                    if (Bullet.IntersectsWith(TopWall))
                    {
                        xBullet.RemoveAt(i);
                        yBullet.RemoveAt(i);
                        directionBullet.RemoveAt(i);
                        break;
                    }
                    if (Bullet.IntersectsWith(LeftWall))
                    {
                        xBullet.RemoveAt(i);
                        yBullet.RemoveAt(i);
                        directionBullet.RemoveAt(i);
                        break;
                    }
                    if (Bullet.IntersectsWith(BottomWall))
                    {
                        xBullet.RemoveAt(i);
                        yBullet.RemoveAt(i);
                        directionBullet.RemoveAt(i);
                        break;
                    }
                    if (Bullet.IntersectsWith(RightWall))
                    {
                        xBullet.RemoveAt(i);
                        yBullet.RemoveAt(i);
                        directionBullet.RemoveAt(i);
                        break;
                    }
                    if (Bullet.IntersectsWith(Player2))
                    {            
                        x1 = 1220;
                        y1 = 345;
                        x2 = 25;
                        y2 = 345;
                        xPlayer2 = 25;
                        yPlayer2 = 345;
                        xPlayer1 = 1220;
                        yPlayer1 = 345;
                        xBullet1.Clear();
                        yBullet1.Clear();
                        directionBullet1.Clear();
                        xBullet.Clear();
                        yBullet.Clear();
                        directionBullet.Clear();
                        player1Score++;
                        Countdown = 0;
                        break;
                    }
                }
            }
            for (int i = 0; i < Box.Count; i++)
            {
                if (BoxHealth[i] == 5)
                {
                    xBox.RemoveAt(i);
                    yBox.RemoveAt(i);
                    BoxHealth.RemoveAt(i);
                    Box.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < directionBullet1.Count; i++)
            {
                Rectangle Bullet1 = new Rectangle(xBullet1[i], yBullet1[i], 5, 5);
                for (int b = 0; b < Box.Count; b++)
                {
                    if (Bullet1.IntersectsWith(Box[b]))
                    {
                        xBullet1.RemoveAt(i);
                        yBullet1.RemoveAt(i);
                        directionBullet1.RemoveAt(i);
                        BoxHealth[b]++;
                        boxHit = true;
                        break;
                    }
                    if (Bullet1.IntersectsWith(TopWall))
                    {
                        xBullet1.RemoveAt(i);
                        yBullet1.RemoveAt(i);
                        directionBullet1.RemoveAt(i);
                        break;
                    }
                    if (Bullet1.IntersectsWith(LeftWall))
                    {
                        xBullet1.RemoveAt(i);
                        yBullet1.RemoveAt(i);
                        directionBullet1.RemoveAt(i);
                        break;
                    }
                    if (Bullet1.IntersectsWith(BottomWall))
                    {
                        xBullet1.RemoveAt(i);
                        yBullet1.RemoveAt(i);
                        directionBullet1.RemoveAt(i);
                        break;
                    }
                    if (Bullet1.IntersectsWith(RightWall))
                    {
                        xBullet1.RemoveAt(i);
                        yBullet1.RemoveAt(i);
                        directionBullet1.RemoveAt(i);
                        break;
                    }
                    if (Bullet1.IntersectsWith(Player1))
                    {
                        x1 = 1220;
                        y1 = 345;
                        x2 = 25;
                        y2 = 345;
                        xPlayer1 = 1220;
                        yPlayer1 = 345;
                        xPlayer2 = 25;
                        yPlayer2 = 345;
                        xBullet1.Clear();
                        yBullet1.Clear();
                        directionBullet1.Clear();
                        xBullet.Clear();
                        yBullet.Clear();
                        directionBullet.Clear();
                        player2Score++;
                        Countdown = 0;
                        break;
                    }
                }
            }
            for (int i = 0; i < Box.Count; i++)
            {
                if (BoxHealth[i] == 5)
                {
                    xBox.RemoveAt(i);
                    yBox.RemoveAt(i);
                    BoxHealth.RemoveAt(i);
                    Box.RemoveAt(i);
                    break;
                }
            }

            for (int i = 0; i < Box.Count; i++)
            {
                if (Player1.IntersectsWith(Box[i]))
                {
                    xPlayer1 = x1;
                    yPlayer1 = y1;
                }
            }
            for (int i = 0; i < xBox.Count; i++)
            {
                if (Player2.IntersectsWith(Box[i]))
                {
                    xPlayer2 = x2;
                    yPlayer2 = y2;
                }
            }
            if (Player2.IntersectsWith(Player1))
            {
                xPlayer2 = x2;
                yPlayer2 = y2;
            }
            if (Player1.IntersectsWith(Player2))
            {
                xPlayer1 = x1;
                yPlayer1 = y1;
            }
            if (Player1.IntersectsWith(BottomWall))
            {
                xPlayer1 = x1;
                yPlayer1 = y1;
            }
            if (Player1.IntersectsWith(RightWall))
            {
                xPlayer1 = x1;
                yPlayer1 = y1;
            }
            if (Player2.IntersectsWith(BottomWall))
            {
                xPlayer2 = x2;
                yPlayer2 = y2;
            }
            if (Player2.IntersectsWith(RightWall))
            {
                xPlayer2 = x2;
                yPlayer2 = y2;
            }
            if (topPowerVisible == true)
            {
                Rectangle topPower = new Rectangle(624, 150, 50, 50);

                if (Player2.IntersectsWith(topPower))
                {
                    p2ShotRate = 3;
                    topPowerVisible = false;
                }
                if (Player1.IntersectsWith(topPower))
                {
                    p1ShotRate = 3;
                    topPowerVisible = false;
                }
            }
            if (bottomPowerVisible == true)
            {
                Rectangle bottomPower = new Rectangle(624, 525, 50, 50);

                if (Player2.IntersectsWith(bottomPower))
                {
                    p2Speed = 10;
                    bottomPowerVisible = false;
                }
                if (Player1.IntersectsWith(bottomPower))
                {
                    p1Speed =10;
                    bottomPowerVisible = false;
                }
            }
            Refresh();
        }
        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //Keys down for both players
            switch (e.KeyCode)
            {
                case Keys.A:
                    leftArrowDown = false;
                    break;
                case Keys.S:
                    downArrowDown = false;
                    break;
                case Keys.D:
                    rightArrowDown = false;
                    break;
                case Keys.W:
                    upArrowDown = false;
                    break;
                case Keys.Left:
                    aKeyDown = false;
                    break;
                case Keys.Up:
                    wKeyDown = false;
                    break;
                case Keys.Down:
                    sKeyDown = false;
                    break;
                case Keys.Right:
                    dKeyDown = false;
                    break;
                case Keys.Z:
                    mKeyDown = false;
                    break;
                case Keys.Space:
                    qKeyDown = false;
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;

            }
        }  
        public void CountDown()
        {
            //Countdown from 3 at the start of a game and after a player is eliminated.
            Graphics g = this.CreateGraphics();
            gameTimer.Enabled = false;
            for (int i = 3; i > 0; i--)
            { 
                g.DrawString("" + i, countTokarev, blackBrush, (this.Width / 2) - 47, (this.Height / 2) - 38);
                Thread.Sleep(1000);
                Refresh();
                Countdown++;

            }
            //Reset variables after powerups
            gameTimer.Enabled = true;
            p1ShotRate = 7;
            p2ShotRate = 7;
            topPowerVisible = true;
            p1Speed = 5;
            p2Speed = 5;
            bottomPowerVisible = true;
        }
    }
}
