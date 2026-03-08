namespace Transparity.Shared.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class PipelineOrderAttribute : Attribute {
        public short Order { get; }

        public PipelineOrderAttribute(short order) {
            Order = order;
        }
    }
}
