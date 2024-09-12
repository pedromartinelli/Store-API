using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class GenericCommandResult(bool success, string message, object data) : ICommandResult
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public object Data { get; set; } = data;
    }
}
