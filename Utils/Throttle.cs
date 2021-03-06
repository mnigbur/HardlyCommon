﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace HD
{
  public class Throttle
  {
    DateTime lastRun;
    readonly TimeSpan minTimeBetweenRuns;

    public Throttle(
      TimeSpan minTimeBetweenRuns)
    {
      lastRun = DateTime.MinValue;
      this.minTimeBetweenRuns = minTimeBetweenRuns;
    }

    /// <summary>
    /// TODO how-to deal with a huge backlog(?)
    /// </summary>
    public void SleepIfNeeded()
    {
      TimeSpan timeToSleep = minTimeBetweenRuns - (DateTime.Now - lastRun);
      if (timeToSleep.Ticks > 0)
      {
        Thread.Sleep(timeToSleep);
      }
      SetLastUpdateTime();
    }

    public async Task WaitTillReady()
    {
      TimeSpan timeToSleep = minTimeBetweenRuns - (DateTime.Now - lastRun);
      if (timeToSleep.Ticks > 0)
      {
        await Task.Delay(timeToSleep);
      }
      SetLastUpdateTime();
    }

    public void SetLastUpdateTime()
    {
      lastRun = DateTime.Now;
    }
  }
}
