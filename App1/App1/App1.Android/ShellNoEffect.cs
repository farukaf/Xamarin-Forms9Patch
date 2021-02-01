using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using App1.Droid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Netspeed")]
[assembly: ExportEffect(typeof(App1.Droid.ShellNoEffect), "ShellNoEffect")]
namespace App1.Droid
{
    public class ShellNoEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (!(Container.GetChildAt(0) is ViewGroup layout))
                return;

            if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
                return;

            var bottomNavMenuView = bottomNavigationView.GetChildAt(0) as BottomNavigationMenuView;

            for (int i = 0; i < bottomNavMenuView.ChildCount; i++)
            {
                var item = bottomNavMenuView.GetChildAt(i) as BottomNavigationItemView;
                var itemTitle = item.GetChildAt(1);

                var smallTextView = ((TextView)((BaselineLayout)itemTitle).GetChildAt(0));
                var largeTextView = ((TextView)((BaselineLayout)itemTitle).GetChildAt(1));

                smallTextView.SetTextSize(Android.Util.ComplexUnitType.Sp, 8);
                largeTextView.SetTextSize(Android.Util.ComplexUnitType.Sp, 8); 
            }
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
        }

    }
}