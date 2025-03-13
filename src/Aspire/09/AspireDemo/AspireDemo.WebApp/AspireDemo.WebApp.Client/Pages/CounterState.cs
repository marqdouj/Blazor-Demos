﻿using Marqdouj.CLRCommon;

namespace AspireDemo.WebApp.Client.Pages
{
    public class CounterState : StateContainer
    {
        #region CurrentCount
        private int currentCount;
        public int CurrentCount { get => currentCount; set => SetValue(ref currentCount, value); }
        #endregion
    }
}
