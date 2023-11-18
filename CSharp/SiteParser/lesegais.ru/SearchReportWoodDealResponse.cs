using System.Collections.Generic;

namespace lesegais.ru
{
  public class SearchReportWoodDealResponse
  {
    public SearchReportWoodDealContent SearchReportWoodDeal { get; set; }
    public class SearchReportWoodDealContent
    {
      public List<ContentContent> Content { get; set; }
      public class ContentContent
      {
        public string SellerName { get; set; }
        public string SellerInn { get; set; }
        public string BuyerName { get; set; }
        public string BuyerInn { get; set; }
        public string WoodVolumeBuyer { get; set; }
        public string WoodVolumeSeller { get; set; }
        public string DealDate { get; set; }
        public string DealNumber { get; set; }
        public string __Typename { get; set; }
      }
      public string __Typename { get; set; }
    }
  }
}
