using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace prySistemadeInformación
{
    public partial class FarmaciasNoya : Form
    {
        private readonly List<Medicamento> medicamentos = new List<Medicamento>();
        private readonly string usuarioAutenticado;
        private readonly string carpetaDatos = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FARMACIA NOYA");
        // Se inicializan al declarar el campo para que el constructor sin parámetros
        // también deje la instancia en un estado válido para el diseñador de WinForms.
        private readonly string archivoMedicamentos = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FARMACIA NOYA", "medicamentos.txt");
        private readonly string archivoHistorial = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FARMACIA NOYA", "historial.txt");
        public bool SolicitarNuevoInicioSesion { get; private set; }

        public FarmaciasNoya()
        {
            InitializeComponent();
        }

        public FarmaciasNoya(string usuarioAutenticado)
        {
            InitializeComponent();
            this.usuarioAutenticado = usuarioAutenticado;
            Directory.CreateDirectory(carpetaDatos);
            MigrarDatosExistentes();
            lblUsuario.Text = usuarioAutenticado + "  •  Empleado autorizado";
            cmbSucursal.SelectedIndexChanged += cmbSucursal_SelectedIndexChanged;
            CargarDatos();
        }

        private void MigrarDatosExistentes()
        {
            string carpetaAnterior = AppDomain.CurrentDomain.BaseDirectory;
            MigrarArchivo(Path.Combine(carpetaAnterior, "medicamentos.txt"), archivoMedicamentos);
            MigrarArchivo(Path.Combine(carpetaAnterior, "historial.txt"), archivoHistorial);
        }

        private void MigrarArchivo(string origen, string destino)
        {
            if (File.Exists(origen) && !File.Exists(destino)) File.Copy(origen, destino);
        }

        private void FarmaciasNoya_Load(object sender, EventArgs e)
        {
            AjustarElementosAlAncho();
            AplicarFiltros();
        }

        private void CargarMedicamentosDePrueba()
        {
            AgregarSiNoExiste(new Medicamento("PAR-500", "Paracetamol", "Analgésico", "Venta libre", "Caja", "500 mg", "Comprimido", 1850.50m, 32, 18, 25));
            AgregarSiNoExiste(new Medicamento("TAF-500", "Tafirol", "Analgésico", "Venta libre", "Caja", "500 mg", "Comprimido", 2100.00m, 20, 15, 18));
            AgregarSiNoExiste(new Medicamento("TYL-500", "Tylenol", "Analgésico", "Venta libre", "Caja", "500 mg", "Comprimido", 2250.00m, 12, 10, 14));
            AgregarSiNoExiste(new Medicamento("IBU-400", "Ibuprofeno", "Analgésico", "Venta libre", "Caja", "400 mg", "Comprimido", 2340.00m, 15, 22, 8));
            AgregarSiNoExiste(new Medicamento("ACT-400", "Actron", "Analgésico", "Venta libre", "Caja", "400 mg", "Cápsula", 2500.00m, 18, 12, 9));
            AgregarSiNoExiste(new Medicamento("IBP-600", "Ibupirac", "Analgésico", "Venta libre", "Caja", "600 mg", "Comprimido", 2890.00m, 9, 8, 11));
            AgregarSiNoExiste(new Medicamento("DIC-050", "Diclofenac", "Analgésico", "Venta libre", "Caja", "50 mg", "Comprimido", 2200.00m, 14, 10, 16));
            AgregarSiNoExiste(new Medicamento("AMO-500", "Amoxicilina", "Antibiótico", "Con receta", "Caja", "500 mg", "Cápsula", 4890.75m, 10, 0, 6));
            AgregarSiNoExiste(new Medicamento("LOS-050", "Losartán", "Antihipertensivo", "Con receta", "Blíster", "50 mg", "Comprimido", 3150.00m, 20, 12, 17));
            AgregarSiNoExiste(new Medicamento("CET-010", "Cetirizina", "Antihistamínico", "Venta libre", "Caja", "10 mg", "Comprimido", 1980.25m, 7, 14, 11));
            AgregarSiNoExiste(new Medicamento("SAL-100", "Salbutamol", "Broncodilatador", "Con receta", "Frasco", "100 mg", "Inhalador", 7250.00m, 4, 9, 0));
            AgregarSiNoExiste(new Medicamento("OME-020", "Omeprazol", "Gastrointestinal", "Venta libre", "Caja", "20 mg", "Cápsula", 2690.00m, 25, 16, 20));
            AgregarSiNoExiste(new Medicamento("MET-500", "Metformina", "Antidiabético", "Con receta", "Caja", "500 mg", "Comprimido", 3440.60m, 18, 5, 13));
            AgregarSiNoExiste(new Medicamento("INS-100", "Insulina Glargina", "Antidiabético", "Con receta", "Caja", "100 UI/ml", "Lapicera", 12850.00m, 6, 4, 8));
        }

        private void AgregarSiNoExiste(Medicamento medicamento)
        {
            if (!medicamentos.Any(m => m.Codigo.Equals(medicamento.Codigo, StringComparison.OrdinalIgnoreCase))) medicamentos.Add(medicamento);
        }

        private void CargarDatos()
        {
            if (File.Exists(archivoMedicamentos) && CargarMedicamentosDesdeArchivo())
            {
                int cantidadAnterior = medicamentos.Count;
                CargarMedicamentosDePrueba();
                if (medicamentos.Count != cantidadAnterior) GuardarMedicamentosEnArchivo();
                return;
            }

            CargarMedicamentosDePrueba();
            GuardarMedicamentosEnArchivo();
        }

        private bool CargarMedicamentosDesdeArchivo()
        {
            try
            {
                foreach (string linea in File.ReadAllLines(archivoMedicamentos))
                {
                    if (string.IsNullOrWhiteSpace(linea) || linea.StartsWith("#")) continue;
                    string[] datos = linea.Split('|');
                    decimal precio;
                    int stock1, stock2, stock3;
                    if ((datos.Length != 11 && datos.Length != 12) || !decimal.TryParse(datos[7], NumberStyles.Number, CultureInfo.InvariantCulture, out precio) ||
                        !int.TryParse(datos[8], out stock1) || !int.TryParse(datos[9], out stock2) || !int.TryParse(datos[10], out stock3)) return false;

                    Medicamento medicamento = new Medicamento(datos[0], datos[1], datos[2], datos[3], datos[4], datos[5], datos[6], precio, stock1, stock2, stock3);
                    if (datos.Length == 12) medicamento.Grupo = datos[11];
                    medicamentos.Add(medicamento);
                }
                return medicamentos.Count > 0;
            }
            catch (IOException)
            {
                medicamentos.Clear();
                return false;
            }
        }

        private void GuardarMedicamentosEnArchivo()
        {
            var lineas = new List<string> { "# Codigo|Nombre|TipoDroga|Venta|Distribucion|Concentracion|Formato|Precio|StockNoya1|StockNoya2|StockNoya3|Grupo" };
            lineas.AddRange(medicamentos.Select(m => string.Join("|", m.Codigo, m.Nombre, m.TipoDroga, m.Venta, m.Distribucion, m.Concentracion, m.Formato, m.Precio.ToString(CultureInfo.InvariantCulture), m.StockNoya1, m.StockNoya2, m.StockNoya3, m.Grupo)));
            File.WriteAllLines(archivoMedicamentos, lineas);
        }

        private void RegistrarHistorial(string operacion)
        {
            string linea = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | Usuario: " + usuarioAutenticado + " | " + operacion + Environment.NewLine;
            File.AppendAllText(archivoHistorial, linea);
        }

        private void AjustarElementosAlAncho()
        {
            int ancho = panelContenido.ClientSize.Width;
            panelFiltros.Width = Math.Max(825, ancho - 56);
            dgvProductos.Width = Math.Max(825, ancho - 56);
            dgvProductos.Height = Math.Max(180, panelContenido.ClientSize.Height - 377);
            btnModificarPrecio.Left = ancho - 165;
            btnAgregarProducto.Left = ancho - 318;
            btnModificarStock.Left = ancho - 458;
        }

        private void AplicarFiltros()
        {
            ActualizarSucursalConsultada();
            string texto = txtBuscar.Text == "Nombre, código o grupo..." ? "" : txtBuscar.Text.Trim().ToLower();
            IEnumerable<Medicamento> resultado = medicamentos.Where(m =>
                string.IsNullOrEmpty(texto) || m.Nombre.ToLower().Contains(texto) || m.Codigo.ToLower().Contains(texto) || m.Grupo.ToLower().Contains(texto));

            if (cmbTipoDroga.SelectedIndex > 0) resultado = resultado.Where(m => m.TipoDroga == cmbTipoDroga.Text);
            if (cmbVenta.SelectedIndex > 0) resultado = resultado.Where(m => m.Venta == cmbVenta.Text);
            if (cmbDistribucion.SelectedIndex > 0) resultado = resultado.Where(m => m.Distribucion == cmbDistribucion.Text);
            if (cmbConcentracion.SelectedIndex > 0) resultado = resultado.Where(m => m.Concentracion == cmbConcentracion.Text);
            if (cmbFormato.SelectedIndex > 0) resultado = resultado.Where(m => m.Formato == cmbFormato.Text);
            if (cmbSucursal.SelectedIndex > 0)
            {
                if (cmbSucursal.Text == "Noya 1 - Cosquín") resultado = resultado.Where(m => m.StockNoya1 > 0);
                if (cmbSucursal.Text == "Noya 2 - Santa María de Punilla" ) resultado = resultado.Where(m => m.StockNoya2 > 0);
                if (cmbSucursal.Text == "Noya 3 - Bialet Massé") resultado = resultado.Where(m => m.StockNoya3 > 0);
            }

            List<Medicamento> lista = resultado.OrderBy(m => m.Grupo).ThenBy(m => m.Nombre).ToList();
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = lista;
            lblResultados.Text = "Resultados: " + lista.Count + " producto" + (lista.Count == 1 ? "" : "s");
        }

        private void btnBuscar_Click(object sender, EventArgs e) { AplicarFiltros(); }

        private void cmbSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarSucursalConsultada();
        }

        private void ActualizarSucursalConsultada()
        {
            if (cmbSucursal.SelectedIndex <= 0)
            {
                lblAlcance.Text = "●  Consultando todas las sucursales";
                return;
            }

            lblAlcance.Text = "●  Consultando " + cmbSucursal.Text;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "Nombre, código o grupo..."; txtBuscar.ForeColor = Color.Gray;
            cmbTipoDroga.SelectedIndex = 0; cmbVenta.SelectedIndex = 0; cmbDistribucion.SelectedIndex = 0; cmbConcentracion.SelectedIndex = 0; cmbFormato.SelectedIndex = 0; cmbSucursal.SelectedIndex = 0;
            AplicarFiltros();
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvProductos.Columns["Acciones"].Index) ModificarPrecioSeleccionado();
        }

        private void btnModificarPrecio_Click(object sender, EventArgs e) { ModificarPrecioSeleccionado(); }

        private void btnModificarStock_Click(object sender, EventArgs e)
        {
            if (!SolicitarAutorizacion()) return;

            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un producto para modificar su stock.", "Modificar stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Medicamento medicamento = dgvProductos.CurrentRow.DataBoundItem as Medicamento;
            if (medicamento == null) return;

            int stock1Anterior = medicamento.StockNoya1;
            int stock2Anterior = medicamento.StockNoya2;
            int stock3Anterior = medicamento.StockNoya3;
            using (var dialogo = new DialogoStock(medicamento))
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    GuardarMedicamentosEnArchivo();
                    RegistrarHistorial("Stock modificado: " + medicamento.Nombre + " | Noya 1: " + stock1Anterior + " -> " + medicamento.StockNoya1 + " | Noya 2: " + stock2Anterior + " -> " + medicamento.StockNoya2 + " | Noya 3: " + stock3Anterior + " -> " + medicamento.StockNoya3);
                    AplicarFiltros();
                }
            }
        }

        private void ModificarPrecioSeleccionado()
        {
            if (!SolicitarAutorizacion()) return;

            if (dgvProductos.CurrentRow == null) { MessageBox.Show("Seleccioná un producto para modificar su precio.", "Modificar precio", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            Medicamento medicamento = dgvProductos.CurrentRow.DataBoundItem as Medicamento;
            if (medicamento == null) return;
            decimal precioAnterior = medicamento.Precio;
            using (var dialogo = new DialogoPrecio(medicamento))
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    GuardarMedicamentosEnArchivo();
                    RegistrarHistorial("Precio modificado: " + medicamento.Nombre + " | " + precioAnterior.ToString("C2") + " -> " + medicamento.Precio.ToString("C2"));
                    AplicarFiltros();
                }
            }
        }

        internal class DialogoStock : Form
    {
        private readonly NumericUpDown numStock1;
        private readonly NumericUpDown numStock2;
        private readonly NumericUpDown numStock3;

        public DialogoStock(Medicamento medicamento)
        {
            Text = "Modificar stock";
            Size = new Size(430, 270);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;

            var titulo = new Label { Text = "Stock de " + medicamento.Nombre, AutoSize = true, Location = new Point(24, 20), Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = Color.FromArgb(22, 56, 95) };
            numStock1 = CrearControlStock("Noya 1 - Cosquín", medicamento.StockNoya1, 24, 70);
            numStock2 = CrearControlStock("Noya 2 - Santa María", medicamento.StockNoya2, 145, 70);
            numStock3 = CrearControlStock("Noya 3 - Bialet Massé", medicamento.StockNoya3, 266, 70);
            var guardar = new Button { Text = "Guardar stock", Location = new Point(270, 165), Size = new Size(120, 34), BackColor = Color.FromArgb(35, 105, 170), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            guardar.Click += delegate
            {
                medicamento.StockNoya1 = (int)numStock1.Value;
                medicamento.StockNoya2 = (int)numStock2.Value;
                medicamento.StockNoya3 = (int)numStock3.Value;
                DialogResult = DialogResult.OK;
                Close();
            };
            AcceptButton = guardar;
            Controls.AddRange(new Control[] { titulo, numStock1, numStock2, numStock3, guardar });
        }

        private NumericUpDown CrearControlStock(string texto, int valor, int x, int y)
        {
            var etiqueta = new Label { Text = texto, AutoSize = true, Location = new Point(x, y), Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.FromArgb(70, 78, 89) };
            var control = new NumericUpDown { Location = new Point(x, y + 22), Width = 105, Minimum = 0, Maximum = 100000, Value = valor };
            Controls.Add(etiqueta);
            return control;
        }
    }
        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (!SolicitarAutorizacion()) return;

            using (var dialogo = new DialogoMedicamento())
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    medicamentos.Add(dialogo.Medicamento);
                    GuardarMedicamentosEnArchivo();
                    RegistrarHistorial("Producto agregado: " + dialogo.Medicamento.Nombre + " (" + dialogo.Medicamento.Codigo + ")");
                    AplicarFiltros();
                }
        }
    }

        private bool SolicitarAutorizacion()
        {
            using (var dialogo = new DialogoAutorizacion())
            {
                return dialogo.ShowDialog(this) == DialogResult.OK;
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Querés cerrar la sesión actual?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            SolicitarNuevoInicioSesion = true;
            Close();
        }
    }

    internal class DialogoPrecio : Form
    {
        private readonly NumericUpDown numericPrecio;
        public DialogoPrecio(Medicamento medicamento)
        {
            Text = "Modificar precio"; Size = new Size(360, 185); StartPosition = FormStartPosition.CenterParent; FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
            var titulo = new Label { Text = medicamento.Nombre, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(22, 56, 95), Location = new Point(24, 18), AutoSize = true };
            var etiqueta = new Label { Text = "Nuevo precio de venta", Location = new Point(24, 57), AutoSize = true };
            numericPrecio = new NumericUpDown { Location = new Point(24, 82), Width = 150, DecimalPlaces = 2, Maximum = 1000000, Minimum = 0, Value = medicamento.Precio, ThousandsSeparator = true };
            var aceptar = new Button { Text = "Guardar", DialogResult = DialogResult.OK, Location = new Point(190, 110), Width = 110, BackColor = Color.FromArgb(35, 105, 170), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            aceptar.Click += delegate { medicamento.Precio = numericPrecio.Value; }; AcceptButton = aceptar;
            Controls.AddRange(new Control[] { titulo, etiqueta, numericPrecio, aceptar });
        }
    }

    internal class DialogoMedicamento : Form
    {
        private readonly TextBox txtCodigo = new TextBox(); private readonly TextBox txtNombre = new TextBox(); private readonly ComboBox cmbTipo = new ComboBox(); private readonly ComboBox cmbVenta = new ComboBox(); private readonly ComboBox cmbDistribucion = new ComboBox(); private readonly ComboBox cmbConcentracion = new ComboBox(); private readonly ComboBox cmbFormato = new ComboBox(); private readonly NumericUpDown numPrecio = new NumericUpDown(); private readonly NumericUpDown numStock1 = new NumericUpDown(); private readonly NumericUpDown numStock2 = new NumericUpDown(); private readonly NumericUpDown numStock3 = new NumericUpDown();
        public Medicamento Medicamento { get; private set; }
        public DialogoMedicamento()
        {
            Text = "Agregar producto"; Size = new Size(570, 465); StartPosition = FormStartPosition.CenterParent; FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false; BackColor = Color.White;
            var titulo = new Label { Text = "Nuevo medicamento", Font = new Font("Segoe UI", 15, FontStyle.Bold), ForeColor = Color.FromArgb(22, 56, 95), Location = new Point(24, 18), AutoSize = true }; Controls.Add(titulo);
            Campo("Código", txtCodigo, 24, 62); Campo("Nombre", txtNombre, 290, 62); Combo("Tipo de droga", cmbTipo, new[] { "Analgésico", "Antibiótico", "Antihistamínico", "Antihipertensivo", "Broncodilatador", "Gastrointestinal", "Antidiabético" }, 24, 125); Combo("Venta", cmbVenta, new[] { "Venta libre", "Con receta" }, 290, 125); Combo("Distribución", cmbDistribucion, new[] { "Caja", "Blíster", "Frasco" }, 24, 188); Combo("Concentración", cmbConcentracion, new[] { "10 mg", "20 mg", "40 mg", "100 mg", "500 mg", "100 UI/ml" }, 290, 188); Combo("Formato", cmbFormato, new[] { "Comprimido", "Cápsula", "Jarabe", "Inhalador", "Lapicera" }, 24, 251);
            Campo("Precio", numPrecio, 290, 251); numPrecio.DecimalPlaces = 2; numPrecio.Maximum = 1000000; numPrecio.ThousandsSeparator = true;
            Campo("Stock Noya 1", numStock1, 24, 314); Campo("Stock Noya 2", numStock2, 180, 314); Campo("Stock Noya 3", numStock3, 336, 314); numStock1.Maximum = numStock2.Maximum = numStock3.Maximum = 100000;
            var aceptar = new Button { Text = "Guardar producto", Location = new Point(375, 370), Width = 145, Height = 32, BackColor = Color.FromArgb(35, 145, 105), ForeColor = Color.White, FlatStyle = FlatStyle.Flat }; aceptar.Click += Guardar; Controls.Add(aceptar); AcceptButton = aceptar;
        }
        private void Campo(string texto, Control control, int x, int y) { var etiqueta = new Label { Text = texto, AutoSize = true, Location = new Point(x, y), Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.FromArgb(70, 78, 89) }; control.Location = new Point(x, y + 20); control.Width = 220; if (control is NumericUpDown) ((NumericUpDown)control).Height = 24; Controls.Add(etiqueta); Controls.Add(control); }
        private void Combo(string texto, ComboBox combo, string[] opciones, int x, int y) { ComboBox configurado = combo; configurado.DropDownStyle = ComboBoxStyle.DropDownList; configurado.Items.AddRange(opciones); configurado.SelectedIndex = 0; Campo(texto, configurado, x, y); }
        private void Guardar(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNombre.Text)) { MessageBox.Show("Completá el código y el nombre del producto.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } Medicamento = new Medicamento(txtCodigo.Text.Trim().ToUpper(), txtNombre.Text.Trim(), cmbTipo.Text, cmbVenta.Text, cmbDistribucion.Text, cmbConcentracion.Text, cmbFormato.Text, numPrecio.Value, (int)numStock1.Value, (int)numStock2.Value, (int)numStock3.Value); DialogResult = DialogResult.OK; Close(); }
    }

    public class Medicamento
    {
        public Medicamento(string codigo, string nombre, string tipoDroga, string venta, string distribucion, string concentracion, string formato, decimal precio, int stockNoya1, int stockNoya2, int stockNoya3)
        { Codigo = codigo; Nombre = nombre; TipoDroga = tipoDroga; Grupo = ObtenerGrupo(nombre, tipoDroga); Venta = venta; Distribucion = distribucion; Concentracion = concentracion; Formato = formato; Precio = precio; StockNoya1 = stockNoya1; StockNoya2 = stockNoya2; StockNoya3 = stockNoya3; }
        public string Codigo { get; set; } public string Nombre { get; set; } public string TipoDroga { get; set; } public string Grupo { get; set; } public string Venta { get; set; } public string Distribucion { get; set; } public string Concentracion { get; set; } public string Formato { get; set; } public decimal Precio { get; set; } public int StockNoya1 { get; set; } public int StockNoya2 { get; set; } public int StockNoya3 { get; set; }

        private static string ObtenerGrupo(string nombre, string tipoDroga)
        {
            string nombreNormalizado = nombre.ToLowerInvariant();
            if (nombreNormalizado.Contains("paracetamol") || nombreNormalizado.Contains("tafirol") || nombreNormalizado.Contains("tylenol")) return "Paracetamol";
            if (nombreNormalizado.Contains("ibuprofeno") || nombreNormalizado.Contains("actron") || nombreNormalizado.Contains("ibupirac")) return "Ibuprofeno";
            return tipoDroga;
        }
    }
}