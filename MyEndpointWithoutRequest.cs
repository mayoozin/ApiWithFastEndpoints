using ApiWithFastEndpoints.Model;

namespace ApiWithFastEndpoints
{
    public class MyEndpointWithoutRequest : EndpointWithoutRequest<MyResponse>
    {
        public override void Configure()
        {
            Get("/api/person");
            AllowAnonymous();
        }

        //public override async Task HandleAsync(CancellationToken ct)
        //{
        //    var person = await dbContext.GetFirstPersonAsync();

        //    Response.FullName = person.FullName;
        //    Response.Age = person.Age;
        //}
        public override Task HandleAsync(CancellationToken ct)
        {
            Response = new()
            {
                FullName = "john doe",
                Age = 50
            };
            return Task.CompletedTask;
        }
    }
}
