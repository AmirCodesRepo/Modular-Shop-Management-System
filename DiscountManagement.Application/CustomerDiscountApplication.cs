using _0_Framework.Application;
using DiscounrManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Application.Contracts.CustomerDiscountAgg;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepo;
        private readonly IProductApplication _productApplication;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepo, IProductApplication productApplication)
        {
            _customerDiscountRepo = customerDiscountRepo;
            _productApplication = productApplication;
        }
        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepo.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(OperationMessages.DuplicateItme);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var discount = new CustomerDiscount(command.ProductId, command.DiscountRate, startDate, endDate, command.Reason);

            _customerDiscountRepo.Create(discount);
            _customerDiscountRepo.SaveChanges();
            return operation.Succeded();
        }

        public OperationResult Eidt(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var discount = _customerDiscountRepo.Get(command.Id);

            if (discount == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            if (_customerDiscountRepo.Exists(x => x.Id != command.Id &&
            x.DiscountRate == command.DiscountRate &&
            x.ProductId == command.ProductId))
                return operation.Failed(OperationMessages.DuplicateItme);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            discount.Edit(command.ProductId, command.DiscountRate, startDate, endDate, command.Reason);

            _customerDiscountRepo.SaveChanges();
            return operation.Succeded();
        }

        public EditCustomerDiscount GetDetailes(long id)
        {
            var discount = _customerDiscountRepo.Get(id);
            return new EditCustomerDiscount
            {
                Id = discount.Id,
                DiscountRate = discount.DiscountRate,
                ProductId = discount.ProductId,
                StartDate = discount.StartDate.ToFarsi(),
                EndDate = discount.EndDate.ToFarsi(),
                Reason = discount.Reason,

            };
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {

            var result = _customerDiscountRepo.Search(searchModel.ProdutId, searchModel.StartDate, searchModel.EndDate);

            var productsId = result.Select(x => x.ProductId).Distinct().ToList();
            var products = _productApplication.GetProductsName(productsId);


            var disconts = result.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
                CreationDate = x.CreationDate.ToFarsi(),
                Reason = x.Reason,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                StartDateGr = x.StartDate,
                EndDateGr = x.EndDate,
                Product = products.ContainsKey(x.ProductId) ? products[x.ProductId] : null,
            });

            return disconts.ToList();
        }
    }
}
