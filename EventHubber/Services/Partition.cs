﻿using EventHubber.Model;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventHubber.Services
{
    public class Partition
    {

        IObserver<EventHubMessage> _messages;

        public string PartitionId { get; set; }

        public Partition(string partitionId, IObserver<EventHubMessage> messages )
        {
            PartitionId = partitionId;
            _messages = messages;
        }

        public Task ReadAsync(EventHubReceiver receiver, CancellationTokenSource cancellation)
        {
            return Task.Factory.StartNew(async () => {
                Debug.WriteLine(string.Format("partition {0} started", PartitionId));
               
 
                while (!cancellation.IsCancellationRequested)
                {
                    var message = await receiver.ReceiveAsync(TimeSpan.FromSeconds(5));
                    
                    if (message != null)
                    {
                        _messages.OnNext(new EventHubMessage(receiver.PartitionId,message));
                    }


                }
                await receiver.CloseAsync();
                Debug.WriteLine(string.Format("partition {0} finished", PartitionId));
            }, cancellation.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }
    }
}
