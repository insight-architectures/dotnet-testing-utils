using System.Threading.Tasks;

namespace Tests
{
    public interface ISynchronousService
    {
        string Echo(string message);
    }

    public interface IAsynchronousService
    {
        Task<string> EchoAsync(string message);
    }

    public interface IServiceWithCompositeParameter
    {
        void DoSomething(CompositeParameter parameter);
    }

    public class CompositeParameter
    {
        public string StringProperty { get; set; }

        public Task<string> GetString() => Task.FromResult(StringProperty);
    }
}
