﻿using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SoftLattice.PluginA
{
    public class MessageListener
    {
        private static MessageListener instance;
        public static MessageListener Instance { get { return instance ?? (instance = new MessageListener()); } }

        readonly ConcurrentBag<Tuple<DateTime, object>> messages = new ConcurrentBag<Tuple<DateTime, object>>();

        public void Handle(object message)
        {
            messages.Add(Tuple.Create(DateTime.Now, message));
        }

        public bool Contains<T>()
        {
            var msgs = messages.ToArray();
            return msgs.Any(o => o.Item2 is T);
        }
    }
}