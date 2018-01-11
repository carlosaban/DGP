namespace DGP.Presentation.Ventas
{
    partial class frmDocumentoPagoDetalle
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
            this.nudMontoAplicadoDocumento = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudMontoDocumento = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoAplicadoDocumento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoDocumento)).BeginInit();
            this.SuspendLayout();
            // 
            // nudMontoAplicadoDocumento
            // 
            this.nudMontoAplicadoDocumento.DecimalPlaces = 2;
            this.nudMontoAplicadoDocumento.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudMontoAplicadoDocumento.Location = new System.Drawing.Point(145, 29);
            this.nudMontoAplicadoDocumento.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudMontoAplicadoDocumento.Name = "nudMontoAplicadoDocumento";
            this.nudMontoAplicadoDocumento.ReadOnly = true;
            this.nudMontoAplicadoDocumento.Size = new System.Drawing.Size(120, 20);
            this.nudMontoAplicadoDocumento.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Monto a aplicar";
            // 
            // nudMontoDocumento
            // 
            this.nudMontoDocumento.DecimalPlaces = 2;
            this.nudMontoDocumento.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudMontoDocumento.Location = new System.Drawing.Point(145, 156);
            this.nudMontoDocumento.Margin = new System.Windows.Forms.Padding(4);
            this.nudMontoDocumento.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMontoDocumento.Name = "nudMontoDocumento";
            this.nudMontoDocumento.Size = new System.Drawing.Size(121, 20);
            this.nudMontoDocumento.TabIndex = 20;
            this.nudMontoDocumento.ThousandsSeparator = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Monto";
            // 
            // cbProducto
            // 
            this.cbProducto.Enabled = false;
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(145, 112);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(121, 21);
            this.cbProducto.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "IdVenta";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(145, 196);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Nro Documento";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(145, 247);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Usuario";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Cliente";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(145, 67);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(121, 20);
            this.textBox2.TabIndex = 30;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "Aplicar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmDocumentoPagoDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 362);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.nudMontoAplicadoDocumento);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudMontoDocumento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbProducto);
            this.Controls.Add(this.label2);
            this.Name = "frmDocumentoPagoDetalle";
            this.Text = "frmDocumentoPagoDetalle";
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoAplicadoDocumento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoDocumento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudMontoAplicadoDocumento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudMontoDocumento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbProducto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
    }
}