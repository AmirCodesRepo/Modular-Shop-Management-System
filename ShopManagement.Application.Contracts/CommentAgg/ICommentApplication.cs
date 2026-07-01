using _0_Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.CommentAgg
{
    public interface ICommentApplication
    {
        OperationResult AddComment(AddComment command);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);
        List<CommentViewModel> Search(CommentSearchModle searchModle);


    }
}
