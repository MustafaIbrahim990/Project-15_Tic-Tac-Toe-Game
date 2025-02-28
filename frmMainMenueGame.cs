using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_Game_Project
{
    public partial class frmMainMenueGame : Form
    {
      
        //--------------------------------------------------//

        enum enPlayer { Player1, Player2 };

        enum enWinner { Player1, Player2, Draw, GameInProgress };

        struct stGameStatus
        {
            public byte PlayCount;
            public enWinner Winner;
            public bool GameOver;
        }

        enPlayer PlayerTurn = enPlayer.Player1;
        stGameStatus GameStatus;

        //--------------------------------------------------//

        public frmMainMenueGame()
        {
            InitializeComponent();
        }

        private void frmMainMenueGame_Load(object sender, EventArgs e)
        {

        }

        private void frmMainMenueGame_Paint(object sender, PaintEventArgs e)
        {
            Pen Pen = new Pen(Color.White);

            Pen.Width = 15;

            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(Pen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(Pen, 400, 460, 1050, 460);
            e.Graphics.DrawLine(Pen, 610, 140, 610, 620);
            e.Graphics.DrawLine(Pen, 840, 140, 840, 620);
        }

        //--------------------------------------------------//

        private void btn_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }

        //--------------------------------------------------//

        private void EnableButton(Button btn)
        {
            btn.Enabled = true;
        }

        private void EnableAllButtons()
        {
            EnableButton(btn1);
            EnableButton(btn2);
            EnableButton(btn3);
            EnableButton(btn4);
            EnableButton(btn5);
            EnableButton(btn6);
            EnableButton(btn7);
            EnableButton(btn8);
            EnableButton(btn9);
        }

        private void DisEnableButton(Button btn)
        {
            btn.Enabled = false;
        }

        private void DisEnableAllButtons()
        {
            DisEnableButton(btn1);
            DisEnableButton(btn2);
            DisEnableButton(btn3);
            DisEnableButton(btn4);
            DisEnableButton(btn5);
            DisEnableButton(btn6);
            DisEnableButton(btn7);
            DisEnableButton(btn8);
            DisEnableButton(btn9);
        }

        //--------------------------------------------------//

        public void EndGame()
        {
            lblTurn.Text = "Game Over";

            switch(GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player1";
                    break;

                case enWinner.Player2:
                    lblWinner.Text = "Player2";
                    break;

                default:
                    lblWinner.Text = "Draw";
                    break;
            }
            DisEnableAllButtons();
            MessageBox.Show("Game Over", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString()) 
            {

                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
            }
            GameStatus.GameOver = false;
            return false;
        }

        public void CheckWinner()
        {
            //Check Rows.
            //Check Row1
            if (CheckValues(btn1, btn2, btn3))
                return;

            //Check Row2
            if (CheckValues(btn4, btn5, btn6))
                return;

            //Check Row3
            if (CheckValues(btn7, btn8, btn9))
                return;

            //Check Coloumn.
            //Check Coloumn1
            if (CheckValues(btn1, btn4, btn7))
                return;

            //Check Coloumn2
            if (CheckValues(btn2, btn5, btn8))
                return;

            //Check Coloumn3
            if (CheckValues(btn3, btn6, btn9))
                return;

            //Check Diagonal.
            //Check Diagonal1
            if (CheckValues(btn1, btn5, btn9))
                return;

            //Check Diagonal2
            if (CheckValues(btn3, btn5, btn7))
                return;
        }

        //--------------------------------------------------//

        public void ChangeImage(Button btn)
        {
            if (btn.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:

                        GameStatus.PlayCount++;
                        btn.Image = Properties.Resources.X;
                        btn.Tag = "X";
                        PlayerTurn = enPlayer.Player2;
                        lblTurn.Text = "Player2";
                        CheckWinner();
                        break;
                        
                    case enPlayer.Player2:

                        GameStatus.PlayCount++;
                        btn.Image = Properties.Resources.O;
                        btn.Tag = "O";
                        PlayerTurn = enPlayer.Player1;
                        lblTurn.Text = "Player1";
                        CheckWinner();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (GameStatus.PlayCount == 9 && GameStatus.GameOver == false)  
            {
                GameStatus.Winner = enWinner.Draw;
                GameStatus.GameOver = true;
                EndGame();
            }
        }

        //--------------------------------------------------//

        private void RestButton(Button btn)
        {
            btn.Tag = "?";
            btn.Image = Properties.Resources.question_mark_96;
            btn.BackColor = Color.Black;
        }

        private void RestAllButton()
        {
            //Rest Buttons.
            RestButton(btn1);
            RestButton(btn2);
            RestButton(btn3);
            RestButton(btn4);
            RestButton(btn5);
            RestButton(btn6);
            RestButton(btn7);
            RestButton(btn8);
            RestButton(btn9);
        }

        private void RestartGame()
        {
            PlayerTurn = enPlayer.Player1;
            lblTurn.Text = "Player1";
            lblWinner.Text = "In Progress";
            GameStatus.Winner = enWinner.GameInProgress;
            GameStatus.GameOver = false;
            GameStatus.PlayCount = 0;

            //RestButton.
            RestAllButton();

            //EnableButtons.
            EnableAllButtons();
        }

        //--------------------------------------------------//

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        //--------------------------------------------------//
    }
}
