namespace lesegais.ru
{
  public class SearchReportWoodDealCountResponse
  {
    public SearchReportWoodDealContent SearchReportWoodDeal { get; set; }
    public class SearchReportWoodDealContent
    {
      public string Total { get; set; }
      public string Number { get; set; }
      public string Size { get; set; }
      public string OverallBuyerVolume { get; set; }
      public string OverallSellerVolume { get; set; }
      public string __Typename { get; set; }
    }
  }
}
