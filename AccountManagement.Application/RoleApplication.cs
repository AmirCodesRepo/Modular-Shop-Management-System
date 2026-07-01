using _0_Framework.Application;
using AccountManagement.Application.Contracts.RoleAgg;
using AccountManagement.Domain.RoleAgg;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;
        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();
            if (_roleRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(OperationMessages.DuplicateItme);

            var Role = new Role(command.Name);
            _roleRepository.Create(Role);
            _roleRepository.SaveChanges();

            return operation.Succeded();
        }


        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var Role = _roleRepository.Get(command.Id);
            if(Role == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            if (_roleRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(OperationMessages.DuplicateItme);

            Role.Edit(command.Name);
            _roleRepository.SaveChanges();

            return operation.Succeded();
        }

        public EditRole GetDetailes(long id)
        {
            var Role = _roleRepository.Get(id);
            return new EditRole
            {
                Id = Role.Id,
                Name = Role.Name,
            };
        }

        public List<RoleViewModel> List()
        {
            var Roles = _roleRepository.GetAll();
            return Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name= x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();
        }
    }
}
