using DomainLab3;
using Infrastructure.DbWorkers.Common;

namespace Infrastructure.Converters
{
    public static class DbDtoConverter
    {
        public static Cource ConvertToCource(this DbDto dto)
        {
            if (dto == null || dto.Get().Count == 0)
                return null;
            Cource result = new Cource();
            result.Id = Guid.Parse(dto.Get()[0][0]);
            result.Version = Int32.Parse(dto.Get()[0][1]);
            result.CreatedAt = DateTime.Parse(dto.Get()[0][2]);
            if (!string.IsNullOrEmpty((dto.Get()[0][3])))
                result.UpdatedAt = DateTime.Parse((dto.Get()[0][3]));
            if (!string.IsNullOrEmpty((dto.Get()[0][4])))
                return null;
            return result;
        }
    }
}
