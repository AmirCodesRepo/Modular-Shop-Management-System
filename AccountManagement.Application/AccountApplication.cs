using _0_Framework.Application;
using AccountManagement.Application.Contracts.AccountAgg;
using AccountManagement.Domain.AccountAgg;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;
        public AccountApplication(IAccountRepository accountRepository,IPasswordHasher passwordHasher,
            IAuthHelper authHelper)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
        }
        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            if (command.Password != command.RePassword)
                return operation.Failed(OperationMessages.PasswordErrore);

            string passwordHash = _passwordHasher.Hash(command.Password);
            account.ChangePassword(passwordHash);

            _accountRepository.SaveChanges();

            return operation.Succeded();
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.Exists(x => x.UserName == command.UserName || x.Mobile == command.Mobile))
                return operation.Failed(OperationMessages.DuplicateItme);

            string passwordHash = _passwordHasher.Hash(command.Password);
            var account = new Domain.AccountAgg.Account(command.FullName, command.UserName, passwordHash, command.Mobile, command.RoleId);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();

            return operation.Succeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            if (_accountRepository.Exists(x => (x.UserName == command.UserName || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed(OperationMessages.DuplicateItme);

            account.Edit(command.FullName, command.UserName, command.Mobile, command.RoleId, command.ProfilePhoto);

            _accountRepository.SaveChanges();
            return operation.Succeded();
        }

        public EditAccount GetDetailes(long id)
        {
            var account = _accountRepository.Get(id);
            return new EditAccount
            {
                Id = id,
                FullName = account.FullName,
                UserName = account.UserName,
                Mobile = account.Mobile,
                RoleId = account.RoleId,
                ProfilePhoto = account.ProfilePhoto,
            };
        }

        public OperationResult LoginUser(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.UserName);
            if (account == null)
                return operation.Failed(OperationMessages.WrongUserPass);
            (bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);
            
            if(!result.Verified)
                return operation.Failed(OperationMessages.WrongUserPass);

            var model = new AuthViewModel(account.Id,account.RoleId,account.FullName,account.UserName);
            _authHelper.Signin(model);

            return operation.Succeded();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var result = _accountRepository.Search(searchModel.FullName, searchModel.UserName, searchModel.Mobile,searchModel.RoleId);

            return result.Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Mobile = x.Mobile,
                Role = x.Role.Name,
                ProfilePhoto = x.ProfilePhoto,
                CreationDate = x.CreationDate.ToFarsi()

            }).ToList();
        }

        public void Signout()
        {
            _authHelper.SignOut();
        }
    }
}
