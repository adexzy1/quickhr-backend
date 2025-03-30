using qwikhr.Dtos.Position;
using qwikhr.Models;

namespace qwikhr.Mappers
{
    public static class PositionMappers
    {
        public static PositionDto ToPositionDto(this Position positionModel)
        {
            return new PositionDto
            {
                Id = positionModel.Id,
                Name = positionModel.Name
            };
        }

        public static Position ToPositionFromCreateDto(this CreatePositionDto positionDto)
        {
            return new Position
            {
                Name = positionDto.Name,
            };
        }
    }
}