using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Threading.Channels;

using Xunit;
using System.Collections.Generic;

namespace dotnet_system_threading_channels_koans
{
    public class Tests
    {
        [Fact]
        public void CompletionIsNotImmediate()
        {
            var channel = Channel.CreateUnbounded<int>();
            int lastValue = -1;
            var task = Task.Run(async () =>
                {
                    while (
                            !channel.Reader.Completion.IsCanceled
                            && !channel.Reader.Completion.IsCompleted
                            && await channel.Reader.WaitToReadAsync()
                            )
                    {
                        while (
                            !channel.Reader.Completion.IsCanceled
                            && !channel.Reader.Completion.IsCompleted
                            && channel.Reader.TryRead(out var value))
                        {
                            if (lastValue == 13)
                            {
                                Assert.True(channel.Writer.TryComplete());
                                Assert.Throws<ChannelClosedException>(() => channel.Writer.Complete());
                            }

                            lastValue = value;
                        }

                        if (channel.Reader.Completion.IsCompleted && lastValue == 2043)
                            throw new InvalidProgramException();
                    }
                });

            for (var i = 0; i < 2044; i++)
                channel.Writer.WriteAsync(i);

            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                //throw ex;
                Assert.Equal(ex.InnerExceptions.First().GetType(), typeof(InvalidProgramException));
            }


            Assert.True(channel.Reader.Completion.IsCompleted);
        }

        [Fact]
        public void HangToComplete1()
        {
            var channel = Channel.CreateUnbounded<int>();
            var completed = false;
            var hang = Task.Run(async () =>
            {
                while (await channel.Reader.WaitToReadAsync())
                {

                };
                completed = true;
            });

            Thread.Sleep(TimeSpan.FromSeconds(1));

            Assert.False(completed);
            channel.Writer.Complete();

            hang.Wait(TimeSpan.FromSeconds(10));
            Assert.True(completed);
        }

        [Fact]
        public void HangToComplete2()
        {

            var completed = false;
            var tasks = new List<Task>();
            var channels = new List<Channel<int>>();
            for (int i = 0; i < 50; i++)
            {
                var channel = Channel.CreateUnbounded<int>();
                var hang = Task.Run(async () =>
                {
                    while (await channel.Reader.WaitToReadAsync())
                    {
                        while (channel.Reader.TryRead(out var x))
                        {

                        }
                    };
                });
                tasks.Add(hang);
                channels.Add(channel);
            }


            Thread.Sleep(TimeSpan.FromSeconds(1));

            channels.AsParallel().ForAll(_ => _.Writer.WriteAsync(42));
            channels.AsParallel().ForAll(_ => _.Writer.Complete());

            Assert.True(Task.WaitAll(tasks.ToArray(), TimeSpan.FromSeconds(10)));

        }
    }
}
