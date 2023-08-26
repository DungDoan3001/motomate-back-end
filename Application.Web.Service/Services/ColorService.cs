using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Application.Web.Service.Services
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Color> _colorRepo;
        private readonly IColorQueries _colorQueries;
        private readonly IMapper _mapper;

        public ColorService(IUnitOfWork unitOfWork, IColorQueries colorQueries, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _colorRepo = unitOfWork.GetBaseRepo<Color>();
            _colorQueries = colorQueries;
            _mapper = mapper;
        }

        public async Task<List<Color>> GetColorsAsync()
        {
            var colorsToReturn = await _colorQueries.GetAllColorsAsync();

            return colorsToReturn;
        }

        public async Task<Color> GetColorByIdAsync(Guid colorId)
        {
            var result = await _colorRepo.GetById(colorId);
            return result;
        }

        public async Task<Color> CreateColorAsync(ColorRequestModel requestModel)
        {
            var newColor = _mapper.Map<Color>(requestModel);

            var isColorExisted = await _colorQueries.CheckIfColorExisted(newColor.Name);

            if (isColorExisted)
                throw new StatusCodeException(message: "Color already exsited.", statusCode: StatusCodes.Status409Conflict);
            else
            {
                _colorRepo.Add(newColor);

                await _unitOfWork.CompleteAsync();

                return newColor;
            }
        }

        // Created colors - alreadyExistedColors - colorsHitErrorWhenCreated
        public async Task<(IEnumerable<Color>, IEnumerable<string>, IEnumerable<string>)> CreateBulkColorsAsync(List<ColorRequestModel> requestModels)
        {
            var colorsSuccessfullyCreated = new List<Color>();
            var alreadyExistedColors = new List<string>();
            var colorsHitErrorWhenCreated = new List<string>();
            
            foreach (var requestModel in requestModels)
            {
                try
                {
                    var newColor = _mapper.Map<Color>(requestModel);

                    var isColorExisted = await _colorQueries.CheckIfColorExisted(newColor.Name);

                    if (isColorExisted)
                        throw new StatusCodeException(message: "Color already exsited.", statusCode: StatusCodes.Status409Conflict);
                    else
                    {
                        _colorRepo.Add(newColor);

                        await _unitOfWork.CompleteAsync();

                        colorsSuccessfullyCreated.Add(newColor);
                    }
                }
                catch (StatusCodeException)
                {
                    alreadyExistedColors.Add(requestModel.Color);
                }
                catch (Exception)
                {
                    colorsHitErrorWhenCreated.Add(requestModel.Color);
                }
            }

            return (colorsSuccessfullyCreated, alreadyExistedColors, colorsHitErrorWhenCreated);
        }

        public async Task<Color> UpdateColorAsync(ColorRequestModel requestModel, Guid colorId)
        {
            var color = await _colorRepo.GetById(colorId);

            if (color == null)
                throw new StatusCodeException(message: "Color not found.", statusCode: StatusCodes.Status404NotFound);
            else
            {
                var colorToUpdate = _mapper.Map<ColorRequestModel, Color>(requestModel, color);

                var isColorExisted = await _colorQueries.CheckIfColorExisted(colorToUpdate.Name);

                if (isColorExisted)
                    throw new StatusCodeException(message: "Color already exsited.", statusCode: StatusCodes.Status409Conflict);
                else
                {
                    _colorRepo.Update(colorToUpdate);

                    await _unitOfWork.CompleteAsync();

                    return colorToUpdate;
                }
            }
        }

        public async Task<bool> DeleteColorAsync(Guid colorId)
        {
            var color = await _colorRepo.GetById(colorId);

            if (color == null)
                throw new StatusCodeException(message: "Color not found.", statusCode: StatusCodes.Status404NotFound);

            _colorRepo.Delete(colorId);

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
