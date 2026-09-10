namespace xbd.WarehouseTHPro
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

            using FrmLogin loginForm = new FrmLogin();
            if (loginForm.ShowDialog() != DialogResult.OK || loginForm.LoginUser is null)
            {
                return;
            }

            Application.Run(new FrmMain(loginForm.LoginUser));
        }
    }
}
