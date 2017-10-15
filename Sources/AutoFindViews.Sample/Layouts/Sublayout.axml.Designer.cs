namespace AutoFindViews.Sample
{
    // Sublayout.axml
    [System.CodeDom.Compiler.GeneratedCode("AutoFindViews", "0.1.0.0")]
    public class SublayoutLayoutHolder : ILayoutHolder
    {
        #region Fields

        #endregion

        #region Properties

        public int Identifier { get; } = Resource.Layout.Sublayout;

        public Android.Views.View Source { get; set; }

        #endregion

        public SublayoutLayoutHolder() {}

        public SublayoutLayoutHolder(Android.Views.View source)
        {
            this.Source = source;
        }
    }
}
