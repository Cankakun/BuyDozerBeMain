
using System.Net.Http.Headers;
using System.Text.Json;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.Transactions.Commands.UpdatePaymentConfirmationReceiptTransaction;
using BuyDozerBeMain.Application.Transactions.Report.GetUnitRemaining;
using BuyDozerBeMain.Application.Transactions.Report.ReportCard;
using BuyDozerBeMain.Application.Transactions.Report.SummaryTransactionStatus;
using BuyDozerBeMain.Application.Transactions.Report.TransactionReport;

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
            .MapGet(GetTransactionReport, "GetTransactionReport")
            .MapGet(GetUserTransactionType, "GetUserTransactionType")
            .MapGet(GetSummaryTransactionStatus, "GetSummaryTransactionStatus")
            .MapGet(GetReportCard, "GetReportCard")
            .MapGet(GetUnitRemaining, "GetUnitRemaining");
    }


    public async Task<List<TransactionReportDto>> GetTransactionReport(ISender sender)
    {
        return await sender.Send(new GetTransactionReportQuery());

    }
    public async Task<UnitRemainingVm> GetUnitRemaining(ISender sender)
    {
        return await sender.Send(new GetUnitRemainingQuery());

    }
    public async Task<List<UserTransactionTypeDto>> GetUserTransactionType(ISender sender)
    {
        return await sender.Send(new GetUserTransactionTypeQuery());

    }
    public async Task<List<SummaryTransactionStatusDto>> GetSummaryTransactionStatus(ISender sender)
    {
        return await sender.Send(new GetSummaryTransactionStatusQuery());

    }
    public async Task<List<ReportCardDto>> GetReportCard(ISender sender)
    {
        return await sender.Send(new GetReportCardQuery());

    }
}
