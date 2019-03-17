﻿using System;

namespace Tweetinvi.Events
{
    public class AccountActivityEvent
    {
        public long AccountUserId { get; set; }
        public DateTime EventDate { get; set; }
        public string Json { get; set; }
    }


    public class AccountActivityEvent<T> : AccountActivityEvent
    {
        public T Args { get; set; }

        public AccountActivityEvent(T args)
        {
            Args = args;
        }
    }
}