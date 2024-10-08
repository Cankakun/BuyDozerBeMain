using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;
using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Transactions.TransactionDetailRent.Commands.CreateTransactionDetailRent;
[Authorize]
public record CreateTransactionDetailRentCommand : IRequest<object>
{
    // public required string TransactionNum { get; init; }
    public required string UnitId { get; init; }
    public required string UserId { get; init; }
    public required string PriceListRentId { get; init; }
    public required string ReceiverName { get; init; }
    public required string ReceiverHp { get; init; }
    public required string ReceiverAddress { get; init; }
    public required int QtyTransaction { get; init; }
    public required DateOnly DateTransaction { get; init; }
    public required int StatusTransaction { get; init; }
    public required DateOnly DateRent { get; init; }
}
public class CreateTransactionDetailRentCommandHandler : IRequestHandler<CreateTransactionDetailRentCommand, object>
{
    private readonly IApplicationDbContext _context;
    public CreateTransactionDetailRentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(CreateTransactionDetailRentCommand request, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.Now;
        var priceListRent = await _context.PriceListRents.FindAsync(request.PriceListRentId);
        var heavyUnit = _context.HeavyUnits.Find(request.UnitId);
        if (heavyUnit is not null && priceListRent is not null)
        {
            if (request.QtyTransaction > heavyUnit.QtyUnit || heavyUnit.QtyUnit == 0)
            {
                return new Response { Status = 400, Message = "Bad Request", Data = "Kuantitas Unit yang dipesan melebihi Unit yang tersedia!" };
            }
            decimal price = Convert.ToDecimal(request.QtyTransaction) * heavyUnit.PriceRentUnit;
            var transaction = new Transaction
            {
                TransactionNum = "TRX" + now.ToString("yyyyMMddHHmmss"),
                UnitId = request.UnitId,
                UserId = request.UserId,
                PriceListRentId = priceListRent.Id,
                ReceiverName = request.ReceiverName,
                ReceiverHp = request.ReceiverHp,
                ReceiverAddress = request.ReceiverAddress,
                QtyTransaction = request.QtyTransaction,
                TotalPriceTransaction = price * priceListRent.PriceRentUnit + price,
                DateTransaction = request.DateTransaction,
                StatusTransaction = request.StatusTransaction,
                DetailRents = new DetailRent { DateRent = request.DateRent, DateReturn = request.DateRent.AddMonths(priceListRent.Months) }
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync(cancellationToken);

            if (transaction is not null)
            {
                var response = new Response
                {
                    Status = 200,
                    Message = "success",
                    Data = await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionNum == transaction.TransactionNum)
                };
                // var jsonResult = JObject.Parse(JsonSerializer.Serialize(response));
                // jsonResult.Add("DataDetail", JsonSerializer.Serialize(detail));

                return response;

            }
            else
            {
                var response = new Response
                {
                    Status = 400,
                    Message = "Bad Request",
                    Data = "Kesalahan pada saat transaksi"
                };

                return response;

            }

        }
        else
        {
            var response = new Response
            {
                Status = 404,
                Message = "Not Found",
                Data = "Barang tidak ditemukan!"
            };

            return response;
        }

    }
}