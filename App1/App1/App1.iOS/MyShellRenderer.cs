
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(App1.AppShell), typeof(App1.iOS.MyShellRenderer))]
namespace App1.iOS
{ 
    public class MyShellRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);
            if (renderer != null)
            {
            }
            return renderer;
        }
    }
}