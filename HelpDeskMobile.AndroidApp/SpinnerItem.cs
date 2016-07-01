namespace HelpDeskMobile.AndroidApp
{
    internal class SpinnerItem
    {
        public string Text { get; }
        public bool IsHint { get; }

        public SpinnerItem(string strItem, bool flag)
        {
            this.IsHint = flag;
            this.Text = strItem;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}