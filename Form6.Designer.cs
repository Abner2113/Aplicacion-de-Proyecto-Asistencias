namespace Aplicacion_de_Proyecto_Asistencias
{
    partial class Form6
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRegistrarEmpleado = new System.Windows.Forms.Button();
            this.btnAsistencias = new System.Windows.Forms.Button();
            this.btnInsidencias = new System.Windows.Forms.Button();
            this.btnHorarios = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.picEmpleado = new System.Windows.Forms.PictureBox();
            this.picAsistencias = new System.Windows.Forms.PictureBox();
            this.picIncidencias = new System.Windows.Forms.PictureBox();
            this.picHorarios = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpleado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAsistencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIncidencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHorarios)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.pictureBox1.Location = new System.Drawing.Point(115, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(104, 99);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.pictureBox2.Location = new System.Drawing.Point(230, 32);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(748, 100);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(445, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 43);
            this.label1.TabIndex = 7;
            this.label1.Text = "¡¡BIENVENIDO!!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(500, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "Administrador";
            // 
            // btnRegistrarEmpleado
            // 
            this.btnRegistrarEmpleado.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarEmpleado.Location = new System.Drawing.Point(167, 288);
            this.btnRegistrarEmpleado.Name = "btnRegistrarEmpleado";
            this.btnRegistrarEmpleado.Size = new System.Drawing.Size(210, 98);
            this.btnRegistrarEmpleado.TabIndex = 9;
            this.btnRegistrarEmpleado.Text = " Registrar\r\n Empleado";
            this.btnRegistrarEmpleado.UseVisualStyleBackColor = true;
            this.btnRegistrarEmpleado.Click += new System.EventHandler(this.btnRegistrarEmpleado_Click);
            // 
            // btnAsistencias
            // 
            this.btnAsistencias.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsistencias.Location = new System.Drawing.Point(465, 288);
            this.btnAsistencias.Name = "btnAsistencias";
            this.btnAsistencias.Size = new System.Drawing.Size(210, 98);
            this.btnAsistencias.TabIndex = 10;
            this.btnAsistencias.Text = "  Historial de\r\n  Asistencias";
            this.btnAsistencias.UseVisualStyleBackColor = true;
            this.btnAsistencias.Click += new System.EventHandler(this.btnAsistencias_Click);
            // 
            // btnInsidencias
            // 
            this.btnInsidencias.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsidencias.Location = new System.Drawing.Point(763, 288);
            this.btnInsidencias.Name = "btnInsidencias";
            this.btnInsidencias.Size = new System.Drawing.Size(210, 98);
            this.btnInsidencias.TabIndex = 11;
            this.btnInsidencias.Text = "   Historial de\r\n   Incidencias";
            this.btnInsidencias.UseVisualStyleBackColor = true;
            this.btnInsidencias.Click += new System.EventHandler(this.btnInsidencias_Click);
            // 
            // btnHorarios
            // 
            this.btnHorarios.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHorarios.Location = new System.Drawing.Point(465, 430);
            this.btnHorarios.Name = "btnHorarios";
            this.btnHorarios.Size = new System.Drawing.Size(210, 98);
            this.btnHorarios.TabIndex = 12;
            this.btnHorarios.Text = " Agregar \r\n Horarios";
            this.btnHorarios.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(25, 500);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(80, 27);
            this.btnSalir.TabIndex = 15;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // picEmpleado
            // 
            this.picEmpleado.BackColor = System.Drawing.SystemColors.Menu;
            this.picEmpleado.Location = new System.Drawing.Point(185, 325);
            this.picEmpleado.Name = "picEmpleado";
            this.picEmpleado.Size = new System.Drawing.Size(24, 24);
            this.picEmpleado.TabIndex = 16;
            this.picEmpleado.TabStop = false;
            // 
            // picAsistencias
            // 
            this.picAsistencias.BackColor = System.Drawing.SystemColors.Menu;
            this.picAsistencias.Location = new System.Drawing.Point(489, 325);
            this.picAsistencias.Name = "picAsistencias";
            this.picAsistencias.Size = new System.Drawing.Size(24, 24);
            this.picAsistencias.TabIndex = 17;
            this.picAsistencias.TabStop = false;
            // 
            // picIncidencias
            // 
            this.picIncidencias.BackColor = System.Drawing.SystemColors.Menu;
            this.picIncidencias.Location = new System.Drawing.Point(784, 325);
            this.picIncidencias.Name = "picIncidencias";
            this.picIncidencias.Size = new System.Drawing.Size(24, 24);
            this.picIncidencias.TabIndex = 18;
            this.picIncidencias.TabStop = false;
            // 
            // picHorarios
            // 
            this.picHorarios.BackColor = System.Drawing.SystemColors.Menu;
            this.picHorarios.Location = new System.Drawing.Point(489, 470);
            this.picHorarios.Name = "picHorarios";
            this.picHorarios.Size = new System.Drawing.Size(24, 24);
            this.picHorarios.TabIndex = 19;
            this.picHorarios.TabStop = false;
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.picHorarios);
            this.Controls.Add(this.picIncidencias);
            this.Controls.Add(this.picAsistencias);
            this.Controls.Add(this.picEmpleado);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnHorarios);
            this.Controls.Add(this.btnInsidencias);
            this.Controls.Add(this.btnAsistencias);
            this.Controls.Add(this.btnRegistrarEmpleado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form6";
            this.Text = "Form6";
            this.Load += new System.EventHandler(this.Form6_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpleado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAsistencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIncidencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHorarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRegistrarEmpleado;
        private System.Windows.Forms.Button btnAsistencias;
        private System.Windows.Forms.Button btnInsidencias;
        private System.Windows.Forms.Button btnHorarios;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.PictureBox picEmpleado;
        private System.Windows.Forms.PictureBox picAsistencias;
        private System.Windows.Forms.PictureBox picIncidencias;
        private System.Windows.Forms.PictureBox picHorarios;
    }
}