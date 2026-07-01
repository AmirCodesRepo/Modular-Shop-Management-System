namespace DiscountManagement.Application.Contracts.ColleagueDiscountAgg
{
    public class ColleagueDiscountViewModel
    {
        public long Id {  set; get; }
        public long ProductId {  set; get; }
        public string Product { set; get; }
        public int DiscountRate { set; get; }
        public bool IsRemoved { set; get; }
        public string CreationDate { set; get; }

    }
}
