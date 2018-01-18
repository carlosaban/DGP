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
            this.numMontoVenta = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numMonto = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNroDocumento = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.cmbIdVenta = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numMontoAplicar = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMontoVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMontoAplicar)).BeginInit();
            this.SuspendLayout();
            // 
            // numMontoVenta
            // 
            this.numMontoVenta.DecimalPlaces = 2;
            this.numMontoVenta.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numMontoVenta.Location = new System.Drawing.Point(144, 157);
            this.numMontoVenta.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numMontoVenta.Name = "numMontoVenta";
            this.numMontoVenta.ReadOnly = true;
            this.numMontoVenta.Size = new System.Drawing.Size(120, 20);
            this.numMontoVenta.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Monto de la venta";
            // 
            // numMonto
            // 
            this.numMonto.DecimalPlaces = 2;
            this.numMonto.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numMonto.Location = new System.Drawing.Point(144, 198);
            this.numMonto.Margin = new System.Windows.Forms.Padding(4);
            this.numMonto.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numMonto.Name = "numMonto";
            this.numMonto.Size = new System.Drawing.Size(121, 20);
            this.numMonto.TabIndex = 20;
            this.numMonto.ThousandsSeparator = true;
            this.numMonto.ValueChanged += new System.EventHandler(this.numMonto_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Monto";
            // 
            // txtNroDocumento
            // 
            this.txtNroDocumento.Location = new System.Drawing.Point(144, 238);
            this.txtNroDocumento.Name = "txtNroDocumento";
            this.txtNroDocumento.Size = new System.Drawing.Size(121, 20);
            this.txtNroDocumento.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Nro Documento";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Cliente";
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Location = new System.Drawing.Point(143, 74);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(121, 20);
            this.txtCliente.TabIndex = 30;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(246, 315);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(75, 23);
            this.btnAplicar.TabIndex = 32;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            // 
            // cmbIdVenta
            // 
            this.cmbIdVenta.FormattingEnabled = true;
            this.cmbIdVenta.Location = new System.Drawing.Point(144, 115);
            this.cmbIdVenta.Name = "cmbIdVenta";
            this.cmbIdVenta.Size = new System.Drawing.Size(121, 21);
            this.cmbIdVenta.TabIndex = 34;
            this.cmbIdVenta.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbIdVenta_MouseClick);
            this.cmbIdVenta.SelectedIndexChanged += new System.EventHandler(this.cmbIdVenta_SelectedIndexChanged);
            this.cmbIdVenta.DisplayMemberChanged += new System.EventHandler(this.cmbIdVenta_DisplayMemberChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "IdVenta";
            // 
            // numMontoAplicar
            // 
            this.numMontoAplicar.DecimalPlaces = 2;
            this.numMontoAplicar.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numMontoAplicar.Location = new System.Drawing.Point(145, 39);
            this.numMontoAplicar.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numMontoAplicar.Name = "numMontoAplicar";
            this.numMontoAplicar.ReadOnly = true;
            this.numMontoAplicar.Size = new System.Drawing.Size(120, 20);
            this.numMontoAplicar.TabIndex = 36;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Monto a aplicar";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(297, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Observacion";
            // 
            // txtObservacion
            // 
            this.txtObservacion.Location = new System.Drawing.Point(300, 74);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(272, 184);
            this.txtObservacion.TabIndex = 37;
            // 
            // frmDocumentoPagoDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 360);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtObservacion);
            this.Controls.Add(this.numMontoAplicar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbIdVenta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNroDocumento);
            this.Controls.Add(this.numMontoVenta);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numMonto);
            this.Controls.Add(this.label3);
            this.Name = "frmDocumentoPagoDetalle";
            this.Text = "frmDocumentoPagoDetalle";
            this.Load += new System.EventHandler(this.frmDocumentoPagoDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMontoVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMontoAplicar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numMontoVenta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numMonto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNroDocumento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.ComboBox cmbIdVenta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numMontoAplicar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtObservacion;
    }
}