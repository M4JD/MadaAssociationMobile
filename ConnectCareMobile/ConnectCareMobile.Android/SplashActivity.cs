using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using System.Threading.Tasks;

namespace ConnectCareMobile.Droid
{
    [Activity(Icon = "@mipmap/icon", RoundIcon = "@mipmap/icon", MainLauncher = true, Theme = "@style/MainTheme.Splash", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]

    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
        public override void OnBackPressed() { }
        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() => { SimulateStartupAsync(); });
            startupWork.Start();
        }

        private async void SimulateStartupAsync()
        {
            await Task.Delay(3000);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}