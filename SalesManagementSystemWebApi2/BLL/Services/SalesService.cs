using System;
using System.Linq;
using SalesManagementSystemWebApi2.BLL.Exceptions;
using SalesManagementSystemWebApi2.BLL.Services.Interfaces;
using SalesManagementSystemWebApi2.BLL.Models;
using SalesManagementSystemWebApi2.DAL.Interfaces;
using System.Collections.Generic;

namespace SalesManagementSystemWebApi2.BLL.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesPointRepository _salesPointRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProvidedProductsRepository _providedProductsRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly ISalesDataRepository _salesDataRepository;
        private readonly IBuyerRepository _buyerRepository;

        private Random random = new Random();

        public SalesService(ISalesPointRepository salesPointRepository,
                            IProductRepository productRepository,
                            IProvidedProductsRepository providedProductsRepository,
                            ISaleRepository saleRepository,
                            ISalesDataRepository salesDataRepository,
                            IBuyerRepository buyerRepository)
        {
            _salesPointRepository = salesPointRepository;
            _productRepository = productRepository;
            _providedProductsRepository = providedProductsRepository;
            _saleRepository = saleRepository;
            _salesDataRepository = salesDataRepository;
            _buyerRepository = buyerRepository;
        }

        public Sale BuyProducts(BuyProductsModel model)
        {
            //Проверить что точка продажи существует.       
            bool isSalesPointExists = _salesPointRepository.CheckExists(model.SalesPointId);
            if (!isSalesPointExists)
            {
                throw new SalesPointNotFoundException("Точка продажи не существует");
            }


            //Проверить что товар существует.                        
            bool isProductExists = _productRepository.CheckExists(model.ProductId);
            if (!isProductExists)
            {
                throw new ProductNotFoundException("Товар не существует");
            }


            //Проверить что товар существует в точке продажи
            var productsInSalesPoint = _providedProductsRepository.Query(model.SalesPointId, model.ProductId);
            if (!productsInSalesPoint.Any())
            {
                throw new ProductNotFoundInSalesPointException("Товар не существует в точке продажи");
            }


            //Проверить что товара нужное количество в точке продажи
            var productInSalePoint = productsInSalesPoint.Single();
            if (productInSalePoint.ProductQuantity <= model.Quantity)
            {
                throw new NotEnoughQuantityException("Нет нужного количества товара");
            }


            //Обновляем таблицу ProvidedProducts                                   
            int updateQuantity = productInSalePoint.ProductQuantity - model.Quantity;                        
            _providedProductsRepository.Update(model.SalesPointId, model.ProductId, updateQuantity);                                    

            var product = _productRepository.Query(model.ProductId);
            var amount = model.Quantity * product.Price;//подсчет общей суммы
            var date = DateTime.Now;
            int saleId = 0;


            //Добавляем данные в таблицу Sale              
            saleId = _saleRepository.Create(date, model.SalesPointId, model.BuyerId, amount);                                    
            

            //Добавляем данные в таблицу SalesData                                    
            _salesDataRepository.Create(saleId, model.ProductId, model.Quantity, amount);                        
            

            ///Оформить продажу 
            SalesData salesData = new SalesData
            {
                ProductId = model.ProductId,
                ProductAmount = amount,
                ProductQuantity = model.Quantity
            };
            return new Sale()
            {
                Id = saleId,
                Date = date,
                BuyerId = model.BuyerId,
                TotalAmount = amount,
                SalesPointId = model.SalesPointId,
                SalesData = salesData
            };
        }

        public Product[] PopulateProduct()
        {
            //Сначала удаляем все данные из таблицы Product                       
            _productRepository.DeleteAll();
            
            //Массив с названиями продуктов
            string[] productsStringArray = {
                    "cabbage",
                    "tomatos",
                    "cheese",
                    "bread",
                    "milk",
                    "onion",
                    "bacon",
                    "eggs",
                    "juice"
            };

            //randomLengthProduct - случайное число для размера массива
            int randomLengthProduct = random.Next(4, productsStringArray.Length);
            Product[] products = new Product[randomLengthProduct];

            //randomProduct - случайное число для выбора продукта из массива productsStringArray            
            int randomProduct = 0;
            //Заполняем циклом products
            for (int i = 0; i < products.Length; i++)
            {
                randomProduct = random.Next(i, productsStringArray.Length);
                products[i] = new Product
                {
                    Id = i,
                    Name = productsStringArray[randomProduct] + $" сорт {i}",
                    Price = random.Next(20, 100)
                };
            }

            //Заполняем таблицу product в БД
            foreach (var product in products)
            {
                bool isProductExist = _productRepository.Create(product.Name, product.Price);
                if (!isProductExist)
                {
                    throw new ProductTableException("Таблица Product не заполнена");
                }
            }

            return products;
        }

        public SalesPoint[] PopulateSalesPoint(Product[] products)
        {
            //Сначала удаляем все данные из таблицы SalesPoint  
            _salesPointRepository.DeleteAll();

            //Массив магазинов(точек продаж)            
            string[] salesPointStringArray =
            {
                "Auchan",
                "Pyaterochka",
                "Perekrestok",
                "Dixy",
                "Magnit"
            };

            //randomLengthSalesPoint - случайное число для размера массива
            var randomLengthSalesPoint = random.Next(2, salesPointStringArray.Length);
            SalesPoint[] salesPoints = new SalesPoint[randomLengthSalesPoint];

            //randomSalesPoint - случайное число для выбора продукта из массива salesPointStringArray
            int randomSalesPoint = 0;


            //Заполняем случайными данными products с помощью цикла
            for (int i = 0; i < salesPoints.Length; i++)
            {
                randomSalesPoint = random.Next(i, salesPointStringArray.Length);
                salesPoints[i] = new SalesPoint()
                {
                    Id = i,
                    Name = salesPointStringArray[randomSalesPoint] + $" {i}",
                    ProvidedProducts = RandomProvidedProduct(products)
                };
            }

            //Заполняем таблицу SalesPoint в БД
            foreach (var point in salesPoints)
            {
                bool isSalesPointExist = _salesPointRepository.Create(point.Name);
                if (!isSalesPointExist)
                {
                    throw new SalesPointTableException("Таблица SalesPoint не заполнена");
                }
            }

            return salesPoints;
        }

        private List<ProvidedProduct> RandomProvidedProduct(Product[] products)
        {
            List<ProvidedProduct> providedProducts = new List<ProvidedProduct>();

            int randomLeghtProduct = random.Next(1, products.Length);

            for (int i = 0; i < randomLeghtProduct; i++)
            {
                providedProducts.Add(
                    new ProvidedProduct()
                    {
                        ProductId = products[i].Id,
                        ProductQuantity = random.Next(20,100)
                    }
                    ); ;
            }

            return providedProducts;        
        }

        public void PopulateProvidedProductsTable(SalesPoint[] salesPoints)
        {
            //Сначала удаляем все данные из таблицы ProvidedProducts
            _providedProductsRepository.DeleteAll();

            //Создаем список доступных к продаже продуктов в определенной точке продаж в таблице ProvidedProducts

            //lenghtProvidedProduct - размера массива
            int lenghtProvidedProduct = product.Length * salesPoints.Length;
            var providedProducts = new ProvidedProduct[lenghtProvidedProduct];
            int count = 0;
            //Заполняем случайными данными providedProducts с помощью цикла
            for (int i = 0; i < salesPoints.Length; i++)
            {
                for (int j = 0; j < product.Length; j++)
                {
                    providedProducts[count] = new ProvidedProduct()
                    {                        
                        ProductId = product[random.Next(1, product.Length)].Id,
                        ProductQuantity = random.Next(10, 500),                        
                    };
                }
            }
        }

        public bool Test()
        {
            var res = _productRepository.Create("ASD", 34444);
            return res;
            //throw new NotImplementedException();            
        }
    }
}
