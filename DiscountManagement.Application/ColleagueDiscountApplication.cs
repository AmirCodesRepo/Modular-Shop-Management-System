using _0_Framework.Application;
using DiscounrManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Application.Contracts.ColleagueDiscountAgg;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepo;
        private readonly IProductApplication _productApplication;
        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepo, IProductApplication productApplication)
        {
            _colleagueDiscountRepo = colleagueDiscountRepo;
            _productApplication = productApplication;
        }
        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_colleagueDiscountRepo.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))

                return operation.Failed(OperationMessages.DuplicateItme);
            var discount = new ColleagueDiscount(command.ProductId, command.DiscountRate);

            _colleagueDiscountRepo.Create(discount);
            _colleagueDiscountRepo.SaveChanges();
            return operation.Succeded();
        }

        public OperationResult Edit(EditColleagueDiscounnt command)
        {
            var operation = new OperationResult();
            var discount = _colleagueDiscountRepo.Get(command.Id);

            if (discount == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            if (_colleagueDiscountRepo.Exists(x => x.ProductId == command.ProductId &&
            x.DiscountRate == command.DiscountRate && x.Id != command.Id))

                return operation.Failed(OperationMessages.DuplicateItme);

            discount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepo.SaveChanges();

            return operation.Succeded();

        }

        public EditColleagueDiscounnt GetDetailes(long Id)
        {
            var discount = _colleagueDiscountRepo.Get(Id);

            return new EditColleagueDiscounnt
            {
                Id = discount.Id,
                DiscountRate = discount.DiscountRate,
                ProductId = discount.ProductId
            };
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var discount = _colleagueDiscountRepo.Get(id);
            if (discount == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            discount.Remove();
            _colleagueDiscountRepo.SaveChanges();
            return operation.Succeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var discount = _colleagueDiscountRepo.Get(id);
            if (discount == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            discount.Restore();
            _colleagueDiscountRepo.SaveChanges();
            return operation.Succeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var result = _colleagueDiscountRepo.Search(searchModel.ProductId);
            var productsId = result.Select(x =>x.ProductId).Distinct().ToList();
            var products = _productApplication.GetProductsName(productsId);

            var discounts = result.Select(x => new ColleagueDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
                CreationDate = x.CreationDate.ToFarsi(),
                IsRemoved = x.IsRemoved,
                Product = products.ContainsKey(x.ProductId) ? products[x.ProductId] : null,

            });

            return discounts.ToList();
        }
    }
}
