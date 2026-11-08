using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prySistemadeInformación
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                string usuarioAutenticado;
                using (var login = new FormularioLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK) return;
                    usuarioAutenticado = login.UsuarioAutenticado;
                }

                using (var farmacia = new FarmaciasNoya(usuarioAutenticado))
                {
                    Application.Run(farmacia);
                    if (!farmacia.SolicitarNuevoInicioSesion) return;
                }
            }

        }
    }
}
