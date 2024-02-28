using Gold.Service;

namespace Gold.Client.Usecase
{
    public class SetRecoUsecase
    {
        private readonly GraphQlService _graphQlService;

        public SetRecoUsecase(GraphQlService graphQlService)
        {
            _graphQlService = graphQlService;
        }

        public async Task<bool> Execute(int operation_id)
        {
            string qry = @"mutation {
                  updateOperation (
                    dto: {
                      operation_id: " + operation_id + @"
                      status_id: 2
                    }
                  ) {
                    id
                  }
                }";
            await _graphQlService.Execute(qry);
            return true;
        }
    }
}
