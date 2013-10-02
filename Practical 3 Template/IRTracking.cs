﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Practical_3_Template
{
    class IRTracking
    {
        const int pointerSize = 10;
        const int boardSize = 50;
        const int powerbarWidth = 50;
        const int powerbarHeight = 150;
        bool holdingButtonB = false;
        float x, y;

        public IRTracking()
        {
        }

        public void Update(float dt)
        {
            if (Globals.WiiMote.WiimoteState.ButtonState.B)
            {
                if (!holdingButtonB)
                {
                    x = 1 - Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Position.X;
                    y = Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Position.Y;
                }
                holdingButtonB = true;
            }
            else
                holdingButtonB = false;

        }

        public void Draw(float dt)
        {
            Graphics g = Globals.Graphics;

            g.Clear(Color.Black);

            paintBoard(g);

            g.DrawString("X0: " + Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Position.X.ToString(), new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular), Brushes.White, new Point(50, 50));
            g.DrawString("X1: " + Globals.WiiMote.WiimoteState.IRState.IRSensors[1].Position.X.ToString(), new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular), Brushes.White, new Point(50, 65));


            // Draw the pointer
            if (holdingButtonB)
            {
                g.FillEllipse(Brushes.Yellow, x * Globals.Form.ClientSize.Width - (pointerSize / 2), y * Globals.Form.ClientSize.Height - (pointerSize / 2), pointerSize, pointerSize);
                g.FillRectangle(Brushes.Green, Globals.Form.ClientSize.Width - powerbarWidth, Globals.Form.ClientSize.Height - calculateDepth() * 100, powerbarWidth, calculateDepth() * 100);
            
            }
            else
                g.FillEllipse(Brushes.Blue, (1 - Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Position.X) * Globals.Form.ClientSize.Width - (pointerSize / 2), Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Position.Y * Globals.Form.ClientSize.Height - (pointerSize / 2), pointerSize, pointerSize);
        }

        // Draw the gameboard
        private static void paintBoard(Graphics g)
        {
            for(int x = 10; x >= 0; x--)
            {
                g.FillEllipse(Convert.ToBoolean(x % 2) ? Brushes.White : Brushes.Red, 
                    (Globals.Form.ClientSize.Width / 2) - x * (boardSize / 2), 
                    (Globals.Form.ClientSize.Height / 2) - x * (boardSize / 2),
                    x * boardSize, x * boardSize);
            }
        }

        // Calculate the distance to the sensorbar
        private static float calculateDepth()
        {
            float xSensor0, xSensor1, ySensor0, ySensor1;

            xSensor0 = Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Position.X;
            ySensor0 = Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Position.Y;
            xSensor1 = Globals.WiiMote.WiimoteState.IRState.IRSensors[1].Position.X;
            ySensor1 = Globals.WiiMote.WiimoteState.IRState.IRSensors[1].Position.Y;

            return xSensor1;
        }
    }
}
