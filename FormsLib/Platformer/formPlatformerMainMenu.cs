﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsLib.Platformer
{
    public partial class formPlatformerMainMenu : Form
    {
        public formPlatformerMainMenu()
        {
            InitializeComponent();
            DoubleBuffered = true;
            new Game(this, new Label());
        }
    }
}
