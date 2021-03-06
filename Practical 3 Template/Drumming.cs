﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Practical_3_Template
{
    class Drumming
    {
        // Declare the global variables
        int selectedKit = 0;
        Image[] drumKit = new Image[7];
        SoundPlayer[] drumSound = new SoundPlayer[7] { new SoundPlayer(), new SoundPlayer(), new SoundPlayer(), new SoundPlayer(), new SoundPlayer(), new SoundPlayer(), new SoundPlayer() };

        public Drumming()
        {
            // Fill the arrays with references to the sound and image locations
            drumSound[1].SoundLocation = "Sounds/Crash.wav";
            drumSound[2].SoundLocation = "Sounds/LowTom.wav";
            drumSound[3].SoundLocation = "Sounds/Snare.wav";
            drumSound[4].SoundLocation = "Sounds/MidTom.wav";
            drumSound[5].SoundLocation = "Sounds/Bass.wav";
            drumSound[6].SoundLocation = "Sounds/OpenHiHat.wav";
            

            drumKit[0] = Image.FromFile("Images/drumkit0.jpg", true);
            drumKit[1] = Image.FromFile("Images/drumkit1.jpg", true);
            drumKit[2] = Image.FromFile("Images/drumkit2.jpg", true);
            drumKit[3] = Image.FromFile("Images/drumkit3.jpg", true);
            drumKit[4] = Image.FromFile("Images/drumkit4.jpg", true);
            drumKit[5] = Image.FromFile("Images/drumkit5.jpg", true);
            drumKit[6] = Image.FromFile("Images/drumkit6.jpg", true);
        }

        public void Update(float dt)
        {
            // Check if a hitting motion is being performed
            if (Math.Round(Globals.WiiMote.WiimoteState.AccelState.Values.Z) > 3)
            {
                // Check which button is being pressed
                if (Globals.WiiMote.WiimoteState.ButtonState.Left)
                    selectedKit = 1;
                if (Globals.WiiMote.WiimoteState.ButtonState.A)
                    selectedKit = 2;
                else if(Globals.WiiMote.WiimoteState.ButtonState.Up)
                    selectedKit = 3;
                else if (Globals.WiiMote.WiimoteState.ButtonState.Down)
                    selectedKit = 4;
                else if (Globals.WiiMote.WiimoteState.ButtonState.B)
                    selectedKit = 5;
                else if (Globals.WiiMote.WiimoteState.ButtonState.Right)
                    selectedKit = 6;

                // Play the corresponding drumkit
                if(selectedKit != 0)
                    drumSound[selectedKit].Play();
            }
        }

        public void Draw(float dt)
        {
            // Create the graphics object so we can draw
            Graphics g = Globals.Graphics;
            
            // Clear the screen
            g.Clear(Color.Black);
            // Show the drumkit image corresponding to the sound
            g.DrawImage(drumKit[selectedKit], 0, 0);
            // Draw information
            g.DrawString("To drum: Press A, B, Left, Right, Top or Bottom and perform a drumming motion", new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular), Brushes.White, new Point(20, 50));

            // Resets the image
            if (selectedKit != 0)
                selectedKit = 0;
        }
    }
}