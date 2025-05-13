

namespace Pixsale.Application.Models;

public record CreatePurchase(
      DateTime PurchaseDate,
      Guid SupplierId,
      List<PurchaseDetail> PurchaseDetails
    );

public record UpdatePurchase(
      Guid Id,
      DateTime PurchaseDate,
      Guid SupplierId,
      PurchaseStatus Status,
      List<PurchaseDetail> PurchaseDetails
    );

public record GetPurchase(
      int Id,
      DateTime PurchaseDate,
      Guid SupplierId,
      PurchaseStatus Status
    );

public record GetPurchasDetail(
      Guid Id,
      DateTime PurchaseDate,
      Guid SupplierId,
      PurchaseStatus Status,
      List<PurchaseDetail> PurchaseDetails
    );


public enum PurchaseStatus
{
    Quotation,
    Draft,
    Confirmed,
    Received,
    Cancelled
}

public record PurchaseDetail(
    Guid Id,
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal VATAmount
    );