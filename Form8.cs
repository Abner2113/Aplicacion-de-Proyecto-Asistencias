﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicacion_de_Proyecto_Asistencias
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }
    }
}
