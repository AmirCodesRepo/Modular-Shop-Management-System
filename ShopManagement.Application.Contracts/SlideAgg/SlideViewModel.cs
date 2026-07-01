namespace ShopManagement.Application.Contracts.SlideAgg
{
    public class SlideViewModel
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public string BtnText { get; set; }
        public bool IsRemoved { get; set; }
        public string CreationDate { get; set; }

    }
}
