namespace TrainCross.Utils
{
    class SmartFramerate
    {
        double CurrentFrametimes;
        readonly double Weight;
        readonly int Numerator;

        public double Framerate
        {
            get
            {
                return (Numerator / CurrentFrametimes);
            }
        }

        public SmartFramerate(int oldFrameWeight)
        {
            Numerator = oldFrameWeight;
            Weight = (double)oldFrameWeight / ((double)oldFrameWeight - 1d);
        }

        public void Update(double timeSinceLastFrame)
        {
            CurrentFrametimes /= Weight;
            CurrentFrametimes += timeSinceLastFrame;
        }
    }
}
