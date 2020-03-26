namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevaPestañaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeTokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualDeAplicaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualTécnicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeProyectoNo1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.consola = new System.Windows.Forms.RichTextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.comboAutomata = new System.Windows.Forms.ComboBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.combotipo = new System.Windows.Forms.ComboBox();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureGrafico = new System.Windows.Forms.PictureBox();
            this.reporteDeConsolaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureGrafico)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.reportesToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(57, 100);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(228, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevaPestañaToolStripMenuItem,
            this.abrirArchivoToolStripMenuItem,
            this.guardarArchivoToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevaPestañaToolStripMenuItem
            // 
            this.nuevaPestañaToolStripMenuItem.Name = "nuevaPestañaToolStripMenuItem";
            this.nuevaPestañaToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.nuevaPestañaToolStripMenuItem.Text = "Nueva Pestaña";
            this.nuevaPestañaToolStripMenuItem.Click += new System.EventHandler(this.nuevaPestañaToolStripMenuItem_Click);
            // 
            // abrirArchivoToolStripMenuItem
            // 
            this.abrirArchivoToolStripMenuItem.Name = "abrirArchivoToolStripMenuItem";
            this.abrirArchivoToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.abrirArchivoToolStripMenuItem.Text = "Abrir Archivo";
            this.abrirArchivoToolStripMenuItem.Click += new System.EventHandler(this.abrirArchivoToolStripMenuItem_Click);
            // 
            // guardarArchivoToolStripMenuItem
            // 
            this.guardarArchivoToolStripMenuItem.Name = "guardarArchivoToolStripMenuItem";
            this.guardarArchivoToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.guardarArchivoToolStripMenuItem.Text = "Guardar";
            this.guardarArchivoToolStripMenuItem.Click += new System.EventHandler(this.guardarArchivoToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.guardarComoToolStripMenuItem.Text = "Guardar Como";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteDeTokensToolStripMenuItem,
            this.reporteDeErrorToolStripMenuItem,
            this.reporteDeConsolaToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // reporteDeTokensToolStripMenuItem
            // 
            this.reporteDeTokensToolStripMenuItem.Name = "reporteDeTokensToolStripMenuItem";
            this.reporteDeTokensToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
            this.reporteDeTokensToolStripMenuItem.Text = "Reporte de Tokens";
            this.reporteDeTokensToolStripMenuItem.Click += new System.EventHandler(this.reporteDeTokensToolStripMenuItem_Click);
            // 
            // reporteDeErrorToolStripMenuItem
            // 
            this.reporteDeErrorToolStripMenuItem.Name = "reporteDeErrorToolStripMenuItem";
            this.reporteDeErrorToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
            this.reporteDeErrorToolStripMenuItem.Text = "Reporte de Error";
            this.reporteDeErrorToolStripMenuItem.Click += new System.EventHandler(this.reporteDeErrorToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualDeAplicaciónToolStripMenuItem,
            this.manualTécnicoToolStripMenuItem,
            this.acercaDeProyectoNo1ToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // manualDeAplicaciónToolStripMenuItem
            // 
            this.manualDeAplicaciónToolStripMenuItem.Name = "manualDeAplicaciónToolStripMenuItem";
            this.manualDeAplicaciónToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.manualDeAplicaciónToolStripMenuItem.Text = "Manual de Aplicación";
            // 
            // manualTécnicoToolStripMenuItem
            // 
            this.manualTécnicoToolStripMenuItem.Name = "manualTécnicoToolStripMenuItem";
            this.manualTécnicoToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.manualTécnicoToolStripMenuItem.Text = "Manual Técnico";
            // 
            // acercaDeProyectoNo1ToolStripMenuItem
            // 
            this.acercaDeProyectoNo1ToolStripMenuItem.Name = "acercaDeProyectoNo1ToolStripMenuItem";
            this.acercaDeProyectoNo1ToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.acercaDeProyectoNo1ToolStripMenuItem.Text = "Acerca de Proyecto No. 1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(716, 317);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Analizar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // consola
            // 
            this.consola.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.consola.Location = new System.Drawing.Point(57, 768);
            this.consola.Name = "consola";
            this.consola.Size = new System.Drawing.Size(631, 132);
            this.consola.TabIndex = 5;
            this.consola.Text = "";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(53, 741);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(79, 24);
            this.materialLabel1.TabIndex = 7;
            this.materialLabel1.Text = "Consola";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richTextBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(630, 571);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pestaña No. 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(3, 3);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(621, 562);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(630, 571);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pestaña No. 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(621, 562);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(57, 138);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(638, 600);
            this.tabControl1.TabIndex = 3;
            // 
            // comboAutomata
            // 
            this.comboAutomata.FormattingEnabled = true;
            this.comboAutomata.Location = new System.Drawing.Point(1344, 106);
            this.comboAutomata.Name = "comboAutomata";
            this.comboAutomata.Size = new System.Drawing.Size(380, 24);
            this.comboAutomata.TabIndex = 8;
            this.comboAutomata.SelectedIndexChanged += new System.EventHandler(this.comboAutomata_SelectedIndexChanged);
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(1018, 104);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(48, 24);
            this.materialLabel2.TabIndex = 10;
            this.materialLabel2.Text = "Tipo";
            // 
            // combotipo
            // 
            this.combotipo.FormattingEnabled = true;
            this.combotipo.Location = new System.Drawing.Point(1090, 106);
            this.combotipo.Name = "combotipo";
            this.combotipo.Size = new System.Drawing.Size(121, 24);
            this.combotipo.TabIndex = 11;
            this.combotipo.SelectedIndexChanged += new System.EventHandler(this.combotipo_SelectedIndexChanged);
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(1235, 106);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(92, 24);
            this.materialLabel3.TabIndex = 1;
            this.materialLabel3.Text = "Automata";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureGrafico);
            this.panel1.Location = new System.Drawing.Point(836, 158);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 700);
            this.panel1.TabIndex = 12;
            // 
            // pictureGrafico
            // 
            this.pictureGrafico.Location = new System.Drawing.Point(3, 3);
            this.pictureGrafico.Name = "pictureGrafico";
            this.pictureGrafico.Size = new System.Drawing.Size(100, 50);
            this.pictureGrafico.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureGrafico.TabIndex = 0;
            this.pictureGrafico.TabStop = false;
            // 
            // reporteDeConsolaToolStripMenuItem
            // 
            this.reporteDeConsolaToolStripMenuItem.Name = "reporteDeConsolaToolStripMenuItem";
            this.reporteDeConsolaToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.reporteDeConsolaToolStripMenuItem.Text = "Reporte de Consola";
            this.reporteDeConsolaToolStripMenuItem.Click += new System.EventHandler(this.reporteDeConsolaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1942, 1000);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.combotipo);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.comboAutomata);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.consola);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureGrafico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevaPestañaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeTokensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeErrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualDeAplicaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualTécnicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeProyectoNo1ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox consola;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ComboBox comboAutomata;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.ComboBox combotipo;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureGrafico;
        private System.Windows.Forms.ToolStripMenuItem reporteDeConsolaToolStripMenuItem;
    }
}

