using System.Reflection;

namespace Transparity.Shared.Models {
    public class AppState {
        public bool IsReady { get; set; }
        public bool IsAlive { get; set; }

        public Dictionary<string, object> ToDictionary() {
            return GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(
                    prop => prop.Name,
                    prop => prop.GetValue(this) ?? default
                )!;
        }
    }
}
