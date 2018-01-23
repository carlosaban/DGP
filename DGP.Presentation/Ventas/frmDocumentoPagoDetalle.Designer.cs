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
            this.label5 = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.cbUsuario = new System.Windows.Forms.ComboBox();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.numMontoVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonto)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
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
            this.numMontoVenta.Location = new System.Drawing.Point(381, 21);
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
            this.label6.Location = new System.Drawing.Point(268, 26);
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
            this.numMonto.Location = new System.Drawing.Point(380, 55);
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
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Monto";
            // 
            // txtNroDocumento
            // 
            this.txtNroDocumento.Location = new System.Drawing.Point(643, 20);
            this.txtNroDocumento.Name = "txtNroDocumento";
            this.txtNroDocumento.Size = new System.Drawing.Size(110, 20);
            this.txtNroDocumento.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(533, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Nro Documento";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Cliente";
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Location = new System.Drawing.Point(123, 20);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(121, 20);
            this.txtCliente.TabIndex = 30;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(822, 20);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(112, 23);
            this.btnAplicar.TabIndex = 32;
            this.btnAplicar.Text = "Agregar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // cmbIdVenta
            // 
            this.cmbIdVenta.FormattingEnabled = true;
            this.cmbIdVenta.Location = new System.Drawing.Point(123, 54);
            this.cmbIdVenta.Name = "cmbIdVenta";
            this.cmbIdVenta.Size = new System.Drawing.Size(121, 21);
            this.cmbIdVenta.TabIndex = 34;
            this.cmbIdVenta.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbIdVenta_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "IdVenta";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(546, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Observacion";
            // 
            // txtObservacion
            // 
            this.txtObservacion.Location = new System.Drawing.Point(643, 52);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(110, 23);
            this.txtObservacion.TabIndex = 37;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAplicar);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCliente);
            this.groupBox1.Controls.Add(this.txtObservacion);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbIdVenta);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNroDocumento);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numMonto);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numMontoVenta);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(966, 100);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Amortización";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancelar);
            this.groupBox2.Controls.Add(this.cbUsuario);
            this.groupBox2.Controls.Add(this.btnGrabar);
            this.groupBox2.Location = new System.Drawing.Point(818, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 364);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Usuario";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(17, 114);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(112, 23);
            this.btnCancelar.TabIndex = 43;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // cbUsuario
            // 
            this.cbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsuario.FormattingEnabled = true;
            this.cbUsuario.Location = new System.Drawing.Point(16, 43);
            this.cbUsuario.Name = "cbUsuario";
            this.cbUsuario.Size = new System.Drawing.Size(112, 21);
            this.cbUsuario.TabIndex = 41;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(16, 85);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(112, 23);
            this.btnGrabar.TabIndex = 42;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Monto,
            this.NumeroDocumento,
            this.IdEstado,
            this.IdVenta,
            this.idCliente,
            this.Observacion});
            this.dgvDetalle.Location = new System.Drawing.Point(12, 118);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.Size = new System.Drawing.Size(779, 364);
            this.dgvDetalle.TabIndex = 44;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IdAmortizacionVenta";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id Amortizacion venta";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Monto";
            this.dataGridViewTextBoxColumn2.HeaderText = "Monto";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "NumeroDocumento";
            this.dataGridViewTextBoxColumn3.HeaderText = "NumeroDocumento";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "IdFormaPago";
            this.dataGridViewTextBoxColumn4.HeaderText = "IdFormaPago";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "FechaPago";
            this.dataGridViewTextBoxColumn5.HeaderText = "FechaPago";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "IdTipoAmortizacion";
            this.dataGridViewTextBoxColumn6.HeaderText = "IdTipoAmortizacion";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // Monto
            // 
            this.Monto.DataPropertyName = "Monto";
            this.Monto.HeaderText = "Monto";
            this.Monto.Name = "Monto";
            // 
            // NumeroDocumento
            // 
            this.NumeroDocumento.DataPropertyName = "NumeroDocumento";
            this.NumeroDocumento.HeaderText = "NumeroDocumento";
            this.NumeroDocumento.Name = "NumeroDocumento";
            // 
            // IdEstado
            // 
            this.IdEstado.DataPropertyName = "IdEstado";
            this.IdEstado.HeaderText = "IdEstado";
            this.IdEstado.Name = "IdEstado";
            // 
            // IdVenta
            // 
            this.IdVenta.DataPropertyName = "IdVenta";
            this.IdVenta.HeaderText = "Id Venta";
            this.IdVenta.Name = "IdVenta";
            // 
            // idCliente
            // 
            this.idCliente.DataPropertyName = "idCliente";
            this.idCliente.HeaderText = "Cliente";
            this.idCliente.Name = "idCliente";
            // 
            // Observacion
            // 
            this.Observacion.DataPropertyName = "Observacion";
            this.Observacion.HeaderText = "Observacion";
            this.Observacion.Name = "Observacion";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "IdEstado";
            this.dataGridViewTextBoxColumn7.HeaderText = "IdEstado";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "IdVenta";
            this.dataGridViewTextBoxColumn8.HeaderText = "Id Venta";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "IdCliente";
            this.dataGridViewTextBoxColumn9.HeaderText = "Id Cliente";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "IdDocumento";
            this.dataGridViewTextBoxColumn10.HeaderText = "IdDocumento";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Observacion";
            this.dataGridViewTextBoxColumn11.HeaderText = "Observacion";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // frmDocumentoPagoDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 494);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDocumentoPagoDetalle";
            this.Text = "frmDocumentoPagoDetalle";
            this.Load += new System.EventHandler(this.frmDocumentoPagoDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMontoVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonto)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cbUsuario;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observacion;
        private System.Windows.Forms.DataGridView dgvDetalle;
    }
}