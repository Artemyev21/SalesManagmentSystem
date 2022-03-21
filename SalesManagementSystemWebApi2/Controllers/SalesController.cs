using Microsoft.AspNetCore.Mvc;
using SalesManagementSystemWebApi2.BLL.Exceptions;
using SalesManagementSystemWebApi2.BLL.Models;
using SalesManagementSystemWebApi2.BLL.Services.Interfaces;
using SalesManagementSystemWebApi2.Controllers.Request;
using SalesManagementSystemWebApi2.Controllers.Response;
using System;

namespace SalesManagementSystemWebApi2.Controllers
{
    [ApiController] 
    [Route("v1/sales")]
    public class SalesController : ControllerBase 
                                                  
    {       
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpPost("Test")]
        public bool Test()
        {
            return _salesService.Test();
        }

        [HttpPost("populate-database")]
        public ActionResult<bool> PopulateDB()
        {                       
            Product[] products;
            SalesPoint[] salesPoints;
            try
            {
                products = _salesService.PopulateProduct();
                salesPoints = _salesService.PopulateSalesPoint(products);
                _salesService.PopulateProvidedProducts(products,salesPoints);
            }            
            catch (ProductTableException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (SalesPointTableException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (ProvidedProductsException ex)
            {
                return StatusCode(400, ex.Message);
            }
            return true;
        }


        [HttpPost("buy-products")]
        public ActionResult<BuyProductResponse> BuyProducts(BuyProductRequest request)        
        {  try
            {
                var buyResult = _salesService.BuyProducts(new BuyProductsModel
                {
                    BuyerId = request.BuyerId,
                    ProductId = request.ProductId,
                    SalesPointId = request.SalesPointId,
                    Quantity = request.Quantity
                });

                return Ok(new BuyProductResponse()
                {
                    Id = buyResult.Id,
                    BuyerId = buyResult.BuyerId,
                    Date = buyResult.Date,
                    SalesInfo = new BuyProductResponse.SalesData()
                    {
                        ProductId = buyResult.SalesData.ProductId,
                        ProductAmount = buyResult.SalesData.ProductAmount,
                        ProductQuantity = buyResult.SalesData.ProductQuantity
                    },
                    SalesPointId = buyResult.SalesPointId,
                    TotalAmount = buyResult.TotalAmount
                });
            }
            catch (SalesPointNotFoundException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (ProductNotFoundException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (ProductNotFoundInSalesPointException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (NotEnoughQuantityException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (NotUpdateTableException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (DataNonAddedException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }   
    }
}
