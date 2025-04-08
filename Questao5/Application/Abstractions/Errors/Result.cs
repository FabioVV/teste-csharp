using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Abstractions.Result
{
    public class Result<T>
    {
        public T Value { get; }
        public Errors Error { get; }
        public bool IsSuccess => Error == null;

        private Result(T value, Errors error)
        {
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(value, null);
        public static Result<T> Failure(Errors error) => new Result<T>(default, error);
    }
}
