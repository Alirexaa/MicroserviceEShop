namespace Core.Infrastructure.GuidGenerator
{
    public class SequentialGuidGeneratorOptions
    {

        public SequentialGuidType? DefaultSequentialGuidType { get; set; }

        public SequentialGuidType GetDefaultSequentialGuidType()
        {
            return DefaultSequentialGuidType ??
                   SequentialGuidType.SequentialAtEnd;
        }
    }
}
