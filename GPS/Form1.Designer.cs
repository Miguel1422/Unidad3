namespace GPS
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viajeroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programasToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(476, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programasToolStripMenuItem
            // 
            this.programasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arbolToolStripMenuItem,
            this.gPSToolStripMenuItem,
            this.viajeroToolStripMenuItem});
            this.programasToolStripMenuItem.Name = "programasToolStripMenuItem";
            this.programasToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.programasToolStripMenuItem.Text = "Programas";
            // 
            // arbolToolStripMenuItem
            // 
            this.arbolToolStripMenuItem.Name = "arbolToolStripMenuItem";
            this.arbolToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.arbolToolStripMenuItem.Text = "Arbol";
            this.arbolToolStripMenuItem.Click += new System.EventHandler(this.arbolToolStripMenuItem_Click);
            // 
            // gPSToolStripMenuItem
            // 
            this.gPSToolStripMenuItem.Name = "gPSToolStripMenuItem";
            this.gPSToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gPSToolStripMenuItem.Text = "GPS";
            this.gPSToolStripMenuItem.Click += new System.EventHandler(this.gPSToolStripMenuItem_Click);
            // 
            // viajeroToolStripMenuItem
            // 
            this.viajeroToolStripMenuItem.Name = "viajeroToolStripMenuItem";
            this.viajeroToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viajeroToolStripMenuItem.Text = "Viajero";
            this.viajeroToolStripMenuItem.Click += new System.EventHandler(this.viajeroToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 374);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem programasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gPSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viajeroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
    }
}

