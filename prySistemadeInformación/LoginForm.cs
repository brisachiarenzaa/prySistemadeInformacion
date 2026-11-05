using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace prySistemadeInformación
{
    internal class FormularioLogin : Form
    {
        private static readonly string[] UsuariosPermitidos =
        {
            "Paula", "Nicole", "Laura", "Sandra", "Zaira", "Elias", "Israel", "Mariana", "Zara", "Vanesa",
            "Giuliana", "Carolina", "Oriana", "Mercedes", "Natalie", "Ignacio", "Luli", "Milena", "Gabriela",
            "Brisa", "Hernan", "Belen", "Gloria", "Soledad"
        };
        private readonly TextBox txtUsuario;
        private readonly TextBox txtContrasena;
        private readonly Label lblMensaje;

        public string UsuarioAutenticado { get; private set; }

        public FormularioLogin()
        {
            Text = "Acceso - Farmacia Noya";
            Size = new Size(420, 330);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;

            var titulo = new Label { Text = "Farmacia Noya", AutoSize = true, Location = new Point(35, 25), Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(22, 56, 95) };
            var subtitulo = new Label { Text = "Ingresá tus datos para continuar", AutoSize = true, Location = new Point(38, 65), Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(95, 105, 117) };
            var etiquetaUsuario = new Label { Text = "Usuario", AutoSize = true, Location = new Point(38, 105), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            var etiquetaContrasena = new Label { Text = "Contraseña", AutoSize = true, Location = new Point(38, 155), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            txtUsuario = new TextBox { Location = new Point(38, 125), Width = 325, Font = new Font("Segoe UI", 10) };
            txtContrasena = new TextBox { Location = new Point(38, 175), Width = 325, Font = new Font("Segoe UI", 10), PasswordChar = '●' };
            var btnIngresar = new Button { Text = "Ingresar", Location = new Point(238, 220), Size = new Size(125, 34), BackColor = Color.FromArgb(35, 105, 170), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            lblMensaje = new Label { AutoSize = true, Location = new Point(38, 230), ForeColor = Color.Firebrick };

            btnIngresar.Click += btnIngresar_Click;
            AcceptButton = btnIngresar;
            Controls.AddRange(new Control[] { titulo, subtitulo, etiquetaUsuario, etiquetaContrasena, txtUsuario, txtContrasena, btnIngresar, lblMensaje });
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuarioIngresado = txtUsuario.Text.Trim();
            bool usuarioValido = UsuariosPermitidos.Any(usuario => usuario.Equals(usuarioIngresado, StringComparison.OrdinalIgnoreCase));

            if (usuarioValido && txtContrasena.Text == "noya123")
            {
                UsuarioAutenticado = UsuariosPermitidos.First(usuario => usuario.Equals(usuarioIngresado, StringComparison.OrdinalIgnoreCase));
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            lblMensaje.Text = "Usuario o contraseña incorrectos.";
            txtContrasena.SelectAll();
            txtContrasena.Focus();
        }
    }

    internal class DialogoAutorizacion : Form
    {
        private static readonly string[] UsuariosAutorizados = { "Hernan", "Gloria", "Soledad" };
        private readonly TextBox txtUsuario;
        private readonly TextBox txtContrasena;
        private readonly Label lblMensaje;

        public DialogoAutorizacion()
        {
            Text = "Autorización requerida";
            Size = new Size(420, 300);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;

            var titulo = new Label { Text = "Verificación de permisos", AutoSize = true, Location = new Point(35, 25), Font = new Font("Segoe UI", 17, FontStyle.Bold), ForeColor = Color.FromArgb(22, 56, 95) };
            var subtitulo = new Label { Text = "Ingresá nuevamente tus credenciales para continuar", AutoSize = true, Location = new Point(38, 60), Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(95, 105, 117) };
            var etiquetaUsuario = new Label { Text = "Usuario autorizado", AutoSize = true, Location = new Point(38, 98), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            var etiquetaContrasena = new Label { Text = "Contraseña de modificación", AutoSize = true, Location = new Point(38, 148), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            txtUsuario = new TextBox { Location = new Point(38, 118), Width = 325, Font = new Font("Segoe UI", 10) };
            txtContrasena = new TextBox { Location = new Point(38, 168), Width = 325, Font = new Font("Segoe UI", 10), PasswordChar = '●' };
            var btnAutorizar = new Button { Text = "Autorizar", Location = new Point(238, 210), Size = new Size(125, 34), BackColor = Color.FromArgb(35, 105, 170), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            lblMensaje = new Label { AutoSize = true, Location = new Point(38, 218), ForeColor = Color.Firebrick };

            btnAutorizar.Click += btnAutorizar_Click;
            AcceptButton = btnAutorizar;
            Controls.AddRange(new Control[] { titulo, subtitulo, etiquetaUsuario, etiquetaContrasena, txtUsuario, txtContrasena, btnAutorizar, lblMensaje });
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            bool usuarioValido = UsuariosAutorizados.Any(usuario => usuario.Equals(txtUsuario.Text.Trim(), StringComparison.OrdinalIgnoreCase));
            if (usuarioValido && txtContrasena.Text == "noyaadmin")
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            lblMensaje.Text = "Credenciales de modificación incorrectas.";
            txtContrasena.SelectAll();
            txtContrasena.Focus();
        }
    }
}
