namespace Transparity.Shared.Exceptions {
    public class NotFoundException : Exception {
        public NotFoundException(string? message) : base(message) { }
        public NotFoundException(string? message, Exception? innerEx)
            : base(message, innerEx) { }

        public static void ThrowIfNull(object argument, string paramName) {
            if (argument is null) {

            }
        }
    }
}
