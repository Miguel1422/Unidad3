namespace GPS.Arbol
{
    partial class ArbolC
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
            this.components = new System.ComponentModel.Container();
            this.doubleBufferedPanel1 = new Gps.Arbol.DoubleBufferedPanel();
            this.btnB = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.btnI = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.tbB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbIH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.doubleBufferedPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // doubleBufferedPanel1
            // 
            this.doubleBufferedPanel1.Controls.Add(this.btnB);
            this.doubleBufferedPanel1.Controls.Add(this.btnE);
            this.doubleBufferedPanel1.Controls.Add(this.btnI);
            this.doubleBufferedPanel1.Controls.Add(this.btnC);
            this.doubleBufferedPanel1.Controls.Add(this.tbB);
            this.doubleBufferedPanel1.Controls.Add(this.label5);
            this.doubleBufferedPanel1.Controls.Add(this.tbE);
            this.doubleBufferedPanel1.Controls.Add(this.label4);
            this.doubleBufferedPanel1.Controls.Add(this.tbIH);
            this.doubleBufferedPanel1.Controls.Add(this.label3);
            this.doubleBufferedPanel1.Controls.Add(this.tbIP);
            this.doubleBufferedPanel1.Controls.Add(this.label2);
            this.doubleBufferedPanel1.Controls.Add(this.tbC);
            this.doubleBufferedPanel1.Controls.Add(this.label1);
            this.doubleBufferedPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferedPanel1.DoubleBuffered = true;
            this.doubleBufferedPanel1.Location = new System.Drawing.Point(0, 0);
            this.doubleBufferedPanel1.Name = "doubleBufferedPanel1";
            this.doubleBufferedPanel1.Size = new System.Drawing.Size(649, 398);
            this.doubleBufferedPanel1.TabIndex = 0;
            this.doubleBufferedPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.doubleBufferedPanel1_Paint);
            // 
            // btnB
            // 
            this.btnB.Location = new System.Drawing.Point(183, 197);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(75, 23);
            this.btnB.TabIndex = 27;
            this.btnB.Text = "Buscar";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Click += new System.EventHandler(this.btnB_Click);
            // 
            // btnE
            // 
            this.btnE.Location = new System.Drawing.Point(183, 157);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(75, 23);
            this.btnE.TabIndex = 26;
            this.btnE.Text = "Eliminar";
            this.btnE.UseVisualStyleBackColor = true;
            this.btnE.Click += new System.EventHandler(this.btnE_Click);
            // 
            // btnI
            // 
            this.btnI.Location = new System.Drawing.Point(183, 109);
            this.btnI.Name = "btnI";
            this.btnI.Size = new System.Drawing.Size(75, 23);
            this.btnI.TabIndex = 25;
            this.btnI.Text = "Insertar";
            this.btnI.UseVisualStyleBackColor = true;
            this.btnI.Click += new System.EventHandler(this.btnI_Click);
            // 
            // btnC
            // 
            this.btnC.Location = new System.Drawing.Point(183, 41);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(75, 23);
            this.btnC.TabIndex = 24;
            this.btnC.Text = "Crear";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.btnC_Click);
            // 
            // tbB
            // 
            this.tbB.Location = new System.Drawing.Point(67, 199);
            this.tbB.Name = "tbB";
            this.tbB.Size = new System.Drawing.Size(100, 20);
            this.tbB.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Nodo";
            // 
            // tbE
            // 
            this.tbE.Location = new System.Drawing.Point(67, 159);
            this.tbE.Name = "tbE";
            this.tbE.Size = new System.Drawing.Size(100, 20);
            this.tbE.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Nodo";
            // 
            // tbIH
            // 
            this.tbIH.Location = new System.Drawing.Point(67, 112);
            this.tbIH.Name = "tbIH";
            this.tbIH.Size = new System.Drawing.Size(100, 20);
            this.tbIH.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Hijo";
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(67, 86);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(100, 20);
            this.tbIP.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Padre";
            // 
            // tbC
            // 
            this.tbC.Location = new System.Drawing.Point(67, 43);
            this.tbC.Name = "tbC";
            this.tbC.Size = new System.Drawing.Size(100, 20);
            this.tbC.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Nodo";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ArbolC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 398);
            this.Controls.Add(this.doubleBufferedPanel1);
            this.Name = "ArbolC";
            this.Text = "Arbol";
            this.doubleBufferedPanel1.ResumeLayout(false);
            this.doubleBufferedPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.Button btnE;
        private System.Windows.Forms.Button btnI;
        private System.Windows.Forms.Button btnC;
        private System.Windows.Forms.TextBox tbB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbIH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private Gps.Arbol.DoubleBufferedPanel doubleBufferedPanel1;
    }
}