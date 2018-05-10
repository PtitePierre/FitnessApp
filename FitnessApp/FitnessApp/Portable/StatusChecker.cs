using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Xamarin.Forms;

namespace FitnessApp.Portable
{
    public class StatusChecker
    {
        private int invokeCount;
        private int maxCount;

        public StatusChecker(int count)
        {
            invokeCount = 0;
            maxCount = count;
        }

        // This method is called by the timer delegate.
        public void CheckStatus(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            
            Console.WriteLine("{0} Checking status {1,2}.",
                DateTime.Now.ToString("h:mm:ss.fff"),
                (++invokeCount).ToString());
            
            /*
            DependencyService.Get<IMessage>().shorttime((++invokeCount).ToString());
            */
            if (invokeCount == maxCount)
            {
                // Reset the counter and signal the waiting thread.
                invokeCount = 0;
                autoEvent.Set();
            }
        }
    }
}
