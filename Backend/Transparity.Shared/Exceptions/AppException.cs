using Transparity.Shared.Constants;

namespace Transparity.Shared.Exceptions {
    public class AppException : Exception {
        public AppException(string? message) : base(message) { }
        public AppException(string? message, Exception? innerEx) 
            : base(message, innerEx) { }

        public static void ThrowIfNull(object argument) {
            if (argument is null) {
                throw new AppException(ErrorMessageConstants.ServerErrMessage);
            }
        }
    }
}
