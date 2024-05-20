
using System.Net.Http.Headers;
using System.Text.Json;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.Transactions.Commands.UpdatePaymentConfirmationReceiptTransaction;
using BuyDozerBeMain.Application.Transactions.Report.TransactionReport;

// using BuyDozerBeMain.Application.Transactions.Report.TransactionReport;
using BuyDozerBeMain.Application.Transactions.TransactionOnGoing.Queries.GetTransactionOnGoing;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BuyDozerBeMain.Web.Endpoints;

public class TransactionReport : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTransactionReport, "GetTransactionReport");
    }


    public async Task<List<TransactionReportDto>> GetTransactionReport(ISender sender)
    {
        return await sender.Send(new GetTransactionReportQuery());

    }
}
