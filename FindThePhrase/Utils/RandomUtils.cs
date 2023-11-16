namespace FindThePhrase.Utils
{
    public static class RandomUtils
    {
        public static List<T> MultiSample<T>(this Random rnd, List<T> elements, int length)
        {
            List<T> result = new();

            if (elements.Count < length)
                throw new InvalidOperationException($"There must be at least {length} elements in the list.");

            while (result.Count < length)
            {
                var index = rnd.Next(elements.Count);
                T selectedElement = elements.ElementAt(index);
                elements.RemoveAt(index);
                result.Add(selectedElement);
            }

            return result;
        }
    }
}
