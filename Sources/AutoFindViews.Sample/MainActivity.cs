namespace AutoFindViews.Sample
{
	using Android.App;
	using Android.OS;

	[Activity(Label = "AutoFindViews", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		public MainLayoutHolder Layout { get; private set; }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			this.Layout = this.SetContentView<MainLayoutHolder>();
			this.Layout.included.title.Text = "sub layout title";

			this.Layout.myButton.Click += delegate { this.Layout.myButton.Text = $"{count++} clicks!"; };
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Layout.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}

