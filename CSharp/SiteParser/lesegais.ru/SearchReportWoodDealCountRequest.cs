using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace lesegais.ru
{
  public class SearchReportWoodDealCountRequest
  {
    string Link { get; } = "https://www.lesegais.ru/open-area/graphql";
    string _Query { get; } = @"
          query SearchReportWoodDealCount($size: Int!, $number: Int!, $filter: Filter, $orders: [Order!]) {
            searchReportWoodDeal(filter: $filter, pageable: {number: $number, size: $size}, orders: $orders) {
              total
              number
              size
              overallBuyerVolume
              overallSellerVolume
              __typename
            }
          }";
    uint PageSize { get; }
    uint PageNumber { get; }
    GraphQLResponse<SearchReportWoodDealCountResponse> Response;
    public SearchReportWoodDealCountRequest(uint pageSize, uint pageNumber)
    {
      PageSize = pageSize;
      PageNumber = pageNumber;
    }
    public async void Run()
    {
      using (var graphQLClient = new GraphQLHttpClient(Link, new NewtonsoftJsonSerializer()))
      {
        var gqlRequest = new GraphQLRequest
        {
          Query = _Query,
          OperationName = "SearchReportWoodDealCount",
          Variables = new
          {
            size = PageSize,
            number = PageNumber
          }
        };

        Response = await graphQLClient.SendQueryAsync<SearchReportWoodDealCountResponse>(gqlRequest);
      }
    }
  }
}
