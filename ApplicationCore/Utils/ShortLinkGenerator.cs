namespace ShortenerUrl.Api.ApplicationCore.Utils
{
    public static class ShortLinkGenerator
    {
        // ShortId with size 6 has a value space of 36^6, which is greater than int.MaxValue, so it's safe using only 6 chars
        private const int ShortLinkSize = 6;
        // Default base is [0-9][A-Z][a-z] shuffled
        private const string DefaultBase = "Gu7e6QIvtrfjEnsmUdyJ38lbMXA2pCNF0Pz1SYK4WaHZkBiDhLow9cOqx5VTgR";

        public static string ValueToShort(int value)
        {
            var shortId = new char[ShortLinkSize];
            var targetBase = DefaultBase.Length;
            var offset = 0;

            for (var i = ShortLinkSize - 1; i >= 0; i--)
            {
                var charForPosition = DefaultBase[(value + offset) % targetBase];
                shortId[i] = charForPosition;
                value /= targetBase;
                offset++;
            }

            return new string(shortId);
        }
    }
}
