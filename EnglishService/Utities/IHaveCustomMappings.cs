using AutoMapper;

namespace EnglishService.Utities
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
