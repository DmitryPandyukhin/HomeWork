using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace lesegais.ru
{
  public class SearchReportWoodDealRequest
  {
    string Link { get; } = "https://www.lesegais.ru/open-area/graphql";
    string _Query { get; } = @"
          query SearchReportWoodDeal($size: Int!, $number: Int!, $filter: Filter, $orders: [Order!]) {
            searchReportWoodDeal(filter: $filter, pageable: {number: $number, size: $size}, orders: $orders) {
              content {
                sellerName
                sellerInn
                buyerName
                buyerInn
                woodVolumeBuyer
                woodVolumeSeller
                dealDate
                dealNumber
                __typename
              }
              __typename
            }
          }";
    uint PageSize { get; }
    uint PageNumber { get; }
    GraphQLResponse<SearchReportWoodDealResponse> Response;
    public SearchReportWoodDealRequest(uint pageSize, uint pageNumber)
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
          OperationName = "SearchReportWoodDeal",
          Variables = new
          {
            size = PageSize,
            number = PageNumber
          }
        };

        Response = await graphQLClient.SendQueryAsync<SearchReportWoodDealResponse>(gqlRequest);
      }
    }
  }
}
