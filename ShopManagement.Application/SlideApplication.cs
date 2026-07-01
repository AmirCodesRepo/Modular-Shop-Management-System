using _0_Framework.Application;
using ShopManagement.Application.Contracts.SlideAgg;
using ShopManagement.Application.Mapping;
using ShopManagement.Domain.SlideAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IFileUploader _fileUploader;
        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _slideRepository = slideRepository;
            _fileUploader = fileUploader;
        }
        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();

            var picturePath = _fileUploader.UploadFile(command.Picture , "Slides");
            _slideRepository.Create(MapTo.NewSlide(command, picturePath));
            _slideRepository.SaveChanges();
            return operation.Succeded();
        }
        
        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();

            var slid = _slideRepository.Get(command.Id);
            if (slid == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            var picturePath = _fileUploader.UploadFile(command.Picture , "Slides");

            MapTo.ExistsSlide(slid, command, picturePath);
            _slideRepository.SaveChanges();

            return operation.Succeded();
        }

        public EditSlide GetDetails(long id)
        {
            var slide = _slideRepository.Get(id);
            return MapTo.EditSlide(slide);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slid = _slideRepository.Get(id);

            if (slid == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            slid.Remove();
            _slideRepository.SaveChanges();

            return operation.Succeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slid = _slideRepository.Get(id);

            if (slid == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            slid.Restore();
            _slideRepository.SaveChanges();

            return operation.Succeded();
        }

        public List<SlideViewModel> Search(SlideSearchModel searchModel)
        {
            var slides = _slideRepository.Search(searchModel.Heading,searchModel.IsRemoved);

            return slides.Select(x => new SlideViewModel
            {
                Id = x.Id,
                Heading = x.Heading,
                Picture = x.Picture,
                BtnText = x.BtnText,
                Title = x.Title,
                IsRemoved = x.IsRemoved,
                CreationDate = x.CreationDate.Date.ToFarsi()
            }).ToList();
        }
    }
}
