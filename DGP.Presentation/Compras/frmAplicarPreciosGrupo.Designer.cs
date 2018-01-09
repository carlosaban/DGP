namespace DGP.Presentation.Compras
{
    partial class frmAplicarPreciosGrupo
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
            this.btnAplicar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nubMargenMinimo = new System.Windows.Forms.NumericUpDown();
            this.nudPrecioBase = new System.Windows.Forms.NumericUpDown();
            this.cbFormaAplicar = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nubMargenMinimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioBase)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(172, 222);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(75, 23);
            this.btnAplicar.TabIndex = 17;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Margen Por Defecto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Precio Base";
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(33, 53);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(50, 13);
            this.lblProducto.TabIndex = 14;
            this.lblProducto.Text = "Producto";
            // 
            // cbProducto
            // 
            this.cbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(171, 45);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(121, 21);
            this.cbProducto.TabIndex = 13;
            this.cbProducto.SelectedIndexChanged += new System.EventHandler(this.cbProducto_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Forma Aplicar";
            // 
            // nubMargenMinimo
            // 
            this.nubMargenMinimo.DecimalPlaces = 2;
            this.nubMargenMinimo.Location = new System.Drawing.Point(172, 174);
            this.nubMargenMinimo.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nubMargenMinimo.Name = "nubMargenMinimo";
            this.nubMargenMinimo.Size = new System.Drawing.Size(120, 20);
            this.nubMargenMinimo.TabIndex = 11;
            this.nubMargenMinimo.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // nudPrecioBase
            // 
            this.nudPrecioBase.DecimalPlaces = 2;
            this.nudPrecioBase.Location = new System.Drawing.Point(172, 127);
            this.nudPrecioBase.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPrecioBase.Name = "nudPrecioBase";
            this.nudPrecioBase.Size = new System.Drawing.Size(120, 20);
            this.nudPrecioBase.TabIndex = 10;
            // 
            // cbFormaAplicar
            // 
            this.cbFormaAplicar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormaAplicar.FormattingEnabled = true;
            this.cbFormaAplicar.Location = new System.Drawing.Point(171, 89);
            this.cbFormaAplicar.Name = "cbFormaAplicar";
            this.cbFormaAplicar.Size = new System.Drawing.Size(121, 21);
            this.cbFormaAplicar.TabIndex = 9;
            // 
            // frmAplicarPreciosGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 277);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.cbProducto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nubMargenMinimo);
            this.Controls.Add(this.nudPrecioBase);
            this.Controls.Add(this.cbFormaAplicar);
            this.Name = "frmAplicarPreciosGrupo";
            this.Text = "frmAplicarPreciosGrupo";
            this.Load += new System.EventHandler(this.frmAplicarPreciosGrupo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nubMargenMinimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioBase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cbProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nubMargenMinimo;
        private System.Windows.Forms.NumericUpDown nudPrecioBase;
        private System.Windows.Forms.ComboBox cbFormaAplicar;
    }
}