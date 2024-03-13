using BuyDozerBeMain.Domain.Entities;
using BuyDozerBeMain.Infrastructure.Identity;

namespace BuyDozerBeMain.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<UserEntity>();
    }
}
