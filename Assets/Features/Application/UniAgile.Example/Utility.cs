using Newtonsoft.Json;

namespace UnityProjectTemplate.Application
{
    public static class Utility
    {
        public static string ToJson<T>(this T serialized)
        {
            return JsonConvert.SerializeObject(serialized);
        }

        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static int ToMs(this int integer)
        {
            return integer * 1000;
        }
    }
}