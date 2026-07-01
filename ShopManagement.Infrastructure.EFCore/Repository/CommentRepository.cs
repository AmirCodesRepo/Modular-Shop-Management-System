using _0_Framework.Infrastucture;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly ShopContext _context;
        public CommentRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
        public List<Comment> Search(string name, string email)
        {
            var queery = _context.Comments.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                queery = queery.Where(x => x.Name.Contains(name));
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                queery = queery.Where(x => x.Email.Contains(email));
            }

            return queery.OrderByDescending(x=> x.Id).ToList();
        }
    }
}
