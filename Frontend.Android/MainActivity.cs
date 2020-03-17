using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using static Android.Widget.AdapterView;
using System;
using Android.Views;

namespace Frontend.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);            
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.file_settings:
                    Toast.MakeText(this, "File Settings", ToastLength.Short).Show();
                    break;
                case Resource.Id.new_game1:
                    Toast.MakeText(this, "New Game", ToastLength.Short).Show();
                    break;
                case Resource.Id.help:
                    Toast.MakeText(this, "Help", ToastLength.Short).Show();
                    break;
                case Resource.Id.about_app:
                    Toast.MakeText(this, "About App", ToastLength.Short).Show();
                    break;
                default:
                    break;
            }
            
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.myMenu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }
    }
}