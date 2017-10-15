namespace AutoFindViews.Sample
{
    // Main.axml
    [System.CodeDom.Compiler.GeneratedCode("AutoFindViews", "0.1.0.0")]
    public class MainLayoutHolder : ILayoutHolder
    {
        #region Fields

		// L0: @+id/root5
		private Android.Widget.LinearLayout _root5;

		// L0: @+id/myButton
		private Android.Widget.Button _myButton;

		// L0: @+id/included
		private SublayoutLayoutHolder _included;

        #endregion

        #region Properties

        public int Identifier { get; } = Resource.Layout.Main;

        public Android.Views.View Source { get; set; }

		// L0: @+id/root5
		public Android.Widget.LinearLayout root5 => _root5 ?? (_root5 = Source.FindViewById<Android.Widget.LinearLayout>(Resource.Id.root5));

		// L0: @+id/myButton
		public Android.Widget.Button myButton => _myButton ?? (_myButton = Source.FindViewById<Android.Widget.Button>(Resource.Id.myButton));

		// L0: @+id/included
		public SublayoutLayoutHolder included => _included ?? (_included = new SublayoutLayoutHolder(Source.FindViewById<Android.Views.View>(Resource.Id.included)));

        #endregion

        public MainLayoutHolder() {}

        public MainLayoutHolder(Android.Views.View source)
        {
            this.Source = source;
        }
    }
}
