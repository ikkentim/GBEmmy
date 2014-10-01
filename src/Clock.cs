namespace GBEmmy
{
    public class Clock
    {
        public short M { get; set; }
        public short T { get; set; }

        public void Reset()
        {
            M = 0;
            T = 0;
        }
    }
}
