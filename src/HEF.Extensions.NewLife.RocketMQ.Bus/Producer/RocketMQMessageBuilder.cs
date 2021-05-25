using NewLife.RocketMQ.Protocol;
using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQMessageBuilder
    {
        IRocketMQMessageBuilder Tag(string tag);

        IRocketMQMessageBuilder Key(string key);

        IRocketMQMessageBuilder Delay(int delayLevel);

        Message Build();
    }

    public class RocketMQMessageBuilder : IRocketMQMessageBuilder
    {
        private readonly Message _message;

        public RocketMQMessageBuilder()
        {
            _message = new Message();
        }

        public IRocketMQMessageBuilder Tag(string tag)
        {
            _message.Tags = tag;

            return this;
        }

        public IRocketMQMessageBuilder Key(string key)
        {
            _message.Keys = key;

            return this;
        }

        public IRocketMQMessageBuilder Delay(int delayLevel)
        {
            if (delayLevel < 1)
                delayLevel = 0;

            _message.DelayTimeLevel = delayLevel;

            return this;
        }

        public Message Build()
        {
            return _message;
        }
    }
}
