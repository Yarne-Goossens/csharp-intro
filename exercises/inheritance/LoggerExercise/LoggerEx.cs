using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoggerExercise
{
    public abstract class Logger
    {
        public abstract void Log(string message);

        public virtual void Close()
        {
            //Empty
        }
    }

    public class StreamLogger : Logger
    {
        private readonly StreamWriter writer;
        public StreamLogger(StreamWriter writer)
        {
            this.writer = writer;
        }

        public override void Log(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }
    }

    public class FileLogger : StreamLogger
    {
        private readonly FileStream stream;

        public FileLogger(FileStream stream) : base(new StreamWriter(stream))
        {
            this.stream = stream;
        }

        public static Logger Create(string filename)
        {
            var file = File.OpenWrite(filename);
            return new FileLogger(file);
        }

        public override void Close()
        {
            stream.Close();
        }
    }

    public class NullLogger : Logger
    {
        public override void Log(string message)
        {
            //Empty
        }
    }

    public class LogBroadcaster : Logger
    {
        private readonly IList<Logger> loggers;

        public LogBroadcaster(IEnumerable<Logger> loggers)
        {
            this.loggers = loggers.ToList();
        }

        public override void Log(string message)
        {
            foreach (var log in loggers)
            {
                log.Log(message);
            }
        }

        public override void Close()
        {
            foreach (var log in loggers)
            {
                log.Close();
            }
        }
    }
}
