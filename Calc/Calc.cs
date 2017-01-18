namespace HSA.FingerGymnastics.Calc
{
    using Mhaze.Unity.Logging;
    using System;

    public class Calc
    {
        protected static Logger<Calc> logger = new Logger<Calc>();

        private float d;
        private float m;
        private float b;
        private float tn;
        private float x;
        private float minX = 0;
        private float maxX = 0;
        private float offset = 0;

        public Calc(float maxXPosition, float minXPosition, float velocity)
        {
            logger.AddLogAppender<ConsoleAppender>();

            maxX = maxXPosition;
            minX = minXPosition;

            d = maxX - minX;

            x = minX + new Random(DateTime.Now.TimeOfDay.Milliseconds).Next((int)(maxX - minX));
            tn = velocity;
            m = d / tn;
            b = minX;
        }
        
        public float GetXByTime(float time)
        {
            int count = UpdateValues(time);
            float offset = count * tn;

            x = m * (time - offset) + b;

            return x;
        }
        
        private int UpdateValues(float time)
        {
            int count = (int)(time / tn);

            if (count % 2 == 0)
            {
                m = d / tn;
                b = minX;
            }

            else
            {
                m = -d / tn;
                b = maxX;
            }

            return count;
        }

        public float X
        {
            get
            {
                return x;
            }
        }
        
        public bool IsLeft
        {
            get
            {
                if (x < 0)
                    return true;

                else if (x > 0)
                    return false;

                else
                    return b == maxX;
            }
        }
    }
}
