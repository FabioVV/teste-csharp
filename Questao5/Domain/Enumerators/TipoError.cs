namespace Questao5.Domain.Enumerators
{
    public enum TipoError
    {
        INVALID_ACCOUNT = 0,
        INACTIVE_ACCOUNT = 1,
        INVALID_VALUE = 2,
        INVALID_TYPE = 3,
        MISSING_HEADERS = 4,
        INVALID_IDEMPOTENCY_HEADER = 5,
    }
}
