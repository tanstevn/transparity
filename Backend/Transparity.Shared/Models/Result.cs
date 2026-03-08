namespace Transparity.Shared.Models {
    public class Result<TData> {
        public bool Successful { get; set; }
        public TData? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public static Result<TData> Success(TData data) {
            return new() {
                Data = data,
                Successful = true
            };
        }

        public static Result<TData> Error(string error) {
            return new() {
                Errors = new List<string> {
                    error
                },
                Successful = false
            };
        }

        public static Result<TData> MultipleErrors(IEnumerable<string> errors) {
            return new() {
                Errors = errors,
                Successful = false
            };
        }
    }
}
