using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Num
{
    public partial class Form1 : Form
    {
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        private bool bCapsDown = false;
        private bool bShift = false;
        private bool bCapsStart = false;
        private int iStartX, iStartY;

        private Button[] btnsSymbol;
        private Button[] btnsLetter;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
                cp.Parent = IntPtr.Zero; // Keep this line only if you used UserControl
                return cp;
            }
        }

        public static bool CapsLockStatus
        {
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs);
                return (bs[0x14] == 1);
            }
        }

        private void InitButton()
        {
            //line 1
            btn0.Tag = new string[2] { "0", "{)}" };
            btn1.Tag = new string[2] { "1", "{!}" };
            btn2.Tag = new string[2] { "2", "{@}" };
            btn3.Tag = new string[2] { "3", "{#}" };
            btn4.Tag = new string[2] { "4", "{$}" };
            btn5.Tag = new string[2] { "5", "{%}" };
            btn6.Tag = new string[2] { "6", "{^}" };
            btn7.Tag = new string[2] { "7", "{&}" };
            btn8.Tag = new string[2] { "8", "{*}" };
            btn9.Tag = new string[2] { "9", "{(}" };
            btnMinus.Tag = new string[2] { "{-}", "{_}" };
            btnEqual.Tag = new string[2] { "{=}", "{+}" };
            btnApostrophe.Tag = new string[2] { "{`}", "{~}" };
            btnBackspace.Tag = "{BS}";

            //line 2
            btnQ.Tag = new string[2] { "q", "Q" };
            btnW.Tag = new string[2] { "w", "W" };
            btnE.Tag = new string[2] { "e", "E" };
            btnR.Tag = new string[2] { "r", "R" };
            btnT.Tag = new string[2] { "t", "T" };
            btnY.Tag = new string[2] { "y", "Y" };
            btnU.Tag = new string[2] { "u", "U" };
            btnI.Tag = new string[2] { "i", "I" };
            btnO.Tag = new string[2] { "o", "O" };
            btnP.Tag = new string[2] { "p", "P" };
            btnLBrackets.Tag = new string[2] { "{[}", "{{}" };
            btnRBrackets.Tag = new string[2] { "{]}", "{}}" };
            btnBacklash.Tag = new string[2] { "\\", "{|}" };

            //line 3
            btnA.Tag = new string[2] { "a", "A" };
            btnS.Tag = new string[2] { "s", "S" };
            btnD.Tag = new string[2] { "d", "D" };
            btnF.Tag = new string[2] { "f", "F" };
            btnG.Tag = new string[2] { "g", "G" };
            btnH.Tag = new string[2] { "h", "H" };
            btnJ.Tag = new string[2] { "j", "J" };
            btnK.Tag = new string[2] { "k", "K" };
            btnL.Tag = new string[2] { "l", "L" };
            btnSemicolon.Tag = new string[2] { "{;}", "{:}" };
            btnQuotation.Tag = new string[2] { "{'}", "\"" };

            //line 4
            btnZ.Tag = new string[2] { "z", "Z" };
            btnX.Tag = new string[2] { "x", "X" };
            btnC.Tag = new string[2] { "c", "C" };
            btnV.Tag = new string[2] { "v", "V" };
            btnB.Tag = new string[2] { "b", "B" };
            btnN.Tag = new string[2] { "n", "N" };
            btnM.Tag = new string[2] { "m", "M" };
            btnComma.Tag = new string[2] { "{,}", "{<}" };
            btnPeriod.Tag = new string[2] { "{.}", "{>}" };
            btnSlash.Tag = new string[2] { "{/}", "{?}" };
            btnSpace.Tag = " ";

            btnsSymbol = new Button[] { btnMinus, btnEqual, btnApostrophe, btnLBrackets, btnRBrackets, btnBacklash, 
                btnSemicolon, btnQuotation, btnComma, btnPeriod, btnSlash, btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };

            btnsLetter = new Button[] { btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL,
                btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ };
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            bShift = !bShift;
            if (bShift)
            {
                ControlSymbolBtnText(1);
                ButtonMouseDown(btnShift, null);
            }
            else
            {
                ControlSymbolBtnText(0);
                ButtonMouseUp(btnShift, null);
            }
        }

        private void ControlLetterBtnText(int index)
        {
            foreach (Button btn in btnsLetter)
            {
                string[] btnChar = (string[])btn.Tag;
                string strChar = btnChar[index];
                btn.Text = strChar;
            }
        }

        private void ControlSymbolBtnText(int index)
        {
            foreach (Button btn in btnsSymbol)
            {
                string[] btnChar = (string[])btn.Tag;
                string strChar = btnChar[index];
                strChar = strChar.Replace("{", "");
                strChar = strChar.Replace("}", "");
                if (index == 1)
                {
                    switch (btn.Name)
                    {
                        case "btn7":
                            strChar = "&&";
                            break;
                        case "btnLBrackets":
                            strChar = "{";
                            break;
                        case "btnRBrackets":
                            strChar = "}";
                            break;
                    }
                }
                btn.Text = strChar;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string[] args)
        {
            InitializeComponent();
            int x, y;
            try
            {
                x = int.Parse(args[0]);
                y = int.Parse(args[1]);
            }
            catch (Exception)
            {
                x = Location.X;
                y = Location.Y;
            }
            Location = new Point(x, y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitButton();

            if (CapsLockStatus)
            {
                bCapsStart = true;
                btnCaps_Click(null, null);
            }
               
        }

        private void ButtonMouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Color tmp = btn.ForeColor;
            btn.ForeColor = btn.BackColor;
            btn.BackColor = tmp;
        }

        private void ButtonMouseUp(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Color tmp = btn.ForeColor;
            btn.ForeColor = btn.BackColor;
            btn.BackColor = tmp;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string btnChar = (string)btn.Tag;
                SendKeys.Send(btnChar);
            }
        }


        private void ButtonLetterClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string[] btnChar = (string[])btn.Tag;
                if(bCapsDown)
                {
                    if (bCapsStart)
                        SendKeys.Send(btnChar[0]);
                    else
                        SendKeys.Send(btnChar[1]);
                }
                else 
                {
                    if(bCapsStart)
                        SendKeys.Send(btnChar[1]);
                    else
                        SendKeys.Send(btnChar[0]);
                }
                
            }
        }

        private void ButtonSymbolClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string[] btnChar = (string[])btn.Tag;
                if(bShift)
                {
                    SendKeys.Send(btnChar[1]);
                    btnShift_Click(null, null);
                }
                else
                {
                    SendKeys.Send(btnChar[0]);
                }
            }
        }

        private void btnCaps_Click(object sender, EventArgs e)
        {
            bCapsDown = !bCapsDown;
            if(bCapsDown)
            {
                ControlLetterBtnText(1);
                ButtonMouseDown(btnCaps, null);
            }
            else
            {
                ControlLetterBtnText(0);
                ButtonMouseUp(btnCaps, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMove_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                iStartX = e.X;
                iStartY = e.Y;
                ButtonMouseDown(sender, e);
            }
        }

        private void btnMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - iStartX;
                this.Top += e.Y - iStartY;
            }
        }

    }

}
