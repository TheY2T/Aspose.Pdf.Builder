using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AsposePdfBuilder.Extensions
{
    public static class SerializationExtensions
    {
        /// <summary>
        /// Serializes an object of type <typeparamref name="T"/> into a byte array.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="toSerialize">toSerialize</param>
        public static byte[] SerializeObject<T>(this T toSerialize)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, toSerialize);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// De-serializes a byte array to an object of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="toDeserialize">toDeserialize</param>
        /// <returns>T</returns>
        public static T DeserializeObject<T>(this byte[] toDeserialize)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream(toDeserialize))
            {
                var result = bf.Deserialize(ms);
                return (T)result;
            }
        }
    }
}
