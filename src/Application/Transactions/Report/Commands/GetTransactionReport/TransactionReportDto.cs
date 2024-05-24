using BuyDozerBeMain.Application.Transactions.TransactionDetailBuy.Queries.GetTransactionDetailBuy;
using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Transactions.Report.TransactionReport;
public class TransactionReportDto
{
    public int MonthTransaction { get; set; }
    public int TransaksiSewa { get; set; }
    public int TransaksiBeli { get; set; }
    public int Transaksi { get; set; }

}