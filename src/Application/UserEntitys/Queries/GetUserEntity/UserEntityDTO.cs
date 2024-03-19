using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.UserEntitys.Queries.GetUserEntity;
public class UserEntityDTO
{
    public string? Id { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public string? CompanyUser { get; init; }
    public string? PositionUser { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserEntity, UserEntityDTO>();
        }
    }
}