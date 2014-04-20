
namespace Itransition.Training.Task01
{
    /// <summary>
    /// Represents a class which is capable to count amount of own copies
    /// </summary>
    class Counter
    {
        private static volatile int counterCount;

        public Counter()
        {
            counterCount++;
        }

        ~Counter()
        {
            counterCount--;
        }

        /// <summary>
        /// Get the number of Counter copies
        /// </summary>
        public static int Count
        {
            get { return counterCount; }
        }
    }
}
