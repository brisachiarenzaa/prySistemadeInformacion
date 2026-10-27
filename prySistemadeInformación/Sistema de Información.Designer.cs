namespace prySistemadeInformación
{
    partial class FarmaciasNoya
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.cmbSucursales = new System.Windows.Forms.ComboBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDroga = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblPresentación = new System.Windows.Forms.Label();
            this.lblMiligramos = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTítuloStock = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblFechaVencimiento = new System.Windows.Forms.Label();
            this.lblEquivalencia = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(251, 22);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(192, 24);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "FARMACIAS NOYA";
            // 
            // cmbSucursales
            // 
            this.cmbSucursales.FormattingEnabled = true;
            this.cmbSucursales.Items.AddRange(new object[] {
            "Farmacia Noya 1 - Cosquín",
            "Farmacia Noya 2 - Santa María de Punilla",
            "Farmacia Noya 3 - Bialet Masse"});
            this.cmbSucursales.Location = new System.Drawing.Point(327, 60);
            this.cmbSucursales.Name = "cmbSucursales";
            this.cmbSucursales.Size = new System.Drawing.Size(97, 21);
            this.cmbSucursales.TabIndex = 1;
            this.cmbSucursales.SelectedIndexChanged += new System.EventHandler(this.cmbSucursales_SelectedIndexChanged);
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Location = new System.Drawing.Point(259, 63);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(51, 13);
            this.lblSucursal.TabIndex = 2;
            this.lblSucursal.Text = "Sucursal:";
            this.lblSucursal.Click += new System.EventHandler(this.lblSucursal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Marca:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblDroga
            // 
            this.lblDroga.AutoSize = true;
            this.lblDroga.Location = new System.Drawing.Point(40, 214);
            this.lblDroga.Name = "lblDroga";
            this.lblDroga.Size = new System.Drawing.Size(39, 13);
            this.lblDroga.TabIndex = 4;
            this.lblDroga.Text = "Droga:";
            this.lblDroga.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Medicamento:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(40, 146);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 7;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.Click += new System.EventHandler(this.label2_Click_2);
            // 
            // lblPresentación
            // 
            this.lblPresentación.AutoSize = true;
            this.lblPresentación.Location = new System.Drawing.Point(40, 248);
            this.lblPresentación.Name = "lblPresentación";
            this.lblPresentación.Size = new System.Drawing.Size(72, 13);
            this.lblPresentación.TabIndex = 8;
            this.lblPresentación.Text = "Presentación:";
            this.lblPresentación.Click += new System.EventHandler(this.label2_Click_3);
            // 
            // lblMiligramos
            // 
            this.lblMiligramos.AutoSize = true;
            this.lblMiligramos.Location = new System.Drawing.Point(216, 248);
            this.lblMiligramos.Name = "lblMiligramos";
            this.lblMiligramos.Size = new System.Drawing.Size(59, 13);
            this.lblMiligramos.TabIndex = 9;
            this.lblMiligramos.Text = "Miligramos:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(381, 370);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 17;
            // 
            // lblTítuloStock
            // 
            this.lblTítuloStock.AutoSize = true;
            this.lblTítuloStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTítuloStock.Location = new System.Drawing.Point(381, 108);
            this.lblTítuloStock.Name = "lblTítuloStock";
            this.lblTítuloStock.Size = new System.Drawing.Size(57, 18);
            this.lblTítuloStock.TabIndex = 18;
            this.lblTítuloStock.Text = "Stock:";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(381, 146);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(102, 13);
            this.lblCantidad.TabIndex = 19;
            this.lblCantidad.Text = "Cantidad disponible:";
            // 
            // lblFechaVencimiento
            // 
            this.lblFechaVencimiento.AutoSize = true;
            this.lblFechaVencimiento.Location = new System.Drawing.Point(381, 180);
            this.lblFechaVencimiento.Name = "lblFechaVencimiento";
            this.lblFechaVencimiento.Size = new System.Drawing.Size(115, 13);
            this.lblFechaVencimiento.TabIndex = 20;
            this.lblFechaVencimiento.Text = "Fecha de vencimiento:";
            this.lblFechaVencimiento.Click += new System.EventHandler(this.label5_Click);
            // 
            // lblEquivalencia
            // 
            this.lblEquivalencia.AutoSize = true;
            this.lblEquivalencia.Location = new System.Drawing.Point(381, 214);
            this.lblEquivalencia.Name = "lblEquivalencia";
            this.lblEquivalencia.Size = new System.Drawing.Size(125, 13);
            this.lblEquivalencia.TabIndex = 21;
            this.lblEquivalencia.Text = "Equivalencias/sustitutos:";
            // 
            // FarmaciasNoya
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 450);
            this.Controls.Add(this.lblEquivalencia);
            this.Controls.Add(this.lblFechaVencimiento);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.lblTítuloStock);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMiligramos);
            this.Controls.Add(this.lblPresentación);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDroga);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSucursal);
            this.Controls.Add(this.cmbSucursales);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FarmaciasNoya";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FarmaciasNoya_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ComboBox cmbSucursales;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDroga;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblPresentación;
        private System.Windows.Forms.Label lblMiligramos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTítuloStock;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblFechaVencimiento;
        private System.Windows.Forms.Label lblEquivalencia;
    }
}

