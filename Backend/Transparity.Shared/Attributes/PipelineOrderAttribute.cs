namespace Transparity.Shared.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class PipelineOrderAttribute : Attribute {
        public short Order { get; private set; }

        public PipelineOrderAttribute(short order) {
            Order = order;
        }
    }
}
