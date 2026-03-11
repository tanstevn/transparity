using Transparity.Shared.Constants;

namespace Transparity.Shared.Exceptions {
    public class DataException : Exception {
        public DataException(string? message) : base(message) { }
        public DataException(string? message, Exception? innerEx) 
            : base(message, innerEx) { }

        public static void ThrowIfNullOrWhitespace(string argument, string argName) {
            if (string.IsNullOrWhiteSpace(argument)) {
                ThrowDataException(ErrorMessageConstants.ArgCannotBeNullOrWhitespace, argName);
            }
        }

        public static void ThrowIfNull(object argument, string argName) {
            if (argument is null) {
                ThrowDataException(ErrorMessageConstants.ArgCannotBeNull, argName);
            }
        }

        public static void ThrowIfDefault<T>(T argument, string argName) {
            var isArgumentDefault = EqualityComparer<T>.Default
                .Equals(argument, default);

            if (isArgumentDefault) {
                var errorMessage = string.Format(
                    ErrorMessageConstants.ArgCannotBeDefault, default(T));

                ThrowDataException(errorMessage, argName);
            }
        }

        private static void ThrowDataException(string message, string argName) {
            var argErrMessage = string.Format(
                ErrorMessageConstants.ArgErrMessage, argName);

            var errorMessage = string.Format("{0} {1}",
                argErrMessage, message);

            throw new DataException(errorMessage);
        }
    }
}
