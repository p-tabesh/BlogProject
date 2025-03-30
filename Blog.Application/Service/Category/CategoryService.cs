using AutoMapper;
using Blog.Application.RequestModel;
using Blog.Application.Service.Category;
using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;

namespace Blog.Application.Category.Service;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public void Add(AddCategoryRequest request)
    {
        var category = _mapper.Map<Domain.Entity.Category>(request);
        _categoryRepository.Add(category);
        _unitOfWork.Commit();
    }
}
