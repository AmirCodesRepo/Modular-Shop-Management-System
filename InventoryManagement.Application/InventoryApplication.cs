using _0_Framework.Application;
using InventoryManagement.Application.Contracts.InventoryAgg;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductApplication _productApplication;
        public InventoryApplication(IInventoryRepository inventoryRepository, IProductApplication productApplication)
        {
            _inventoryRepository = inventoryRepository;
            _productApplication = productApplication;
        }
        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(OperationMessages.DuplicateItme);

            var inventory = new Inventory(command.ProductId, command.UnitPrice);

            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Succeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);

            if (inventory == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(OperationMessages.DuplicateItme);

            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();

            return operation.Succeded();
        }

        public EditInventory GetDetails(long id)
        {
            var inventory = _inventoryRepository.Get(id);
            return new EditInventory
            {
                Id = inventory.Id,
                ProductId = inventory.ProductId,
                UnitPrice = inventory.UnitPrice
            };
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            List<InventoryOperationViewModel> operations = new List<InventoryOperationViewModel>();

            var inventory = _inventoryRepository.Get(inventoryId);

            foreach (var item in inventory.Operations)
            {
                operations.Add(new InventoryOperationViewModel
                {
                    Id = item.Id,
                    Count = item.Count,
                    CurrentCount = item.CurrentCount,
                    Description = item.Description,
                    OperationDate = item.OperationDate.ToFarsi(),
                    Operator = "admin",
                    OperatorId = 1,
                    Opertion = item.Operation,
                    OrderId = item.OrderId,
                });
            }
            return operations.OrderByDescending(x => x.Id).ToList();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            const long OperatorId = 1;
            inventory.Increase(command.Count, OperatorId, command.Description);
            _inventoryRepository.SaveChanges();

            return operation.Succeded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);

            if (inventory == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            const long OperatorId = 1;
            inventory.Reduce(command.Count, OperatorId, command.Description, 0);

            _inventoryRepository.SaveChanges();
            return operation.Succeded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            const long OperatorId = 1;
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetByProductId(item.ProductId);

                if (inventory == null)
                    return operation.Failed(OperationMessages.NotFoundItem);

                inventory.Reduce(item.Count, OperatorId, item.Description, item.OrderId);
            }
            _inventoryRepository.SaveChanges();
            return operation.Succeded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var result = _inventoryRepository.Search(searchModel.ProductId, searchModel.InStock);
            var ids = result.Select(x => x.ProductId).Distinct().ToList();
            var products = _productApplication.GetProductsName(ids);

            return result.Select(x => new InventoryViewModel
            {
                ProductId = x.ProductId,
                InStock = x.InStock,
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                CreationDate = x.CreationDate.ToFarsi(),
                CurrentCount = x.CalculateCurrentCount(),
                Product = products.ContainsKey(x.ProductId) ? products[x.ProductId] : null
            }).ToList();
        }
    }
}
