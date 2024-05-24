namespace BuyDozerBeMain.Application.Transactions.Report.SummaryTransactionStatus;

public class SummaryTransactionStatusDto
{
    public int TransactionRejected { get; set; }
    public int TransactionOnGoing { get; set; }
    public int TransactionPaid { get; set; }
    public int TransactionFinish { get; set; }
}
