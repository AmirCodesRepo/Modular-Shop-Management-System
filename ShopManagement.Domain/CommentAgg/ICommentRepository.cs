using _0_Framework.Domain;
using System.Collections.Generic;

namespace ShopManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<long,Comment>
    {
        List<Comment> Search(string name,string email);
    }
}
