namespace TimeCore.Services
{
    public class CounterService
    {
        public int Count { get; private set; }
        public void Increment()
        {
            Count += 1;
        }
    }
}
