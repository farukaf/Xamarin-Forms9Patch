using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1;
using App1.Droid;
using Android.Support.Design.Internal;
using Android.Text;
using Android.Text.Style;

[assembly: ExportRenderer(typeof(Shell), typeof(App1.Droid.MyShellRenderer))]
namespace App1.Droid
{
    public class MyShellRenderer : ShellRenderer
    {
        public MyShellRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (!(sender is Shell shell))
            {
                return;
            }

            if (!(shell.FindByName<TabBar>("tabBar") is ShellItem tabBar))
            {
                return;
            }

        }

        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {

            return new CustomBottomNavAppearance();
        }
    }

    public class CustomBottomNavAppearance : IShellBottomNavViewAppearanceTracker
    {
        public void Dispose()
        {

        }

        public void ResetAppearance(BottomNavigationView bottomView)
        {

        }


        public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            IMenu myMenu = bottomView.Menu;
            for (int i = 0; i < myMenu.Size(); i++)
            {
                var item = myMenu.GetItem(i);
                SpannableString span = new SpannableString(item.ToString());
                int end = span.Length();
                //span.SetSpan(new AbsoluteSizeSpan(14, true), 0, end, SpanTypes.ExclusiveExclusive);
                span.SetSpan(new Rel ativeSizeSpan(0.8f), 0, end, SpanTypes.ExclusiveExclusive);
                item.SetTitle(span);
            }
        }
    }

}