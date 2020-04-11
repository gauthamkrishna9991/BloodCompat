using System;
using Gtk;

namespace BloodCompat
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Application("com.github.gauthamkrishna9991.BloodCompat", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow();
            win.SetIconFromFile("appicon.png");
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}
