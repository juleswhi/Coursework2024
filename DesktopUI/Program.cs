using RayEngine;
using PhysicsEngine;
using FormsLib.Chess;
using FormsLib.Menus;
using FormsLib.Platformer;
using UserDetails;

namespace DesktopUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new formLevelEditor());
        }
    }
}