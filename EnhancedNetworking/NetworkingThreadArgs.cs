//-----------------------------------------------------------------------
// <copyright file="NetworkingThreadArgs.cs" company="FHWN">
//   FHWN. All rights reserved.
// </copyright>
// <summary>Contains the NetworkingThreadArgs class.</summary>
// <author>Thomas Stranz</author>
//-----------------------------------------------------------------------
namespace MorskoyGoy
{
    using System;


    public abstract class NetworkingThreadArgs
    {
 
        public NetworkingThreadArgs(int pollDelay = 200)
        {
            if (pollDelay <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pollDelay), "The value must be greater than zero.");
            }

            this.PollDelay = pollDelay;
            this.Stop = false;
        }


        public int PollDelay
        {
            get;
            set;
        }

        public bool Stop
        {
            get;
            set;
        }
    }
}
