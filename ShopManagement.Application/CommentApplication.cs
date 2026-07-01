using _0_Framework.Application;
using ShopManagement.Application.Contracts.CommentAgg;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;
        public CommentApplication(ICommentRepository commentRepository,IProductRepository productRepository)
        {
            _commentRepository = commentRepository;
            _productRepository = productRepository;
        }
        public OperationResult AddComment(AddComment command)
        {
            var operation = new OperationResult();
            var comment = new Comment(command.Name, command.Email, command.Message, command.ProductId);

            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();

            return operation.Succeded();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            comment.Cancel();
            _commentRepository.SaveChanges();

            return operation.Succeded();

        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            comment.Confirm();
            _commentRepository.SaveChanges();

            return operation.Succeded();
        }

        public List<CommentViewModel> Search(CommentSearchModle searchModle)
        {
            var result = _commentRepository.Search(searchModle.Name, searchModle.Email);

            var ids = result.Select(x => x.ProductId).Distinct().ToList();

            var products = _productRepository.GetProductsName(ids);

            return result.Select(x => new CommentViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                IsCanceld = x.IsCanceld,
                IsConfirmed = x.IsConfirmed,
                CreationDate = x.CreationDate.ToFarsi(),
                ProductName = products.ContainsKey(x.ProductId) ? products[x.ProductId] : null

            }).ToList();
        }
    }
}
