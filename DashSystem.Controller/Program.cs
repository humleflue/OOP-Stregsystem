using DashSystem.UI;

namespace DashSystem.Controller
{
    class Program
    {
        private static void Main()
        {
            IDashSystem dashSystem = new DashSystem();
            IDashSystemUI ui = new DashSystemCLI(dashSystem);
            new DashSystemController(ui, dashSystem);

            ui.Start();
        }
    }
}
